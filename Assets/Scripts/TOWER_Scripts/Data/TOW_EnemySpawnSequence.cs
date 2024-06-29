using System;
using UnityEngine;

namespace TOWER
{
    [Serializable]
    public class TOW_EnemySpawnSequence
    {
        public TOW_EnemyController enemyPrefab;
        public int amount;
        public int spawnRate;
    }
}