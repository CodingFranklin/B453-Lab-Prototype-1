using System.Collections;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] GameObject targetPrefab;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float spawnInterval = 1f;
    [SerializeField] int maxSpawnCount = 10;
    private int spawnCount = 0;
    void Start()
    {
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
        if (spawnCount <= maxSpawnCount)
        {
            // randomly pick a spawn point to spawn a target
            int rand = Random.Range(0, spawnPoints.Length);
            GameObject target = Instantiate(targetPrefab, spawnPoints[rand].position, Quaternion.identity);
            spawnCount++;
        }
    }
}
