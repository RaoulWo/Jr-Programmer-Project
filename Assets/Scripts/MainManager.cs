using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Color TeamColor;

    private void Awake()
    {
        // If singleton is set, destroy all other MainManager objects
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Initialize the singleton instance
        Instance = this;
        
        // Let the Main Manager persist between scenes
        DontDestroyOnLoad(gameObject);

        // Load the color
        LoadColor();
    }

    // The class to be serialized to json
    [System.Serializable]
    private class SaveData
    {
        public Color TeamColor;
    }

    // Serializes the SaveData to json
    public void SaveColor()
    {
        // Create the SaveData object to be stored
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        // Serialize the SaveData object
        string json = JsonUtility.ToJson(data);

        // Write the json to savefile.json
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // Deserializes the json to SaveData
    public void LoadColor()
    {
        // Get the path of savefile.json
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            // Read the file content
            string json = File.ReadAllText(path);

            // Deserialize the json to SaveData
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Load the color
            TeamColor = data.TeamColor;
        }
    }
}
