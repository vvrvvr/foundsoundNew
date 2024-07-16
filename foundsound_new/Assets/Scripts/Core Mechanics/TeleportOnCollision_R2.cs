using UnityEngine;

public class TeleportOnCollision_R2 : MonoBehaviour
{
    public Vector3 teleportPosition; // Заданные координаты для телепортации

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, столкнулись ли с объектом, на который должны реагировать
        if (other.gameObject.CompareTag("TeleportTrigger_2"))
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
