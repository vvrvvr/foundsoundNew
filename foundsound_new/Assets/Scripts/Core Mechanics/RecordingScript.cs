using UnityEngine;

public class RecordingScript : MonoBehaviour
{
    private AudioSource audioSource; // Ссылка на компонент AudioSource
    private bool isFadingOut = false; // Флаг для отслеживания плавного понижения громкости
    private float fadeOutDuration = 1.0f; // Время плавного понижения громкости (в секундах)
    private float fadeOutStartTime; // Время начала плавного понижения громкости
    private float initialVolume; // Начальная громкость звука
    public GameObject recorderObject; // Ссылка на объект Recorder
    public float clickRadius = 10f; // Радиус области клика

    void Start()
    {
        // Получаем компонент AudioSource на этом объекте
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource не найден на объекте " + gameObject.name);
        }
        else
        {
            initialVolume = audioSource.volume; // Сохраняем начальную громкость звука
        }
    }

    void Update()
    {
        // Проверяем клик левой кнопкой мыши
        if (Input.GetMouseButtonDown(0))
        {
            // Проверяем, что объект Recorder активен
            if (recorderObject != null && recorderObject.activeSelf)
            {
                // Проверяем, что объект находится в центральной области экрана
                Vector3 viewportPoint = Camera.main.WorldToViewportPoint(transform.position);
                bool isInCentralArea = Mathf.Abs(viewportPoint.x - 0.5f) <= 0.25f && Mathf.Abs(viewportPoint.y - 0.5f) <= 0.25f;

                // Проверяем расстояние до объекта
                if (isInCentralArea && Vector3.Distance(transform.position, Camera.main.transform.position) <= clickRadius)
                {
                    // Проверяем состояние isActivated из другого скрипта
                    if (TextureSwitcher.isActivated)
                    {
                        // Начинаем плавное понижение громкости
                        isFadingOut = true;
                        fadeOutStartTime = Time.time;
                        Debug.Log("Начато плавное понижение громкости на объекте " + gameObject.name);
                    }
                }
            }
        }

        // Плавное понижение громкости
        if (isFadingOut)
        {
            float elapsed = Time.time - fadeOutStartTime;
            float progress = elapsed / fadeOutDuration;

            // Плавно понижаем громкость звука
            audioSource.volume = Mathf.Lerp(initialVolume, 0f, progress);

            // Если понижение завершено, отключаем звук и сбрасываем состояние
            if (progress >= 1.0f)
            {
                audioSource.Stop();
                isFadingOut = false;
                Debug.Log("Звук плавно отключен на объекте " + gameObject.name);
            }
        }
    }
}

