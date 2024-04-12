using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatController : MonoBehaviour
{
    public List<GaussianSplatting> _splatters = new List<GaussianSplatting>();

    private GaussianSplatting _currentSplat = null;

    private bool _busy = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SplatObject obj = new SplatObject();
            obj.UID = "1";
            StartCoroutine(EnableSplat(obj));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SplatObject obj = new SplatObject();
            obj.UID = "2";
            StartCoroutine(EnableSplat(obj));
        }
    }

    public IEnumerator EnableSplat(SplatObject sObject)
    {
        if(_busy)
        {
            Debug.Log("Splat already changing this frame.");
            yield break;
        }

        _busy = true;

        if(_currentSplat)
            _currentSplat.gameObject.SetActive(false);

        yield return 0;

        _splatters.ForEach(x =>
        {
            if (sObject.UID == x.UID)
            {
                _currentSplat = x;
                _currentSplat.gameObject.SetActive(true);
            }
        });

        yield return 0;

        _currentSplat.transform.position = sObject.SplatInitPosition;
        _currentSplat.transform.rotation = sObject.SplatInitRotation;
        _currentSplat.transform.localScale = sObject.SplatInitScale;

        if (_currentSplat == null)
            Debug.Log("No splat found by UID");

        _busy = false;
    }
}
