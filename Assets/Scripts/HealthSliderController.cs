using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderController : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private float currenthealth;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currenthealth = stats.GetHealth();
        float fillvalue = stats.GetHealth() / stats.GetMaxHealth();
        slider.value = fillvalue;
    }
}
