using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    // Переменные для установки скоростей вращения по осям в редакторе Unity
    public float rotationSpeedX = 0f;
    public float rotationSpeedY = 100f;
    public float rotationSpeedZ = 0f;

    // Update вызывается один раз за кадр
    void Update()
    {
        // Вращение объекта вокруг осей X, Y и Z
        float rotationX = rotationSpeedX * Time.deltaTime;
        float rotationY = rotationSpeedY * Time.deltaTime;
        float rotationZ = rotationSpeedZ * Time.deltaTime;

        transform.Rotate(rotationX, rotationY, rotationZ);
    }
}
