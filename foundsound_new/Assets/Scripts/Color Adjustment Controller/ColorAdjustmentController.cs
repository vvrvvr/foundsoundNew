using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorAdjustmentController : MonoBehaviour
{
    public GameObject globalVolume; // Перетащите сюда Global Volume в инспекторе
    public float transitionSpeed = 1f; // Скорость перехода

    private Volume volume;
    private ColorAdjustments colorAdjustments;
    private float targetSaturation;
    private bool isTransitioning = false;

    private void Start()
    {
        // Получаем компонент Volume из Global Volume
        volume = globalVolume.GetComponent<Volume>();

        // Проверяем, есть ли в Volume эффект Color Adjustments
        if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            // Устанавливаем начальные значения без изменений
            targetSaturation = colorAdjustments.saturation.value;
            colorAdjustments.colorFilter.overrideState = false;
        }
    }

    private void Update()
    {
        if (colorAdjustments != null && isTransitioning)
        {
            // Плавное изменение насыщенности
            colorAdjustments.saturation.value = Mathf.Lerp(colorAdjustments.saturation.value, targetSaturation, Time.deltaTime * transitionSpeed);

            // Проверка завершения перехода
            if (Mathf.Abs(colorAdjustments.saturation.value - targetSaturation) < 0.1f)
            {
                colorAdjustments.saturation.value = targetSaturation;
                isTransitioning = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Когда игрок заходит в куб
            targetSaturation = 100f;
            colorAdjustments.colorFilter.overrideState = true;
            isTransitioning = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Когда игрок выходит из куба
            targetSaturation = -100f;
            colorAdjustments.colorFilter.overrideState = false;
            isTransitioning = true;
        }
    }
}