using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatOrientationController : MonoBehaviour
{
    public List<GaussianSplatting> _splatters = new List<GaussianSplatting>();

    private GaussianSplatting _currentSplat = null;

    public IEnumerator EnableSplat(SplatObject sObject)
    {
        if(_currentSplat)
            _currentSplat.gameObject.SetActive(false);

        yield return 0;

        _splatters.ForEach(x =>
        {
            if (sObject.UID == x.UID)
            {
                _currentSplat = x;
                _currentSplat.gameObject.SetActive(true);
                _currentSplat.transform.position = sObject.SplatInitPosition;
                _currentSplat.transform.rotation = sObject.SplatInitRotation;
                _currentSplat.transform.localScale = sObject.SplatInitScale;
            }
        });
    }
}
