using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

namespace TOWER
{
public class TOW_PlayerContoller : MonoBehaviour
{
    //Movement variables
    [Header("Movement Variables")]
    public Rigidbody2D Rb;
    public float Velocity = 10f;

    private Vector2 m_movementDirection = Vector2.zero;

    [Header("Attack Variables")] 
    public List<TOW_TowerController> Towers;
    private List<int> m_AvailableTowers;
    private List<int> m_PlacedTowers;
    public float RangeToGrabTower = 0.25f;
    
    // Start is called before the first frame update
    void Start()
    {
        m_PlacedTowers = new List<int>();
        m_AvailableTowers = new List<int>();
        for (int i = 0; i < Towers.Count; i++)
        {
            m_AvailableTowers.Add(i);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rb.AddForce(m_movementDirection * Velocity, ForceMode2D.Force);
    }
    
    public void Move(InputAction.CallbackContext context) //Input system
    {
        Vector2 input = context.ReadValue<Vector2>();
        m_movementDirection = input;
    }
    
    public void Attack(InputAction.CallbackContext context) //Input system
    {
        if (context.phase == InputActionPhase.Performed)
        {
            for (int i = 0; i < m_PlacedTowers.Count; i++)
            {
                int towerIndex = m_PlacedTowers[i];
                TOW_TowerController tower = Towers[m_PlacedTowers[i]];
                
                if (Vector2.Distance(tower.transform.position, transform.position) <= RangeToGrabTower)
                {
                    tower.DisableTower();
                    m_AvailableTowers.Add(towerIndex);
                    
                    m_PlacedTowers.RemoveAt(i);
                    return;
                }
            }

            if (m_AvailableTowers.Count > 0)
            {
                int towerIndex = m_AvailableTowers[0];
                TOW_TowerController tower = Towers[m_AvailableTowers[0]];
                m_PlacedTowers.Add(towerIndex);
                tower.EnableTower(transform.position);
                
                m_AvailableTowers.RemoveAt(0);
            }
        }
    }

}
}
