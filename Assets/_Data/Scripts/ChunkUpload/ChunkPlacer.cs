using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    public Transform player;
    public GameObject _player;
    public Chunk[] ChunkPrefabs;
    public Chunk FirstChunk;

    private List<Chunk> spawnedChunks = new List<Chunk>();

    void Start()
    {
        spawnedChunks.Add(FirstChunk);
    }

    // Update is called once per frame
    void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        player = _player.transform;
        if(player.position.x > spawnedChunks[spawnedChunks.Count -1].End.position.x -120)
        {
            SpawnedChunk();
        }
    }
    private void SpawnedChunk()
    {
        Chunk newChunk = Instantiate(ChunkPrefabs[UnityEngine.Random.Range(0, ChunkPrefabs.Length)]);
        newChunk.transform.position = spawnedChunks[spawnedChunks.Count-1].End.position - newChunk.Begin.localPosition;
        spawnedChunks.Add(newChunk);

        if(spawnedChunks.Count > 3)
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
        }
    }
}
