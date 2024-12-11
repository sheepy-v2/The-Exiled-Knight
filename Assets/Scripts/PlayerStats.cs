using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float strength;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float defense;
    [SerializeField] private GameObject Shrine;

    private Animator anim;
    private bool isDead = false;
        
    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            if (isDead == false)
            {
                anim.SetTrigger("die");
                isDead = true;
            }
            
        }
    }
    internal float GetStrength()
    {
        return strength;
    }
    internal float GetHealth()
    {
        return currentHealth;
    }
    internal void FullRestoreHealth()
    {
        currentHealth = maxHealth;
    }
    internal float GetMaxHealth()
    {
        return maxHealth;
    }
    internal void Died()
    {
        Respawn();
    }
    internal void SetShrineRespawn(GameObject shrine)
    {
        Shrine = shrine;
    }
    internal void Respawn()
    {
        currentHealth = maxHealth;
        transform.position = Shrine.transform.position;
        isDead = false;
    }
}