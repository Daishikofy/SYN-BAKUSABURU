using UnityEditor;
using UnityEngine;

namespace TOWER
{
    [CreateAssetMenu]
    public class TOW_EnemyWave : ScriptableObject
    {
        public TOW_EnemySpawnSequence[] enemySpawnSequences = {new TOW_EnemySpawnSequence()};
    }
}