using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EnemyTracking : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    [SerializeField] private bool IsInCombat;
    [SerializeField] private Vector2 OffsetA;
    [SerializeField] private Vector2 OffsetB;
    [SerializeField] private LayerMask TargetLayer;

    [SerializeField] private float AggressionTimer;
    [SerializeField] private float TimeLeft;
    private Vector2 pointA;
    private Vector2 pointB;
    [SerializeField] private float Speed;
    [SerializeField] private float hf = 0.0f;
    [SerializeField] private bool flipped;
    private Rigidbody2D rb;
    [SerializeField] private Vector2 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        TimeLeft = AggressionTimer;
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pointA = new Vector2(transform.position.x + OffsetA.x, transform.position.y + OffsetA.y);
        pointB = new Vector2(transform.position.x + OffsetB.x, transform.position.y + OffsetB.y);
        Collider2D targetCollider = Physics2D.OverlapArea(pointA, pointB, TargetLayer);
        if(targetCollider != null)
        {
            Debug.Log("player found!");
            IsInCombat = true;
            TimeLeft = AggressionTimer;
        }

        if (IsInCombat)
        {
            TimeLeft -= Time.deltaTime;
            hf = transform.position.x < Target.transform.position.x ? 1 : -1;
            rb.velocity = new Vector2(hf * Speed, GetComponent<Rigidbody2D>().velocity.y);
            if (hf == 1 && flipped)
            {
                flip();
            }
            if (hf == -1 && !flipped)
            {
                flip();
            }

            if (TimeLeft <= 0)
            {
                IsInCombat = false;
            }

        }
    }
    private void flip()
    {
        Vector3 currentscale = gameObject.transform.localScale;
        currentscale.x *= -1;
        gameObject.transform.localScale = currentscale;
        OffsetA.x = OffsetA.x * -1;
        OffsetB.x = OffsetB.x * -1;
        flipped = !flipped;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pointA, pointB);

    }
    internal void Respawn()
    {
        IsInCombat = false;
        TimeLeft = AggressionTimer;
        transform.position = spawnPoint;
    }
}
