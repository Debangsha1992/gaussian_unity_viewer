using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GaussianSplattingCameraBlit : MonoBehaviour
{
    public GaussianSplatting gs;
    Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    public void SetGS(GaussianSplatting splat)
    {
        gs = splat;
    }

    [ImageEffectOpaque]
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(gs == null) 
            return;
        
        Material mat = gs.mat;

        if (mat == null)
        {
            Graphics.Blit(source, destination);
            return;
        }
        
        Graphics.Blit(source, destination, mat);
    }
}
