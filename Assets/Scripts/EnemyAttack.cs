using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject AttackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float damage;
    [SerializeField] private float cooldown;
    [SerializeField] private float timeleft;
    [SerializeField] private bool canAttack;
    // Start is called before the first frame update
    void Start()
    {
        timeleft = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeleft < 0)
        {
            canAttack = true;
        }
        else if(timeleft >= 0)
        {
            timeleft -= Time.deltaTime;
        }

        if (canAttack)
        {
            Collider2D targetCollider = Physics2D.OverlapCircle(AttackPoint.transform.position, radius, targetLayer);
            if (targetCollider != null)
            {
                targetCollider.GetComponent<PlayerStats>().Hit(damage);
            }
            timeleft = cooldown;
            canAttack = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AttackPoint.transform.position, radius);
    }
}
