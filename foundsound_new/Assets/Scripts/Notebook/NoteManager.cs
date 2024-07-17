using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Note
{
    public string title;
    public string description;
    public AudioClip audioClip;
}

public class NoteManager : MonoBehaviour
{
    // Статическая переменная для хранения экземпляра синглтона
    private static NoteManager _instance;

    // Свойство для доступа к экземпляру синглтона
    public static NoteManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<NoteManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("NoteManager");
                    _instance = go.AddComponent<NoteManager>();
                }
            }
            return _instance;
        }
    }

    // Список заметок
    public List<Note> notes = new List<Note>();

    // Метод для добавления новой заметки в список
    public void AddNote(string title, string description, AudioClip audioClip)
    {
        Note newNote = new Note
        {
            title = title,
            description = description,
            audioClip = audioClip
        };

        notes.Add(newNote);
    }

    // Метод для удаления заметки из списка по индексу
    public void RemoveNoteAt(int index)
    {
        if (index >= 0 && index < notes.Count)
        {
            notes.RemoveAt(index);
        }
        else
        {
            Debug.LogWarning("Invalid index: " + index);
        }
    }

    // Метод для удаления заметки по названию
    public void RemoveByName(string name)
    {
        for (int i = 0; i < notes.Count; i++)
        {
            if (notes[i].title == name)
            {
                notes.RemoveAt(i);
                return;
            }
        }
        Debug.LogWarning("Note with title '" + name + "' not found.");
    }

    // Метод Awake для инициализации синглтона
    private void Awake()
    {
        // Если экземпляра нет, то установить его и не уничтожать объект при загрузке новой сцены
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            // Если экземпляр уже существует и это не текущий, уничтожить текущий
            Destroy(gameObject);
        }
    }
}
