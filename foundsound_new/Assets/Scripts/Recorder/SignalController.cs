using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class SignalController : MonoBehaviour
{
    public Transform playerCamera;
    public List<Transform> emitersToAdd;
    public List<Transform> emiters;// изменение массива на список
    public float signalForce = 1f;
    public TextMeshProUGUI signalText;
    public float maxSignalDistance = 10f;
    public float signalImpactValue = 0.5f;
    public float distanceImpactValue = 0.5f;
    
    [Space(10)] 
    public HealthBar signalBar;
    
    private float maxSignal = 0f;
    public float delayTime = 0.3f;
    private Coroutine addingEmiter;
    
    void Update()
    {
        maxSignal = 0f;
        
        HandleEmiters();
        
        signalText.SetText("Signal: " + maxSignal.ToString("F2"));
        signalBar.SetSignal(maxSignal);
    }
    
    private void HandleEmiters()
    {
        if(emiters.Count == 0)
            return;
        
        foreach (Transform locator in emiters)
        {
            Vector3 directionToLocator = locator.position - playerCamera.position;
            float distanceToLocator = directionToLocator.magnitude;
            directionToLocator.Normalize();
            Vector3 cameraForward = playerCamera.forward;
            float angle = Vector3.Angle(cameraForward, directionToLocator) / 90f;
            
            float signalImpact = Mathf.Clamp01(1 - angle * signalForce);
            signalImpact *= signalImpactValue;
            
            // Вычисляем влияние расстояния
            float distanceFactor = 1f - Mathf.Clamp01(distanceToLocator / maxSignalDistance);
            float distanceImpact = Mathf.Clamp01(distanceImpactValue * distanceFactor);
            
            float signal = distanceImpact + signalImpact;
            

            if (signal > maxSignal)
            {
                maxSignal = signal;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("emiter"))
        {
            // Добавляем эмиттер в список emitersToAdd
            if (!emitersToAdd.Contains(other.transform))
            {
                emitersToAdd.Add(other.transform);
                addingEmiter = StartCoroutine(AddEmiterAfterDelay(other.transform));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("emiter"))
        {
            RemoveEmiter(other.transform);
        }
    }

    // Метод для удаления эмиттера из списка по атрибуту
    public void RemoveEmiter(Transform emiterToRemove)
    {
        if (emitersToAdd.Contains(emiterToRemove))
        {
            StopCoroutine(addingEmiter);
            emitersToAdd.Remove(emiterToRemove);
               
                
            Debug.Log("Unregistered add: " + emiterToRemove.name);
        }
        if (emiters.Contains(emiterToRemove))
        {
            emiters.Remove(emiterToRemove);
            Debug.Log("Unregistered: " + emiterToRemove.name);
        }
    }

    void OnDisable()
    {
        if (addingEmiter != null)
        {
            StopCoroutine(addingEmiter);
        }
        emiters.Clear();
        emitersToAdd.Clear();
        
    }
    
    private IEnumerator AddEmiterAfterDelay(Transform emiter)
    {
        yield return new WaitForSeconds(delayTime);

        // Проверяем, существует ли переданный эмиттер ещё в списке emitersToAdd
        if (emitersToAdd.Contains(emiter))
        {
            // Если существует, проверяем, есть ли он уже в списке emiters
            if (!emiters.Contains(emiter))
            {
                // Если не существует, добавляем его в список emiters
                emiters.Add(emiter);
                Debug.Log("Registered: " + emiter.name);
            }
            emitersToAdd.Remove(emiter);
        }
    }
    private IEnumerator RemoveEmiterAfterDelay(Transform emiter)
    {
        yield return new WaitForSeconds(delayTime);

        // Проверяем, существует ли переданный эмиттер ещё в списке emitersToAdd
        if (emitersToAdd.Contains(emiter))
        {
            // Если существует, проверяем, есть ли он уже в списке emiters
            if (!emiters.Contains(emiter))
            {
                // Если не существует, добавляем его в список emiters
                emiters.Add(emiter);
                emitersToAdd.Remove(emiter);
                Debug.Log("Registered: " + emiter.name);
            }
        }
    }
}
