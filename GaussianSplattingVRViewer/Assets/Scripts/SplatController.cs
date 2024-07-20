using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SplatController : MonoBehaviour
{
    public static SplatController Instance;
    [SerializeField] private GaussianSplatting _splatPrefab;
    [SerializeField] private GaussianSplattingCameraBlit _cameraBlit;
    [SerializeField] private Transform _grabTRS;

    private void Awake()
    {
        Instance = this;
    }
    public GaussianSplatting GetCurrentGS => _currentSplat; 

    private GaussianSplatting _currentSplat;
    private bool _busy;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            EnableSplat("01");
        if(Input.GetKeyDown(KeyCode.Alpha2))
            EnableSplat("02");
    }

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

        if(data != null)    
            Debug.Log($"Found data file for splat ID {uid}.");
        else 
            Debug.Log($"Did not find data file for splat ID {uid}.");

        //GaussianSplatting splatClone = Instantiate(_splatPrefab, data.SplatInitPosition, data.SplatInitRotation, transform);
        GaussianSplatting splatClone = Instantiate(_splatPrefab, Vector3.zero, Quaternion.identity, transform);

        _cameraBlit.SetGS(splatClone);

        splatClone.UID = data.UID;
        splatClone.model_file_path = data.SplatFileName;
        splatClone.cam = Camera.main;
        splatClone.renderScale = data.SplatRenderScale;
        splatClone.trackTRS = _grabTRS;

        _currentSplat = splatClone;

        _busy = false;
    }
}
