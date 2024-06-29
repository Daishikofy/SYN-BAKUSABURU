using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace TOWER
{
    public class TOW_SpawnPointController : MonoBehaviour
    {
        [SerializeField] 
        private TOW_EnemySpawnSequence[] wave;
        
        public Transform target;
        
        private float _spawnTimer;
        private int _currentItem;
        private int _currentSequence;

        public UnityEvent onWaveEnded = new UnityEvent();

        public void SetupWave(TOW_EnemySpawnSequence[] spawnSequences)
        {
            wave = spawnSequences;
            _currentItem = 0;
            _currentSequence = 0;
            _spawnTimer = 0f;
        }
        
        private void Update()
        {
            if (_currentSequence < wave.Length)
            {
                _spawnTimer += Time.deltaTime;
                if (_spawnTimer >= wave[_currentSequence].spawnRate)
                {
                    if (_currentItem < wave[_currentSequence].amount)
                    {
                        Spawn();
                        _currentItem++;
                        _spawnTimer = 0f;
                    }
                    else
                    {
                        _currentSequence++;
                        _currentItem = 0;
                        if (_currentSequence >= wave.Length)
                        {
                            onWaveEnded.Invoke();
                        }
                    }
                }
            }
        }

        private void Spawn()
        {
            TOW_EnemyController enemy = Instantiate(wave[_currentSequence].enemyPrefab, 
                    transform.position, 
                    quaternion.identity, 
                    transform);
                
                enemy.Initialize(target);
                TOW_GameManager.Instance.OnEnemySpawned(enemy);
        }
    }
}