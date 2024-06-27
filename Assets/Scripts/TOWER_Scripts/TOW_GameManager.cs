using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace TOWER
{
    public class TOW_GameManager : MonoBehaviour
    {
        private static TOW_GameManager m_Instance;
        public static TOW_GameManager Instance => m_Instance;

        public TOW_EnemyController[] Enemies;
        private int m_EnemyCount;

        private void Awake()
        {
            if (m_Instance != null)
            {
                Destroy(this);
            }
            else
            {
                m_Instance = this;
            }

            m_EnemyCount = Enemies.Length;
        }

        public List<TOW_EnemyController> GetEnemiesInRange(Vector2 position, float range)
        {
            List<TOW_EnemyController> enemiesInRange = new List<TOW_EnemyController>();
            for (int i = 0; i < m_EnemyCount; i++)
            {
                TOW_EnemyController enemy = Enemies[i];
                if (Vector2.Distance(position, enemy.transform.position) <= range)
                {
                    enemiesInRange.Add(enemy);
                }
            }

            return enemiesInRange;
        }

        public void OnEnemyDefeated(TOW_EnemyController enemy)
        {
            for (int i = 0; i < m_EnemyCount; i++)
            {
                if (i != m_EnemyCount - 1 && Enemies[i] == enemy )
                {
                    Enemies[i] = Enemies[m_EnemyCount - 1];
                }
            }
            m_EnemyCount -= 1;
        }
    }
}
