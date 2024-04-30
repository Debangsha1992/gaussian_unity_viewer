using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GazeTestObject : BaseInteractable
{
    public Color startInteractColor = Color.green;
    public Color updateInteractColor = Color.yellow;
    private Color baseColor;
    private MeshRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        baseColor = renderer.material.color;
    }

    public override void OnStartInteract()
    {
        base.OnStartInteract();
        renderer.material.color = startInteractColor;
        renderer.material.DOColor(updateInteractColor, .5f);
    }

    public override void OnInteractUpdate()
    {
        base.OnInteractUpdate();
    }

    public override void OnEndInteract()
    {
        base.OnEndInteract();
        renderer.material.color = baseColor;
    }
}
