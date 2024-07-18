using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public bool isRotateX = false;
    public bool isRotateY = false;
    public bool isRotateZ = false;

    void Update()
    {
        Vector3 rotation = Vector3.zero;

        if (isRotateX)
        {
            rotation.x = rotationSpeed * Time.deltaTime;
        }

        if (isRotateY)
        {
            rotation.y = rotationSpeed * Time.deltaTime;
        }

        if (isRotateZ)
        {
            rotation.z = rotationSpeed * Time.deltaTime;
        }

        transform.Rotate(rotation);
    }
}