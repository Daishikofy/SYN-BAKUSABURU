using TOWER.Components;
using UnityEngine;

namespace TOWER
{
    public class TOW_ProjectileController : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D physicComponent;
    public float velocity = 100f;
    private Vector2 _movementDirection = Vector2.zero;
    
    private int _damage = 1;
    private string _targetTag = "";

    private float _lifeRange = 1f;
    private Vector2 _spawnedPosition;

    private void Awake()
    {
        _spawnedPosition = transform.position;
    }

    public void Initialize(Vector2 movementDirection, float lifeRange, int damage, string targetTag)
    {
        _movementDirection = movementDirection;
        _lifeRange = lifeRange;
        _damage = damage;
        _targetTag = targetTag;
    }

    private void FixedUpdate()
    {
        physicComponent.AddForce(_movementDirection * velocity, ForceMode2D.Force);
        
        _lifeRange -= Time.deltaTime;
        if (_lifeRange <= Vector2.Distance(transform.position, _spawnedPosition))
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger with: " + col.gameObject);
        if (col.CompareTag(_targetTag))
        {
            col.gameObject.GetComponent<TOW_HealthComponent>()?.Damage(_damage);
            Destroy(gameObject);
        }
    }
}
}
