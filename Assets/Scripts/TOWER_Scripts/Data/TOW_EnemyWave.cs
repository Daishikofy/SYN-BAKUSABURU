using System;
using UnityEditor;
using UnityEngine;

namespace TOWER
{
    [Serializable]
    public class TOW_EnemyWave
    {
        public TOW_SpawnPointSequences[] spawnPointsSequences = {new TOW_SpawnPointSequences()};

        public int GetEnemiesInWaveCount()
        {
            int count = 0;

            foreach (TOW_SpawnPointSequences spawnPointsSequence in spawnPointsSequences)
            {
                foreach (TOW_SpawnSequence sequence in spawnPointsSequence.spawnSequences)
                {
                    count += sequence.amount;
                }
            }

            return count;
        }
    }
}