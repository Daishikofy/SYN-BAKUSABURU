using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOWER
{
    public class TOW_EnemyController : MonoBehaviour
    {
        public int MaxLife = 3;
        private int m_CurrentLife;

        private void Awake()
        {
            m_CurrentLife = MaxLife;
        }

        public void ReceiveDamage(int damage)
        {
            m_CurrentLife -= damage;
            if (m_CurrentLife <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            TOW_GameManager.Instance.OnEnemyDefeated(this);
            Destroy(gameObject);
        }
    }
}
