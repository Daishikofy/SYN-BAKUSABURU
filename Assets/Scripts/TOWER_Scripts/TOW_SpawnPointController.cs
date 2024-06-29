using System;
using Unity.Mathematics;
using UnityEngine;

namespace TOWER
{
    public class TOW_SpawnPointController : MonoBehaviour
    {
        public TOW_EnemyController[] spawnPrefabs;
        public float spawnRate = 5f;
        public Transform target;
        private float _spawnTimer;
        private int _currentPrefab;

        private void Update()
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer >= spawnRate)
            {
                _spawnTimer = 0f;
                Spawn();
            }
        }

        private void Spawn()
        {
            if (_currentPrefab < spawnPrefabs.Length)
            {
                TOW_EnemyController enemy = Instantiate(spawnPrefabs[_currentPrefab], transform.position, quaternion.identity, transform);
                enemy.Initialize(target);
                TOW_GameManager.Instance.OnEnemySpawned(enemy);
                _currentPrefab++;
            }
        }
    }
}