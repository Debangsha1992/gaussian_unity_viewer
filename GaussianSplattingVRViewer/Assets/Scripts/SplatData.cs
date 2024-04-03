using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SplatData
{
    public List<SplatObject> SplatObjects;
}


[System.Serializable]
public class SplatObject
{
    public string UID;
    public string SplatFileName;
    public string SplatAudioFileName;

    public Vector3 SplatInitPosition;
    public Quaternion SplatInitRotation;
    public Vector3 SplatInitScale;

    public Vector3 GrabInitPosition;
    public Quaternion GrabInitRotation;
    public Vector3 GrabInitScale;

    public float SplatRenderScale;


    public Vector3 UserInitPosition;

}

