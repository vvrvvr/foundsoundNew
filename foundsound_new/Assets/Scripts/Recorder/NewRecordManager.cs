using UnityEngine;
using System.Collections.Generic;

public class NewRecordManager : MonoBehaviour
{
    // Синглтон
    public static NewRecordManager Instance { get; private set; }

    // Структура для хранения информации об объекте Record
    [System.Serializable]
    private struct RecordInfo
    {
       
        public string name;
        public bool isActive;

        public RecordInfo( string name, bool isActive)
        {
            
            this.name = name;
            this.isActive = isActive;
        }

        // Метод для обновления isActive
        public void SetIsActive(bool isActive)
        {
            this.isActive = isActive;
        }
    }

    // Публичный список для хранения информации об объектах Record
    [SerializeField]
    private List<RecordInfo> records = new List<RecordInfo>();

    private void Awake()
    {
        // Настройка синглтона
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Поиск всех объектов со скриптом Record и добавление их в список
        Record[] foundRecords = FindObjectsOfType<Record>();
        foreach (Record record in foundRecords)
        {
            records.Add(new RecordInfo( record.gameObject.name, record.isActive));
        }
    }

    public void PrintRecordNames()
    {
        // Проход по списку объектов и вывод их имен
        foreach (RecordInfo recordInfo in records)
        {
            // Находим объект на сцене по имени и скрипту Record
            GameObject obj = GameObject.Find(recordInfo.name);
            if (obj != null && obj.GetComponent<Record>() != null)
            {
                Debug.Log("Object Name: " + recordInfo.name);
            }
        }
    }

    public void UpdateRecordInformation(string name)
    {
        // Поиск структуры по имени и обновление её isActive
        for (int i = 0; i < records.Count; i++)
        {
            if (records[i].name == name)
            {
                RecordInfo updatedRecordInfo = records[i];
                updatedRecordInfo.SetIsActive(false);
                records[i] = updatedRecordInfo;
                Debug.Log("Updated isActive for: " + name);
                return;
            }
        }
        Debug.LogWarning("Record with name " + name + " not found.");
    }

    public void SetupRecords()
    {
        // Поиск объектов на сцене по именам из списка records
        foreach (RecordInfo recordInfo in records)
        {
            if (!recordInfo.isActive)
            {
                // Находим объект на сцене по имени
                GameObject obj = GameObject.Find(recordInfo.name);
                if (obj != null)
                {
                    // Получаем скрипт Record и вызываем метод DisableRecord()
                    Record record = obj.GetComponent<Record>();
                    if (record != null)
                    {
                        record.DisableRecord();
                        Debug.Log("Disabled record for: " + recordInfo.name);
                    }
                }
            }
        }
    }
}
