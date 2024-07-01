using System;
using UnityEngine;

namespace TOWER
{
    [Serializable]
    public class TOW_SpawnSequence
    {
        public TOW_EnemyController enemyPrefab;
        public int amount;
        public float spawnRate;
    }
}