using System;
using System.IO;
using UnityEngine;

public class SaveData
{

    public static string filePath = Application.streamingAssetsPath + "/config.JSON";
    public void SavetoFile(SplatData data)
    {

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(filePath, json);

        Debug.Log("File Saving Completed");
    }

    public SplatData LoadFromFile()
    {
        if (File.Exists(filePath))
        {
            // Read the JSON string from the file
            string json = File.ReadAllText(filePath);

            // Deserialize the JSON string to a Player object
            SplatData loadedData = JsonUtility.FromJson<SplatData>(json);

            // Check if the loadedPlayer is not null (deserialize successful)
            if (loadedData != null)
            {
                return loadedData;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

}
