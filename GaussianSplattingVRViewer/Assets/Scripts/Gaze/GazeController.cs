using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;

public class GazeController : MonoBehaviour
{
    [SerializeField] private float _interactDuration = 1;
    [SerializeField] private GazeUI _gazeUI;

    public bool CanInteract => _canInteract;

    private bool _canInteract = true;
    private float _currentInteractTime = 0;
    private float _interactDistance = 100;
    private float _volumeRadius = .1f;
    private BaseInteractable _currentInteractable = null;
    private bool _interacting;

    private void Start()
    {
        _gazeUI.SetFill(0);
        _gazeUI.ShowReticle();
    }

    private void Update()
    {
        if (_canInteract)
        {
            if (GetClosestInteractable() != null)
            {
                // if (_gazeUI.IsActive == false)
                //     _gazeUI.ShowReticle();

                if (GetClosestInteractable() != null && _currentInteractable != GetClosestInteractable())
                {
                    _currentInteractTime = 0;
                    _currentInteractable?.OnEndInteract();
                    _interacting = false;
                }

                _currentInteractable = GetClosestInteractable();
            }
            else
            {
                if (_currentInteractable != null)
                {
                    //_gazeUI.HideReticle();
                    _gazeUI.SetFill(0);
                    _currentInteractable.OnEndInteract();
                    _currentInteractable = null;
                    _interacting = false;
                    _currentInteractTime = 0;
                }
                else
                    _gazeUI.SetFill(0);

                return;
            }

            if (_currentInteractable == GetClosestInteractable())
            {
                if (!_interacting)
                {
                    _gazeUI.SetFill(_currentInteractTime / _interactDuration);
                    _currentInteractTime += Time.deltaTime;
                }

                if (!_interacting && _currentInteractTime > _interactDuration)
                {
                    _currentInteractTime = 0;
                    _currentInteractable.OnStartInteract();
                    _interacting = true;
                }
            }
        }
        else if (!_canInteract && _currentInteractTime > 0)
        {
            _currentInteractTime = 0;
            _currentInteractable = null;
            _interacting = false;
        }

        if (_currentInteractable != null && _interacting)
            _currentInteractable.OnInteractUpdate();
    }

    private BaseInteractable GetClosestInteractable()
    {
        Collider[] hits = Physics.OverlapCapsule(transform.position, transform.position + transform.forward * _interactDistance, _volumeRadius);
        Debug.DrawLine(transform.position, transform.position + transform.forward * _interactDistance);
        float dist = 1000;
        BaseInteractable target = null;
        for (int i = 0; i < hits.Length; i++)
            if (hits[i].GetComponent<BaseInteractable>() != null &&
                Vector3.Distance(transform.position, hits[i].transform.position) < dist)
                target = hits[i].GetComponent<BaseInteractable>();
        if (target)
            return target;
        else
            return null;

    }

    public void SetAllowInteract(bool state)
    {
        _canInteract = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }
}
