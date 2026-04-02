using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetSpawner : MonoBehaviour, IDataPersistent
{
    public static TargetSpawner instance;
    
    [SerializeField] GameObject targetPrefab;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float spawnInterval = 1f;
    [SerializeField] private TextMeshProUGUI maxSpawnText;
    public int maxSpawnCount = 10;
    private int spawnCount = 0;
    void Start()
    {
        if (instance == null) instance = this;
        maxSpawnText.text = maxSpawnCount.ToString();
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            SpawnTarget();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnTarget()
    {
        if (spawnCount < maxSpawnCount)
        {
            // randomly pick a spawn point to spawn a target
            int rand = Random.Range(0, spawnPoints.Length);
            GameObject target = Instantiate(targetPrefab, spawnPoints[rand].position, Quaternion.identity);
            spawnCount++;
        }
    }

    public void LoadData(GameData data)
    {
        this.maxSpawnCount = data.maxSpawnCount;
    }

    public void SaveData(ref GameData data)
    {
        data.maxSpawnCount = maxSpawnCount + 1;
    }
}
