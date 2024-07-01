using System;
using System.Collections.Generic;
using UnityEngine;

namespace TOWER
{
    public class TOW_GameManager : MonoBehaviour
    {
        private static TOW_GameManager _instance;
        public static TOW_GameManager Instance => _instance;

        [Header("Managers")] 
        [SerializeField] private TOW_SpawnPointManager spawnPointManager;

        [Header("Waves parameters")]
        [SerializeField] private TOW_LevelScenario levelScenario;

        private int _currentWaveId;



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
        }

        private void Start()
        {
            StartPlayingLevel();
        }

        // _ _ _ _ _ LEVEL SCENARIO _ _ _ _ _ _
        private void StartPlayingLevel()
        {
            _currentWaveId = 0;
            spawnPointManager.InitializeCurrentWave(levelScenario.enemyWaves[_currentWaveId]);
        }

        public void OnWaveEnded()
        {
            _currentWaveId++;
            if (_currentWaveId < levelScenario.enemyWaves.Length)
            {
                spawnPointManager.InitializeCurrentWave(levelScenario.enemyWaves[_currentWaveId]);
            }
        }

        // _ _ _ _ _ ENEMIES _ _ _ _ _ _
        public List<TOW_EnemyController> GetEnemiesInRange(Vector2 position, float range)
        {
            return spawnPointManager.GetEnemiesInRange(position, range);
        }
        

        public void OnEnemyDefeated(TOW_EnemyController enemy)
        {
            spawnPointManager.OnEnemyDefeated(enemy);
        }
        
        public void OnEnemySpawned(TOW_EnemyController enemy)
        {
            spawnPointManager.OnEnemySpawned(enemy);
        }
    }
}