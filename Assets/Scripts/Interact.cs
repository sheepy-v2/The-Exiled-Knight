using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private bool works = false;
    [SerializeField] private float weworking = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (works)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                weworking += 1;
                stats.FullRestoreHealth();
                stats.SetShrineRespawn(gameObject);
                UnityEngine.Debug.Log($"eyyyy het werktttttt");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision = playerCollider)
        {
            works = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision = playerCollider)
        {
            works = false;
        }
    }
}
