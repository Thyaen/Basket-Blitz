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

    private IEnumerator Start()
    {
        soundManager = FindFirstObjectByType<SoundManager>();

        // Eine Frame warten, bis alle Manager initialisiert sind
        yield return null;

        foreach (Transform spawnPoint in transform)
        {
            SpawnApple(spawnPoint);
            StartCoroutine(SpawnRoutine(spawnPoint));
        }
    }

    private IEnumerator SpawnRoutine(Transform spawnPoint)
    {
        SpawnPointData data = spawnPoint.GetComponent<SpawnPointData>();

        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            if (data.currentApple != null)
                continue;

            SpawnApple(spawnPoint);
        }
    }

    private void SpawnApple(Transform spawnPoint)
    {
        GameObject apple = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

        SpawnPointData data = spawnPoint.GetComponent<SpawnPointData>();

        if (data == null)
            data = spawnPoint.gameObject.AddComponent<SpawnPointData>();

        data.currentApple = apple;

        AppleBehaviour appleBehaviour = apple.GetComponent<AppleBehaviour>();

        if (appleBehaviour != null)
        {
            appleBehaviour.SetSpawnPoint(data);
        }

        if (soundManager != null)
        {
            soundManager.PlaySpawnSound();
        }
    }
}