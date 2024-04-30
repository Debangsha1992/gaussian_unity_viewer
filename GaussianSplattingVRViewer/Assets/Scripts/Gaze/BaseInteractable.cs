using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractable : MonoBehaviour, IInteractable
{
    public virtual void OnStartInteract()
    {
        Debug.Log("Start Interact");
    }

    public virtual void OnInteractUpdate()
    {
        Debug.Log("Update Interact");
    }

    public virtual void OnEndInteract()
    {
        Debug.Log("End Interact");
    }
}
