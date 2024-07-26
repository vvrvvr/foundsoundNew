using UnityEngine;

public class OscillateRotation : MonoBehaviour
{
    // Начальная и конечная амплитуды углов вращения
    public float minAngle = 45f;
    public float maxAngle = 90f;

    // Скорость вращения
    public float speed = 1f;

    // Текущий угол
    private float currentAngle;

    // Направление вращения: true - вперед, false - назад
    private bool rotatingForward = true;

    void Update()
    {
        // Если вращаем вперед
        if (rotatingForward)
        {
            // Увеличиваем угол
            currentAngle += speed * Time.deltaTime;
            if (currentAngle > maxAngle)
            {
                currentAngle = maxAngle;
                rotatingForward = false;
            }
        }
        // Если вращаем назад
        else
        {
            // Уменьшаем угол
            currentAngle -= speed * Time.deltaTime;
            if (currentAngle < minAngle)
            {
                currentAngle = minAngle;
                rotatingForward = true;
            }
        }

        // Применяем вращение по оси Z
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentAngle));
    }
}
