using UnityEngine;

public class MoveObjectOnTrigger : MonoBehaviour
{
    public GameObject targetObject; // Объект, который нужно двигать
    public GameObject playerObject; // Игрок, который тоже должен двигаться
    public GameObject firstPersonAudioObject; // Объект FirstPersonAudio, который нужно отключить
    public float moveSpeed = 1f; // Скорость движения объекта
    private bool isPlayerInside = false;
    private float elapsedTime = 0f;
    private float duration = 100f; // Продолжительность движения

    private Vector3 initialTargetPosition;

    void Start()
    {
        initialTargetPosition = targetObject.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            elapsedTime = 0f; // Сбросить таймер при входе

            if (firstPersonAudioObject != null)
            {
                firstPersonAudioObject.SetActive(false);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            elapsedTime = 0f; // Сбросить таймер при выходе

            // Вернуть целевой объект на исходную позицию
            targetObject.transform.position = initialTargetPosition;

            if (firstPersonAudioObject != null)
            {
                firstPersonAudioObject.SetActive(true);
            }
        }
    }

    void Update()
    {
        if (isPlayerInside && elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            Vector3 movement = Vector3.forward * moveSpeed * Time.deltaTime; // Глобальные координаты вперед по оси Z
            targetObject.transform.Translate(movement, Space.World);
            playerObject.transform.Translate(movement, Space.World);
        }
    }
}


