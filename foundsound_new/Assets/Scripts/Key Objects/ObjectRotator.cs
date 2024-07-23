using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    // Переменная для установки скорости вращения в редакторе Unity
    public float rotationSpeed = 100f;

    // Update вызывается один раз за кадр
    void Update()
    {
        // Вращение объекта вокруг оси Y
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
