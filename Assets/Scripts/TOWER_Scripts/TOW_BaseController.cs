using TOWER.Components;
using UnityEngine;

namespace TOWER
{
    [RequireComponent(typeof(TOW_HealthComponent))]
    public class TOW_BaseController : MonoBehaviour
    {
        [SerializeField]
        public TOW_HealthComponent healthComponent;
        
        private void Awake()
        {
            healthComponent.onDefeated.AddListener(OnDefeated);
        }

        private void OnDefeated()
        {
            Debug.Log("GAME OVER");
        }
    }
}