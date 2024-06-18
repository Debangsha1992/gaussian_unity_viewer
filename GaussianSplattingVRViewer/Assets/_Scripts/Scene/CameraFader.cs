using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFader : MonoBehaviour
{
    [SerializeField] private MeshRenderer _faderMesh;

    private Material _mat;
    private bool _busy;

    private void Start()
    {
        _mat = _faderMesh.material;
        _mat.color = new Color(0,0,0,0);
        _faderMesh.enabled = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            FadeToBlack(2);

        if(Input.GetKeyDown(KeyCode.Alpha2))
            FadeToTransparent(2);
    }

    public void FadeToBlack(float duration)
    {
        if (!_busy)
            StartCoroutine(Fade(duration, true));
    }

    public void FadeToTransparent(float duration)
    {
        if (!_busy)
            StartCoroutine(Fade(duration, false));
    }

    private IEnumerator Fade(float duration, bool fadeToBlack)
    {
        if(fadeToBlack)
            _faderMesh.enabled = true;
        float endColor = fadeToBlack ? 1 : 0;
        _busy = true;
        _mat.DOFade(endColor, duration);
        yield return new WaitForSeconds(duration);
        _busy = false;
        if(fadeToBlack == false)
            _faderMesh.enabled = false;
    }
}
