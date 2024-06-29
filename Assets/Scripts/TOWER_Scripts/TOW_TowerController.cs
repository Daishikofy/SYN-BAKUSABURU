using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TOWER
{
    public class TOW_TowerController : MonoBehaviour
{
    public TOW_ProjectileController projectilePrefab;
    [SerializeField]
    private Transform _projectilesHolder;
    public float shootRate = 1f;
    public int projectileDamage = 1;
    public float detectionRange = 3f;
    public string targetTag = "";

    private float _shootTimer;
    [SerializeField]
    

    // Update is called once per frame
    void Update()
    {
        _shootTimer += Time.deltaTime;
        if (_shootTimer >= shootRate)
        {
            SpawnProjectile();
            _shootTimer = 0f;
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
        foreach (Transform projectile in _projectilesHolder)
        {
            Destroy(projectile.gameObject);
        }
    }

    private void SpawnProjectile()
    {
        //Todo: Pooling system
        Transform parentTransform = transform;
        List<TOW_EnemyController> enemiesInRange =
            TOW_GameManager.Instance.GetEnemiesInRange(parentTransform.position, detectionRange);
        if (enemiesInRange.Count > 0)
        {
            TOW_ProjectileController projectile = Instantiate(projectilePrefab, parentTransform.position, Quaternion.identity, _projectilesHolder);
            Vector2 projectileDirection = enemiesInRange[Random.Range(0, enemiesInRange.Count)].transform.position -
                                          transform.position;
            projectile.Initialize(projectileDirection, detectionRange, projectileDamage, targetTag);
        }
    }
}
}
