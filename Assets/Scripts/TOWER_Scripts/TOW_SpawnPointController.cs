using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace TOWER
{
    public class TOW_SpawnPointController : MonoBehaviour
    {
        public Transform target;
        
        private TOW_SpawnSequence[] _sequence;
        
        private float _spawnTimer;
        private int _currentItem;
        private int _currentSequence;
        
        public void SetupWave(TOW_SpawnSequence[] spawnSequences)
        {
            _sequence = spawnSequences;
            _currentItem = 0;
            _currentSequence = 0;
            _spawnTimer = 0f;
        }
        
        private void Update()
        {
            if (_currentSequence < _sequence.Length)
            {
                _spawnTimer += Time.deltaTime;
                if (_spawnTimer >= _sequence[_currentSequence].spawnRate)
                {
                    if (_currentItem < _sequence[_currentSequence].amount)
                    {
                        Spawn();
                        _currentItem++;
                        _spawnTimer = 0f;
                    }
                    else
                    {
                        _currentSequence++;
                        _currentItem = 0;
                    }
                }
            }
        }

        private void Spawn()
        {
            TOW_EnemyController enemy = Instantiate(_sequence[_currentSequence].enemyPrefab, 
                    transform.position, 
                    quaternion.identity, 
                    transform);
                
                enemy.Initialize(target);
                TOW_GameManager.Instance.OnEnemySpawned(enemy);
        }
    }
}