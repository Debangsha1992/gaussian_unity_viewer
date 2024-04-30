using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GazeUI : MonoBehaviour
{
    [SerializeField] private Image _fillImage;

    public bool IsActive => _isActive;

    private bool _isActive;
    private CanvasGroup _cg;

    private void Start()
    {
        _cg = GetComponent<CanvasGroup>();
    }

    public void SetFill(float fill)
    {
        _fillImage.fillAmount = fill;
    }

    public void ShowReticle()
    {
        _cg.DOFade(1, .15f);
    }

    public void HideReticle()
    {
        _cg.DOFade(0, .15f);
    }
}
