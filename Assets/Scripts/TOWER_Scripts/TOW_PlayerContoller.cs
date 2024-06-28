using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TOWER
{
public class TOW_PlayerContoller : MonoBehaviour
{
    //Movement variables
    [Header("Movement Variables")]
    public Rigidbody2D physicComponent;
    public float velocity = 10f;

    private Vector2 _movementDirection = Vector2.zero;

    [Header("Attack Variables")] 
    public List<TOW_TowerController> towers;
    private List<int> _availableTowers;
    private List<int> _placedTowers;
    public float rangeToGrabTower = 0.25f;
    
    // Start is called before the first frame update
    void Start()
    {
        _placedTowers = new List<int>();
        _availableTowers = new List<int>();
        for (int i = 0; i < towers.Count; i++)
        {
            _availableTowers.Add(i);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        physicComponent.AddForce(_movementDirection * velocity, ForceMode2D.Force);
    }
    
    public void Move(InputAction.CallbackContext context) //Input system
    {
        Vector2 input = context.ReadValue<Vector2>();
        _movementDirection = input;
    }
    
    public void Attack(InputAction.CallbackContext context) //Input system
    {
        if (context.phase == InputActionPhase.Performed)
        {
            for (int i = 0; i < _placedTowers.Count; i++)
            {
                int towerIndex = _placedTowers[i];
                TOW_TowerController tower = towers[_placedTowers[i]];
                
                if (Vector2.Distance(tower.transform.position, transform.position) <= rangeToGrabTower)
                {
                    tower.DisableTower();
                    _availableTowers.Add(towerIndex);
                    
                    _placedTowers.RemoveAt(i);
                    return;
                }
            }

            if (_availableTowers.Count > 0)
            {
                int towerIndex = _availableTowers[0];
                TOW_TowerController tower = towers[_availableTowers[0]];
                _placedTowers.Add(towerIndex);
                tower.EnableTower(transform.position);
                
                _availableTowers.RemoveAt(0);
            }
        }
    }

}
}
