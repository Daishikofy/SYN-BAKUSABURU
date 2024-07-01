using System;
using System.Collections.Generic;
using UnityEngine;

namespace TOWER
{
    [Serializable]
    public class TOW_SpawnPointManager
    {
        [SerializeField] private List<TOW_SpawnPointController> spawnPoints;

        private TOW_EnemyWave _currentWave;
        
        private List<TOW_EnemyController> _enemies = new List<TOW_EnemyController>();
        private int _enemyCount;

        private int _defeatedEnemiesCount;
        private int _enemiesToDefeatCount;

        public void InitializeCurrentWave(TOW_EnemyWave wave)
        {
            _currentWave = wave;
            
            foreach (TOW_SpawnPointSequences spawnPointSequences in _currentWave.spawnPointsSequences)
            {
                spawnPoints[spawnPointSequences.spawnPointId].SetupWave(spawnPointSequences.spawnSequences);
            }
            
            _enemies.Clear();
            _enemyCount = 0;
            _defeatedEnemiesCount = 0;
            _enemiesToDefeatCount = _currentWave.GetEnemiesInWaveCount();
        }
        
        public List<TOW_EnemyController> GetEnemiesInRange(Vector2 position, float range)
        {
            List<TOW_EnemyController> enemiesInRange = new List<TOW_EnemyController>();
            for (int i = 0; i < _enemyCount; i++)
            {
                TOW_EnemyController enemy = _enemies[i];
                if (Vector2.Distance(position, enemy.transform.position) <= range)
                {
                    enemiesInRange.Add(enemy);
                }
            }

            return enemiesInRange;
        }

        public void OnEnemySpawned(TOW_EnemyController enemy)
        {
            if (_enemies.Count > _enemyCount)
            {
                _enemies[_enemyCount] = enemy;
            }
            else
            {
                _enemies.Add(enemy);
            }

            _enemyCount++;
        }

        public void OnEnemyDefeated(TOW_EnemyController enemy)
        {
            for (int i = 0; i < _enemyCount; i++)
            {
                if (i != _enemyCount - 1 && _enemies[i] == enemy)
                {
                    _enemies[i] = _enemies[_enemyCount - 1];
                }
            }

            _enemyCount -= 1;
            _defeatedEnemiesCount += 1;
            if (_defeatedEnemiesCount >= _enemiesToDefeatCount)
            {
                TOW_GameManager.Instance.OnWaveEnded();
            }
        }
    }
}