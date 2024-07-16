using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public float oscillationSpeed = 1f; // скорость колебаний
    private float originalHealth; // изначальное значение health

    private void Start()
    {
        originalHealth = slider.value;
       
    }

    // пока не нужно
    // public void SetMaxHealth(int health) 
    // {
    //     slider.maxValue = health;
    //     slider.value = health;
    //     originalHealth = health; // обновляем изначальное значение
    //     fill.color = gradient.Evaluate(1f);
    // }

    public void SetSignal(float volume)
    {
        slider.value = volume + Random.Range(-0.02f, 0.02f); 
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

  
}