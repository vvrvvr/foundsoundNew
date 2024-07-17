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
    private static NoteManager _instance;

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

    // Метод Awake для инициализации синглтона
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

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

    public void Test()
    {
        Debug.Log("Test");
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
}