using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class Attack : MonoBehaviour
{  
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private float strength;
    [SerializeField] private float weaponDMG;

    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private bool isAttacking;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isAttacking = false;
        strength = gameObject.GetComponent<PlayerStats>().GetStrength();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetBool("attacking", true);
        }
        if (isAttacking)
        {
            // Create a HashSet to store enemies hit in this attack cycle
            HashSet<Collider2D> hitEnemies = new HashSet<Collider2D>();

            Gizmos.color = Color.red;
            double currDamage = (weaponDMG * (strength * 0.7)) * 0.8;
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemyLayer);

            foreach (Collider2D enemyGameObject in enemies)
            {
                if (!hitEnemies.Contains(enemyGameObject))
                {
                    enemyGameObject.gameObject.GetComponent<EnemyStats>().Hit(currDamage);
                    Debug.Log("hit enemy");

                    // Add the enemy to the HashSet to avoid hitting it again
                    hitEnemies.Add(enemyGameObject);
                }
            }
            Gizmos.color = Color.yellow;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
    internal void AttackBehaviour()
    {
        isAttacking = true;
    }
    internal void AttackEnd()
    {
        isAttacking = false;
        anim.SetBool("attacking", false);
    }
}
