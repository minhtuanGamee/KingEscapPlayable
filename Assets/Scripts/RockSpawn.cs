using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawn : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int rockCount = 10;
    [SerializeField] private float randomOffset = 0.5f;
    [SerializeField] private float spawnDuration = 5f;

    [Header("Pool")]
    [SerializeField] private GameObject rockPrefab;

    private Coroutine spawnCoroutine;

    private readonly Queue<GameObject> pool = new();
    private readonly List<GameObject> activeRocks = new();

    private void OnEnable()
    {
        EventBus.ResetGame += Setup;
    }

    private void OnDisable()
    {
        EventBus.ResetGame -= Setup;
    }

    private void Start()
    {
        Setup();
    }

    public void Setup()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
        for (int i = activeRocks.Count - 1; i >= 0; i--)
        {
            GameObject rock = activeRocks[i];

            if (rock != null)
            {
                rock.SetActive(false);
                rock.transform.SetParent(transform);
                pool.Enqueue(rock);
            }
        }

        activeRocks.Clear();


        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        float spawnInterval = spawnDuration / rockCount;

        for (int i = 0; i < rockCount; i++)
        {
            SpawnRock();

            yield return new WaitForSeconds(spawnInterval);
        }

        spawnCoroutine = null;
    }

    private GameObject CreateRock()
    {
        GameObject rock = Instantiate(rockPrefab, transform);
        rock.SetActive(false);

        return rock;
    }

    private void SpawnRock()
    {
        GameObject rock;

        if (pool.Count == 0)
        {
            rock = CreateRock();
        }
        else
        {
            rock = pool.Dequeue();
        }

        Vector3 randomPosition = spawnPoint.position + new Vector3(
            Random.Range(-randomOffset, randomOffset),
            Random.Range(-randomOffset, randomOffset),
            0f
        );

        rock.transform.SetPositionAndRotation(
            randomPosition,
            spawnPoint.rotation
        );

        rock.SetActive(true);

        activeRocks.Add(rock);
    }

    public void ReturnRock(GameObject rock)
    {
        activeRocks.Remove(rock);

        rock.SetActive(false);
        rock.transform.SetParent(transform);

        pool.Enqueue(rock);
    }
}