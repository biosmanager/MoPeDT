using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MoPeDT.BodilyAwarenessBalance
{
    public abstract class ChunkSpawnerBase : MonoBehaviour
    {
        [Header("Grid")]
        public int gridSize = 3;
        public float chunkSize = 10;
        public Transform target;
        public bool alignToTargetYaw = false;

        [Space]

        [Header("Chunk")]
        public GameObject chunkElementPrefab;
        public float chunkElementPrefabScale = 1.0f;
        public int elementsPerChunkAxis = 10;
        public float offsetDivide = 2;
        public float randomOffsetDistance = 0;

        [Space]

        [Header("Distance fade")]
        public float minDistance = 0.0f;
        public float distance1 = 2.5f;
        public float distance2 = 7.5f;
        public float maxDistance = 10.0f;


        protected GameObject[,,] chunks;
        protected float spacing;
        protected Material material;


        protected virtual void Init()
        {
            // Set distance material properties
            material = chunkElementPrefab.GetComponentInChildren<Renderer>().sharedMaterial;

            // Spawn grid within chunks
            PoolChunks();
            SpawnChunks();
        }

        protected void FollowTarget()
        {
            transform.position = new Vector3(Mathf.Round(target.position.x / chunkSize) * chunkSize, Mathf.Round(target.position.y / chunkSize) * chunkSize, Mathf.Round(target.position.z / chunkSize) * chunkSize);
            if (alignToTargetYaw)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, target.eulerAngles.y, transform.eulerAngles.z);
            }
        }


        public void SetChunkParameters(float? chunkElementPrefabScale = null, int? elementsPerChunkAxis = null, float? chunkSize = null, float? randomOffsetDistance = null)
        {
            if (chunkElementPrefabScale.HasValue) this.chunkElementPrefabScale = chunkElementPrefabScale.Value;
            if (elementsPerChunkAxis.HasValue) this.elementsPerChunkAxis = elementsPerChunkAxis.Value;
            if (chunkSize.HasValue) this.chunkSize = chunkSize.Value;
            if (randomOffsetDistance.HasValue) this.randomOffsetDistance = randomOffsetDistance.Value;

            DestroyChunks();
            PoolChunks();
            SpawnChunks();
        }

        protected void PoolChunks()
        {
            chunks = new GameObject[gridSize, gridSize, gridSize];

            spacing = chunkSize / elementsPerChunkAxis;
            var totalGridSize = gridSize * chunkSize;

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    for (int k = 0; k < gridSize; k++)
                    {
                        var chunkPosition = new Vector3(i * chunkSize, j * chunkSize, k * chunkSize) - new Vector3(totalGridSize / 2, totalGridSize / 2, totalGridSize / 2);
                        var go = new GameObject();
                        go.transform.parent = transform;
                        go.transform.localPosition = chunkPosition;
                        go.name = $"Chunk ({i},{j},{k})";
                        chunks[i, j, k] = go;
                    }
                }
            }
        }

        protected virtual void SpawnChunks()
        {
            foreach (var chunk in chunks)
            {
                for (int z = 0; z < elementsPerChunkAxis; z++)
                {
                    for (int y = 0; y < elementsPerChunkAxis; y++)
                    {
                        for (int x = 0; x < elementsPerChunkAxis; x++)
                        {
                            if ((x + y + z) % 2 == 0)
                            {
                                SpawnElement(chunk.transform, new Vector3(x, y, z));
                            }
                        }
                    }
                }
            }
        }

        protected virtual GameObject SpawnElement(Transform chunk, Vector3 elementPosition)
        {
            var pos = Vector3.Scale(elementPosition, new Vector3(spacing, spacing, spacing)) + UnityEngine.Random.onUnitSphere.normalized * randomOffsetDistance;
            var go = Instantiate(chunkElementPrefab, chunk.transform.position + pos, Quaternion.identity);
            go.transform.SetParent(chunk.transform);
            go.transform.localScale = new Vector3(chunkElementPrefabScale, chunkElementPrefabScale, chunkElementPrefabScale);

            return go;
        }

        public virtual void DestroyChunks()
        {
            if (chunks is null) return;

            foreach (var chunk in chunks)
            {
                if (chunk is object)
                {
                    Destroy(chunk);
                }
            }
        }
    }
}