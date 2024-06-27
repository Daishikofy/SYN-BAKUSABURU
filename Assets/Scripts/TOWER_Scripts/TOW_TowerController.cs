using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TOWER
{
    public class TOW_TowerController : MonoBehaviour
{
    public TOW_ProjectileController ProjectilePrefab;
    public float ShootRate = 1f;
    public float DetectionRange = 3f;

    private float m_ShootTimer = 0f;
    private List<TOW_ProjectileController> m_Projectiles;

    private void Start()
    {
        m_Projectiles = new List<TOW_ProjectileController>();
    }

    // Update is called once per frame
    void Update()
    {
        m_ShootTimer += Time.deltaTime;
        if (m_ShootTimer >= ShootRate)
        {
            SpawnProjectile();
            m_ShootTimer = 0f;
        }
    }

    public void EnableTower(Vector2 position)
    {
        gameObject.transform.position = position;
        gameObject.SetActive(true);
    }

    public void DisableTower()
    {
        gameObject.SetActive(false);
        foreach (TOW_ProjectileController projectile in m_Projectiles)
        {
            Destroy(projectile.gameObject);
        }
    }

    private void SpawnProjectile()
    {
        //Todo: Pooling system
        Transform parentTransform = transform;
        List<TOW_EnemyController> enemiesInRange =
            TOW_GameManager.Instance.GetEnemiesInRange(parentTransform.position, DetectionRange);

        if (enemiesInRange.Count > 0)
        {
            TOW_ProjectileController projectile = Instantiate(ProjectilePrefab, parentTransform.position, Quaternion.identity, parentTransform);
            Vector2 projectileDirection = enemiesInRange[Random.Range(0, enemiesInRange.Count)].transform.position -
                                          transform.position;
            projectile.Initialize(projectileDirection, DetectionRange);
        }
    }
}
}
