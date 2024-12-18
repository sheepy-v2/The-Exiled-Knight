using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private double maxHealth;
    [SerializeField] private double currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) 
        { 
            transform.position = new Vector2(0, -50);
        }
    }
    internal void Hit(double damageTaken)
    {
        currentHealth -= damageTaken;
    }
    internal void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
