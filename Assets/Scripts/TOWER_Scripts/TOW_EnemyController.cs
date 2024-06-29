using TOWER.Components;
using UnityEngine;

namespace TOWER
{
    [RequireComponent(typeof(Rigidbody2D),typeof(TOW_HealthComponent))]
    public class TOW_EnemyController : MonoBehaviour
    {
        public Rigidbody2D physicComponent;
        public TOW_HealthComponent healthComponent;

        [Header("Attack")] public int damage = 1;

        public float attackRate = 2f;
        private float _attackTimer;
        public float velocity = 100f;
        public Transform target;

        public void Initialize(Transform targetedTransform)
        {
            target = targetedTransform;
        }
        private void Awake()
        {
            healthComponent.onDefeated.AddListener(Death);
        }

        private void FixedUpdate()
        {
            Vector2 movementDirection = target.position - transform.position;
            if (movementDirection.magnitude > 1f)
            {
                physicComponent.AddForce(movementDirection.normalized * velocity, ForceMode2D.Force);
            }
            else
            {
                _attackTimer += Time.deltaTime;
                if (_attackTimer >= attackRate)
                {
                    Attack();
                }
            }
        }

        private void Attack()
        {
            _attackTimer = 0f;
            target.gameObject.GetComponent<TOW_HealthComponent>()?.Damage(damage);
        }

        private void Death()
        {
            TOW_GameManager.Instance.OnEnemyDefeated(this);
            Destroy(gameObject);
        }
    }
}