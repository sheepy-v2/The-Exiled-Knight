using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private bool works = false;
    [SerializeField] private float weworking = 0;
    [SerializeField] private GameObject PopUpPrefab;
    [SerializeField] private Vector2 PopUpOffset;
    private Vector2 PopUpPos;
    private GameObject PopUp;
    private bool popUpMade;
    // Start is called before the first frame update
    void Start()
    {
        PopUpPos = new Vector2(transform.position.x + PopUpOffset.x, transform.position.y + PopUpOffset.y);
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
                RespawnAllEnemies();
                UnityEngine.Debug.Log($"eyyyy het werktttttt");
            }
            if(popUpMade == false)
            {
                PopUp = Instantiate(PopUpPrefab, new Vector3(PopUpPos.x, PopUpPos.y, -10), transform.rotation);
                popUpMade = true;
            }
            
            
        }
        else if(popUpMade == true)
        {
            Destroy(PopUp);
            popUpMade= false;
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
    internal void RespawnAllEnemies()
    {
       
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyStats>().ResetHealth();
            enemy.GetComponent<EnemyTracking>().Respawn();
            UnityEngine.Debug.Log($"{enemy.name} found!");
        }
    }
    private List<GameObject> GetEnemiesOnLayer(int layer)
    {
        List<GameObject> result = new List<GameObject>();

        GameObject[] allObjects = FindObjectsOfType<GameObject>(true); // true = include inactive

        foreach (GameObject obj in allObjects)
        {
            UnityEngine.Debug.Log($"Object {obj.name} is on layer {obj.layer}");
            if (obj.layer == layer) // Check the layer
            {
                result.Add(obj);
            }
        }


        return result;
    }
}
