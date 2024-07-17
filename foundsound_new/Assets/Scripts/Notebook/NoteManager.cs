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
    public List<NotepadNote> notepadNotes = new List<NotepadNote>();
    public GameObject notePrefab;
    public GameObject notesLayout;

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
        
        if (notePrefab != null && notesLayout != null)
        {
            GameObject newNoteObject = Instantiate(notePrefab, notesLayout.transform);
            var note = newNoteObject.GetComponent<NotepadNote>();
            note.SetupNote(newNote.title, newNote.description, newNote.audioClip);
            
            notepadNotes.Add(note);
        }
        else
        {
            Debug.LogWarning("NotePrefab or NotesLayout is not assigned in the inspector.");
        }
    }

    // Метод для удаления заметки из списка по индексу
    // public void RemoveNoteAt(int index)
    // {
    //     if (index >= 0 && index < notes.Count)
    //     {
    //         notes.RemoveAt(index);
    //     }
    //     else
    //     {
    //         Debug.LogWarning("Invalid index: " + index);
    //     }
    // }

    // Метод для удаления заметки по названию
    public void RemoveByName(string name)
    {
        for (int i = 0; i < notes.Count; i++)
        {
            if (notes[i].title == name)
            {
                notes.RemoveAt(i);
                break;
            }
        }
        for (int i = 0; i < notepadNotes.Count; i++)
        {
            if (notepadNotes[i].RecordName == name)
            {
                var noteGameObject = notepadNotes[i].GetComponent<GameObject>();
                Destroy(noteGameObject);
                notepadNotes.RemoveAt(i);
                return;
            }
        }
        Debug.LogWarning("Note with title '" + name + "' not found.");
    }

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
}
