using System;
using System.Collections.Generic;
using UnityEngine;

namespace TOWER
{
    public class TOW_GameManager : MonoBehaviour
    {
        private static TOW_GameManager _instance;
        public static TOW_GameManager Instance => _instance;

        [SerializeField] private TOW_SpawnPointController[] spawnPoints;
        [SerializeField] private TOW_LevelScenario levelScenario;

        public List<TOW_EnemyController> enemies;
        private int _enemyCount;
        private int _currentWave;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
            }

            _enemyCount = enemies.Count;
        }

        private void Start()
        {
            StartPlayingLevel();
        }

        private void StartPlayingLevel()
        {
            spawnPoints[0].SetupWave(levelScenario.enemyWaves[_currentWave].enemySpawnSequences);
            spawnPoints[0].onWaveEnded.AddListener(OnWaveEnded);
        }

        private void OnWaveEnded()
        {
            _currentWave++;
            if (_currentWave < levelScenario.enemyWaves.Length)
            {
                spawnPoints[0].SetupWave(levelScenario.enemyWaves[_currentWave].enemySpawnSequences);
            }
        }

        public List<TOW_EnemyController> GetEnemiesInRange(Vector2 position, float range)
        {
            List<TOW_EnemyController> enemiesInRange = new List<TOW_EnemyController>();
            for (int i = 0; i < _enemyCount; i++)
            {
                TOW_EnemyController enemy = enemies[i];
                if (Vector2.Distance(position, enemy.transform.position) <= range)
                {
                    enemiesInRange.Add(enemy);
                }
            }

            return enemiesInRange;
        }

        public void OnEnemySpawned(TOW_EnemyController enemy)
        {
            if (enemies.Count > _enemyCount)
            {
                enemies[_enemyCount] = enemy;
            }
            else
            {
                enemies.Add(enemy);
            }

            _enemyCount++;
        }

        public void OnEnemyDefeated(TOW_EnemyController enemy)
        {
            for (int i = 0; i < _enemyCount; i++)
            {
                if (i != _enemyCount - 1 && enemies[i] == enemy)
                {
                    enemies[i] = enemies[_enemyCount - 1];
                }
            }

            _enemyCount -= 1;
        }
    }
}