using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoPeDT.BodilyAwarenessBalance
{
    public class LineChunkSpawner : ChunkSpawnerBase
    {
        [Header("Line")]
        public float lineWidthMultiplier = 1.0f;


        private void Start()
        {
            Init();
        }

        private void Update()
        {
            FollowTarget();
        }

        protected override GameObject SpawnElement(Transform chunk, Vector3 elementPosition)
        {
            var go = base.SpawnElement(chunk, elementPosition);
            go.GetComponent<LineRendererCoordinator>().SetWidthMultiplier(lineWidthMultiplier * 0.05f);

            return go;
        }

        public void SetWidthMultiplier(float multiplier)
        {
            lineWidthMultiplier = multiplier;

            DestroyChunks();
            PoolChunks();
            SpawnChunks();
        }
    }
}