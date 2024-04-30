using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SplatController : MonoBehaviour
{
    [SerializeField] private GaussianSplatting _splatPrefab;

    private GaussianSplatting _currentSplat;
    private bool _busy;

    public void EnableSplat(string uid)
    {
        StartCoroutine(EnableSplatRoutine(uid));
    }

    private IEnumerator EnableSplatRoutine(string uid)
    {
        if (_busy)
            yield break;

        _busy = true;

        if (_currentSplat != null)
            Destroy(_currentSplat.gameObject);

        yield return 0;

        SplatObject data = SplatDataManager.Instance.Dat.SplatObjects.First(x => x.UID == uid);

        GaussianSplatting splatClone = Instantiate(_splatPrefab, data.SplatInitPosition, data.SplatInitRotation, transform);
        splatClone.UID = data.UID;
        splatClone.model_file_path = data.SplatFileName;
        splatClone.cam = Camera.main;
        splatClone.renderScale = data.SplatRenderScale;
        //? how to get the model?
        splatClone.Init();

        _busy = false;
    }
}
