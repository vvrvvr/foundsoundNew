using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextureSwitcher : MonoBehaviour
{
    public Texture[] textures; // Массив текстур для анимации
    public Texture originalTexture; // Исходная текстура
    public float switchInterval = 0.1f; // Интервал между переключениями текстур

    private Renderer rend;
    private int currentIndex = 0;
    private int direction = 1; // Направление анимации: 1 - вперед, -1 - назад
    public static bool isActivated = false; // Флаг для определения состояния объекта (включено/выключено)
    private Coroutine animationCoroutine; // Ссылка на корутину анимации

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.mainTexture = originalTexture; // Устанавливаем исходную текстуру при старте
    }

    void Update()
    {
        // Проверяем нажатие левой кнопки мыши для переключения состояния объекта
        if (Input.GetMouseButtonDown(0))
        {
            isActivated = !isActivated; // Переключаем состояние

            if (isActivated)
            {
                // Если объект включен, начинаем анимацию
                animationCoroutine = StartCoroutine(SwitchTextures());
            }
            else
            {
                // Если объект выключен, возвращаем исходную текстуру
                StopAllCoroutines();
                rend.material.mainTexture = originalTexture;
            }
        }
    }

    IEnumerator SwitchTextures()
    {
        while (isActivated)
        {
            // Увеличиваем или уменьшаем индекс в зависимости от направления
            currentIndex += direction;

            // Если достигнут конец или начало массива текстур, меняем направление
            if (currentIndex == 0 || currentIndex == textures.Length - 1)
                direction *= -1;

            // Обеспечиваем, чтобы индекс находился в пределах массива текстур
            currentIndex = Mathf.Clamp(currentIndex, 0, textures.Length - 1);

            // Присваиваем текстуру
            rend.material.mainTexture = textures[currentIndex];

            yield return new WaitForSeconds(switchInterval);
        }
    }
}
