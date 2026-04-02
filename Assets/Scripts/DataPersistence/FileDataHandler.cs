using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataPath = "";
    
    private string dataFileName = "";

    public FileDataHandler(string dataPath, string dataFileName)
    {
        this.dataPath = dataPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        String fullPath = Path.Combine(this.dataPath, dataFileName);
        GameData LoadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                
                LoadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured while trying to save file: " + fullPath + "\n" + e);
            }
        }

        return LoadedData;
    }

    public void Save(GameData gameData)
    {
        String fullPath = Path.Combine(this.dataPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            
            String dataToStore = JsonUtility.ToJson(gameData, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured while trying to save file: " + fullPath + "\n" + e);
        }
    }
}
