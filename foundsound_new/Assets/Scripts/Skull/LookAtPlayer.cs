using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player; // Ссылка на объект игрока
    public float yRotationOffset = 0f; // Корректирующее значение для поворота по оси Y

    private float initialRotationX;
    private float initialRotationZ;

    void Start()
    {
        // Сохраняем начальные значения углов по осям X и Z
        initialRotationX = transform.eulerAngles.x;
        initialRotationZ = transform.eulerAngles.z;
    }

    void Update()
    {
        if (player != null)
        {
            // Вычисляем направление к игроку
            Vector3 direction = player.position - transform.position;

            // Обнуляем компонент по оси Y, чтобы объект не наклонялся вверх или вниз
            direction.y = 0;

            // Проверяем, есть ли направление, чтобы избежать ошибки нулевого вектора
            if (direction != Vector3.zero)
            {
                // Вычисляем вращение, чтобы объект смотрел на игрока
                Quaternion rotation = Quaternion.LookRotation(direction);

                // Ограничиваем вращение только по оси Y, сохраняя начальные значения по X и Z
                Vector3 eulerRotation = rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(initialRotationX, eulerRotation.y + yRotationOffset, initialRotationZ);
            }
        }
    }
}
