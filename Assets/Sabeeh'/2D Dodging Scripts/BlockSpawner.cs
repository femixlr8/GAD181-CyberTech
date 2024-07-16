using System.Collections;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    public GameObject blockPrefab;
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            int randomIndex = Random.Range(0, spawnPoints.Length);

            Debug.Log(randomIndex);
            GameObject block = Instantiate(blockPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
            block.transform.position = new Vector3(block.transform.position.x, block.transform.position.y, 0);
        }
    }
}
