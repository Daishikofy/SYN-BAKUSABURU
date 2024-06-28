using System;
using UnityEngine;
using UnityEngine.Events;

namespace TOWER.Components
{
    [Serializable]
    public class TOW_HealthComponent : MonoBehaviour
    {
        public int maxHealth = 10;
        public int CurrentHealth { get; private set; }
        
        [Header("Callbacks")]
        public UnityEvent onDefeated;

        public TOW_HealthComponent()
        {
            CurrentHealth = maxHealth;
        }

        public void Damage(int damages)
        {
            if (IsAlive())
            {
                CurrentHealth -= damages;
                if (CurrentHealth <= 0)
                {
                    onDefeated.Invoke();
                }
            }
        }

        public bool IsAlive()
        {
            return CurrentHealth > 0;
        }
    }
}