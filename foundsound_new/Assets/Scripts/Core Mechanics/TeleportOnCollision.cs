using UnityEngine;

public class TeleportOnCollision : MonoBehaviour
{
    public Vector3 teleportPosition; // Заданные координаты для телепортации

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, столкнулись ли с объектом, на который должны реагировать
        if (other.gameObject.CompareTag("TeleportTrigger"))
        {
            // Телепортируем игрока
            TeleportPlayer();
        }
    }

    void TeleportPlayer()
    {
        // Телепортируем игрока на заданные координаты
        transform.position = teleportPosition;
    }
}
