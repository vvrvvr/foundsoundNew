using UnityEngine;

public class CustomRecordReciever : RecordReciever
{
    protected override void HandleRecord(string recordName)
    {
        // Переопределяем логику обработки в дочернем классе
        switch (recordName)
        {
            case "Радио":
                
                Debug.Log("Получил запись радио");
                break;

            case "Робот":
                Debug.Log("Получил запись радио");
                break;

            default:
                // Логика по умолчанию
                Debug.Log("Unknown record received: " + recordName);
                break;
        }
    }
}