using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TOWER
{
    public class TOW_TowerController : MonoBehaviour
{
    [Header("Attack setup")]
    public TOW_ProjectileController projectilePrefab;
    [SerializeField]
    private Transform _projectilesHolder;
    public float shootRate = 1f;
    public int projectileDamage = 1;
    public float detectionRange = 3f;
    public string targetTag = "";
    
    [Header("XP info")]
    public int xpRate = 1;

    public int levelThreshold = 10;

    private float _shootTimer;
    private float _xpPoints;
    private bool _isActive;
    private int _currentLevel;
    
    // Update is called once per frame
    void Update()
    {
        _shootTimer += Time.deltaTime;
        if (_shootTimer >= shootRate)
        {
            SpawnProjectile();
            _shootTimer = 0f;
        }
        
        if (_isActive)
        {
            _xpPoints += xpRate * Time.deltaTime;
            if (_xpPoints >= levelThreshold)
            {
                _currentLevel += 1;
                levelThreshold += (int)(levelThreshold * 1.2f);
            }
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
            
            int closestTarget = 0;
            float closestDistance = Vector2.Distance(transform.position, enemiesInRange[closestTarget].transform.position);
            for (int i = 1; i < enemiesInRange.Count; i++)
            {
                float distance = Vector2.Distance(transform.position, enemiesInRange[i].transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = i;
                }
            }
            
            projectile.Initialize(enemiesInRange[closestTarget].transform, detectionRange, projectileDamage, targetTag);
            _isActive = true;
        }
        else
        {
            _isActive = false;
        }
    }
}
}
