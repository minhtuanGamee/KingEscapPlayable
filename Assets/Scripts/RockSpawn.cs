using System.Collections;
using UnityEngine;

public class RockSpawn : MonoBehaviour
{
    public GameObject RockPrefab;
    public Transform SpawnPoint;
    public int RockCount = 10;

    [SerializeField] private float randomOffset = 0.5f;

    private IEnumerator Start()
    {
        float spawnInterval = 5f / RockCount;

        for (int i = 0; i < RockCount; i++)
        {
            Vector3 randomPosition = SpawnPoint.position + new Vector3(
                Random.Range(-randomOffset, randomOffset),
                Random.Range(-randomOffset, randomOffset),
                0f
            );

            Instantiate(RockPrefab,randomPosition,SpawnPoint.rotation, this.transform);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
