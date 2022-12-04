using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private float spawnPosition;
    [SerializeField] private float tileLength;
    [SerializeField] private int tileNumber;
    [SerializeField] private Transform playerTransform;


    private List<GameObject> _activeTiles;
    
    // Start is called before the first frame update
    void Start()
    {
        _activeTiles = new List<GameObject>();
        tileLength = 30;
        tileNumber = 5;

        for (int i = 0; i < tilePrefabs.Length; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 35 > spawnPosition - (tileNumber * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPosition, transform.rotation);
        _activeTiles.Add(go);
        spawnPosition += tileLength;
    }
    
    private void DeleteTile()
    {
        Destroy(_activeTiles[0]);
        _activeTiles.RemoveAt(0);
    }
}
