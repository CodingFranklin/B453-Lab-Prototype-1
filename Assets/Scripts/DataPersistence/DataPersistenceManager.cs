using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager instance { get; private set; }

    private GameData gameData;

    private List<IDataPersistent> dataPersistenceObjects;

    [SerializeField] private string fileName;
    
    private FileDataHandler fileDataHandler;
    
    
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        Debug.Log(Application.persistentDataPath);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }
    
    public void LoadGame()
    {
        this.gameData = fileDataHandler.Load();
        
        if (this.gameData == null)
        {
            Debug.Log("GameData is null, initialize with default data");
            NewGame();
        }

        foreach (IDataPersistent dPObject in this.dataPersistenceObjects)
        {
            dPObject.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistent dPObject in this.dataPersistenceObjects)
        {
            dPObject.SaveData(ref gameData);
        }
        
        fileDataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame(); 
    }

    private List<IDataPersistent> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistent> dPObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistent>();
        return new List<IDataPersistent>(dPObjects);
    }
}
