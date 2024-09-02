using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplatConfigGenerator : MonoBehaviour
{
    public string SceneName;
    public SplatData config = new SplatData();

    void Start()
    {
        string rootFolderPath;

#if UNITY_EDITOR
        // In Editor, use the Assets folder
        rootFolderPath = Application.dataPath;
#else
        // In Build, use the build's root folder
        rootFolderPath = Path.GetFullPath(Application.dataPath + "/..");
#endif

        // Define the path to the config file in the StreamingAssets folder
        string configFilePath = Path.Combine(Application.streamingAssetsPath, "config.json");

        Debug.Log("Root folder path: " + rootFolderPath);

        // Check if the config file exists
        if (File.Exists(configFilePath))
        {
            // Load existing config if it exists
            string existingJson = File.ReadAllText(configFilePath);
            config = JsonUtility.FromJson<SplatData>(existingJson);
            Debug.Log("Loaded existing config file.");
        }
        else
        {
            // Create new config data if the file doesn't exist
            config = new SplatData
            {
                SplatObjects = new List<SplatObject>()
            };

            Debug.Log("Config file does not exist. Creating a new one.");
        }

        // Get all .ply and .byte files in the root folder
        string[] splatFiles = Directory.GetFiles(rootFolderPath, "*.ply");
        string[] byteFiles = Directory.GetFiles(rootFolderPath, "*.byte");

        // Log the number of .ply and .byte files found
        Debug.Log("Number of .ply files found: " + splatFiles.Length);
        Debug.Log("Number of .byte files found: " + byteFiles.Length);

        int uidCounter = 123; // Starting UID

        // Combine both file types into a single array
        string[] allFiles = new string[splatFiles.Length + byteFiles.Length];
        splatFiles.CopyTo(allFiles, 0);
        byteFiles.CopyTo(allFiles, splatFiles.Length);

        Debug.Log("Total files found: " + allFiles.Length);

        // Iterate through all files and check if they are already in the config
        foreach (string file in allFiles)
        {
            string fileName = Path.GetFileName(file);
            Debug.Log("Processing file: " + fileName);

            // Check if this file is already in the config
            bool fileExists = config.SplatObjects.Exists(splat => splat.SplatFileName == fileName);

            if (!fileExists)
            {
                SplatObject splatObject = new SplatObject
                {
                    UID = uidCounter.ToString(),
                    SplatFileName = fileName,
                    SplatInitPosition = new Vector3(0, 0, 0),
                    SplatInitRotation = Quaternion.identity,
                    SplatInitScale = new Vector3(1, 1, 1),
                    GrabInitPosition = new Vector3(0, 0, 0),
                    GrabInitRotation = Quaternion.identity,
                    GrabInitScale = new Vector3(1, 1, 1),
                    SplatRenderScale = 1.0f,
                    UserInitPosition = new Vector3(0, 0, 0)
                };

                config.SplatObjects.Add(splatObject);
                uidCounter++;
                Debug.Log("Added new SplatObject: " + fileName);
            }
            else
            {
                Debug.Log("File already exists in config: " + fileName);
            }
        }

        // Convert to JSON and update or create config.json
        string newJson = JsonUtility.ToJson(config, true);
        File.WriteAllText(configFilePath, newJson);
        Debug.Log("config.json file created/updated at: " + configFilePath);

        // Load the next scene asynchronously
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
