using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    private static PersistentObject instance;

    void Awake()
    {
        // Проверяем, существует ли уже экземпляр этого объекта
        if (instance != null)
        {
            // Если экземпляр существует и это не текущий объект, удаляем текущий объект
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // Если экземпляр не существует, сохраняем текущий объект
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}