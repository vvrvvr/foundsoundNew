using UnityEngine;

public class LightMover : MonoBehaviour
{
    // Переменные для настройки амплитуды, скорости и начальной высоты
    public float amplitude = 1.5f; // Амплитуда движения (размах)
    public float speed = 1f; // Скорость движения
    public float initialHeight = 89.5f; // Начальная высота между 88 и 91

    // Внутренняя переменная для отслеживания времени
    private float timeCounter = 0f;

    void Start()
    {
        // Устанавливаем начальную высоту объекта
        Vector3 startPosition = transform.position;
        startPosition.y = initialHeight;
        transform.position = startPosition;
    }

    // Update вызывается один раз за кадр
    void Update()
    {
        // Обновляем таймер
        timeCounter += Time.deltaTime * speed;

        // Рассчитываем новое положение по оси Y с учетом амплитуды и начальной высоты
        float newY = initialHeight + Mathf.Sin(timeCounter) * amplitude;

        // Обновляем положение объекта
        Vector3 newPosition = transform.position;
        newPosition.y = newY;
        transform.position = newPosition;
    }
}
