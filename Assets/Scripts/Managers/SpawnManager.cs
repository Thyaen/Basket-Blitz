using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private SoundManager soundManager;

    [Header("Prefab, das gespawnt werden soll")]
    public GameObject prefabToSpawn;

    [Header("Spawnintervall")]
    public float minSpawnTime = 10f;
    public float maxSpawnTime = 16f;

    private void Start()
    {
        soundManager = FindFirstObjectByType<SoundManager>();
        // Für jedes Kind des SpawnManagers eine Spawn-Routine starten
        foreach (Transform spawnPoint in transform)
        {
            StartCoroutine(SpawnRoutine(spawnPoint));
        }
    }

    private IEnumerator SpawnRoutine(Transform spawnPoint)
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

            if (soundManager != null)
            {
                soundManager.PlaySpawnSound();
            }
        }
    }
}