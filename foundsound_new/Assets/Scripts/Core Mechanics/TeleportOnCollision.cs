using UnityEngine;

public class TeleportOnCollision : MonoBehaviour
{
    public Transform teleportPosition;
    // Заданные координаты для телепортации

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, столкнулись ли с объектом, на который должны реагировать
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject;
            // Телепортируем игрока
            TeleportPlayer(player);
        }
    }

    void TeleportPlayer(GameObject obj)
    {
        // Телепортируем игрока на заданные координаты
        obj.transform.position = teleportPosition.position;
    }
}
