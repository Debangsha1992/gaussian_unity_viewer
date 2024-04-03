using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatDataManager : MonoBehaviour
{

    public static SplatDataManager Instance;

    public GameEvent DataLoadedFromFile_Event; 
    public SplatData Dat;
    SplatObject Obj;

    private void Awake()
    {
        Instance = this;
        Dat = new SplatData();
        Dat.SplatObjects = new List<SplatObject>();
    }

    private void Start()
    {
        LoadSplatData();
    }

    public void SaveSplatData(Vector3 UserPosition, Vector3 SplatPos, Quaternion SplatRot, Vector3 SplatScale, Vector3 GrabPos, Quaternion GrabRot, Vector3 GrabScale, float renderScale, string UID, string tag)
    {
        Obj = new SplatObject();
        Obj.SplatFileName = tag;
        Obj.UID = UID;

        Obj.SplatInitPosition = SplatPos;
        Obj.SplatInitRotation = SplatRot;
        Obj.SplatInitScale = SplatScale;

        Obj.GrabInitPosition = GrabPos;
        Obj.GrabInitRotation = GrabRot;
        Obj.GrabInitScale = GrabScale;

        Obj.SplatRenderScale = renderScale;

        Obj.UserInitPosition = UserPosition;

        if(Dat.SplatObjects.Count>0)
        Dat.SplatObjects.Clear();


        Dat.SplatObjects.Add(Obj);

        SaveData data = new SaveData();
        data.SavetoFile(Dat);
    }

    public void LoadSplatData()
    {
        SaveData data = new SaveData();
        Dat = data.LoadFromFile();

        if (Dat != null)
        {
            DataLoadedFromFile_Event?.InvokeEvent();
        }
        else
        {
            Dat = new SplatData();
            Dat.SplatObjects = new List<SplatObject>();
        }
    }
    
}
