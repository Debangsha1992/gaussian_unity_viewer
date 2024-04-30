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
        _mat.color = Color.black;
        _faderMesh.enabled = true;
        FadeToTransparent(2);
    }

    public void FadeToTransparent(float duration)
    {
        if (!_busy)
            StartCoroutine(Fade(duration, false, true));
    }

    public void FadeToBlack(float duration)
    {
        if (!_busy)
            StartCoroutine(Fade(duration, true, false));
    }

    private IEnumerator Fade(float duration, bool fadeToBlack, bool disableAfter)
    {
        if (!disableAfter)
            _faderMesh.enabled = true;

        float endColor = fadeToBlack ? 1 : 0;
        _busy = true;
        _mat.DOFade(endColor, duration);
        yield return new WaitForSeconds(duration);
        _busy = false;

        if (disableAfter)
            _faderMesh.enabled = false;
    }
}
