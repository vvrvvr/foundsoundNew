using UnityEngine;

public class HideObjectToggle : MonoBehaviour
{
    public GameObject objectToHide;

    void Update()
    {
        // Проверяем, была ли нажата клавиша "1"
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Проверяем, существует ли объект для скрытия и включен ли он
            if (objectToHide != null && objectToHide.activeSelf)
            {
                // Если да, то выключаем его
                objectToHide.SetActive(false);
            }
            else
            {
                // Если нет или выключен, то включаем его
                objectToHide.SetActive(true);
            }
        }
    }
}