using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class RecordManager : MonoBehaviour
{
    // Структура для хранения данных
    [System.Serializable]
    public class ObjectData
    {
        public string word; // Слово
        public GameObject obj; // Соответствующий объект
        public int status; // Статус 0 или 1
        public string info; // Новое поле для дополнительной информации
        public float timestamp; // Время обновления статуса

        public ObjectData(string word, GameObject obj, string info)
        {
            this.word = word;
            this.obj = obj;
            this.status = 0; // По умолчанию 0
            this.info = info; // Дополнительная информация
            this.timestamp = 0; // Инициализация времени обновления
        }
    }

    public List<ObjectData> objectsData = new List<ObjectData>();

    public List<TextMeshProUGUI> textMeshProObjects; // Список ссылок на компоненты TextMeshPro

    void Start()
    {
        // Пример добавления данных
        objectsData.Add(new ObjectData("Игрушечный робот", /*GameObject ссылка на объект*/ null, "Это игрушечный робот"));
        objectsData.Add(new ObjectData("Поезд", /*GameObject ссылка на объект*/ null, "Это игрушечный поезд"));
        objectsData.Add(new ObjectData("Крушение самолета", /*GameObject ссылка на объект*/ null, "Это крушение самолета"));
    }

    void Update()
    {
        bool textUpdated = false;

        // Проверка состояния аудио у каждого объекта
        foreach (var data in objectsData)
        {
            if (data.obj != null)
            {
                AudioSource audioSource = data.obj.GetComponent<AudioSource>();
                if (audioSource != null && !audioSource.isPlaying && data.status == 0)
                {
                    data.status = 1; // Обновляем статус
                    data.timestamp = Time.time; // Записываем время обновления
                    textUpdated = true; // Помечаем, что нужно обновить текст
                }
            }
        }

        // Если было обновление текста, обновляем его в TextMeshPro
        if (textUpdated)
        {
            UpdateTextMeshPro();
        }
    }

    void UpdateTextMeshPro()
    {
        int textIndex = 0;

        // Сортируем данные по времени обновления
        var sortedData = objectsData.Where(data => data.status == 1).OrderBy(data => data.timestamp);

        // Заполняем текстовые объекты поочередно
        foreach (var data in sortedData)
        {
            if (textIndex < textMeshProObjects.Count)
            {
                textMeshProObjects[textIndex].text = data.word + ": " + data.info;
                textIndex++;
            }
        }

        // Если не хватило активных данных для заполнения всех TMP объектов, очищаем оставшиеся
        for (int i = textIndex; i < textMeshProObjects.Count; i++)
        {
            textMeshProObjects[i].text = "";
        }
    }
}

