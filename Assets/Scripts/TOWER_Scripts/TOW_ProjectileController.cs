using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOWER
{
    public class TOW_ProjectileController : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D Rb;
    public float Velocity = 100f;
    private Vector2 m_MovementDirection = Vector2.zero;

    [Header("Attack")]
    public int Damage = 1;

    private float m_LifeRange = 1f;
    private Vector2 m_SpawnedPosition;

    private void Awake()
    {
        m_SpawnedPosition = transform.position;
    }

    public void Initialize(Vector2 movementDirection, float lifeRange)
    {
        m_MovementDirection = movementDirection;
        m_LifeRange = lifeRange;
    }

    private void FixedUpdate()
    {
        Rb.AddForce(m_MovementDirection * Velocity, ForceMode2D.Force);
        
        m_LifeRange -= Time.deltaTime;
        if (m_LifeRange <= Vector2.Distance(transform.position, m_SpawnedPosition))
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger with: " + col.gameObject);
        if (col.CompareTag("Enemy"))
        {
            TOW_EnemyController enemyController = col.gameObject.GetComponent<TOW_EnemyController>();
            enemyController.ReceiveDamage(Damage);
            Destroy(gameObject);
        }
    }
}
}
