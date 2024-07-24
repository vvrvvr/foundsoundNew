using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;


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
    public List<GameObject> notepadNotes = new List<GameObject>();
    public GameObject notePrefab;
    public GameObject notesLayout;
    public string currentOpenedNoteName = null;
    [Space(10)] 
    public GameObject notePantel;
    public TextMeshProUGUI notePanelTitle;
    public TextMeshProUGUI notePanelDescription;
    public AudioClip currentAudio;
    [Space(10)] public GameObject droppedSoundPrefab;
    public float dropForce = 1f;
    [Space(10)] public int maxNotesCount = 12;
    

    // Метод для добавления новой заметки в список
    public bool AddNote(string title, string description, AudioClip audioClip)
    {
        if (notes.Count < maxNotesCount)
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
                newNoteObject.name = newNote.title;
                note.SetupNote();
                notepadNotes.Add(newNoteObject);
            }
            else
            {
                Debug.LogWarning("NotePrefab or NotesLayout is not assigned in the inspector.");
            }
            Debug.Log(notes.Count);
            return true;
        }
        else
        {
            return false;
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
                break;
            }
        }
        for (int i = 0; i < notepadNotes.Count; i++)
        {
            
            if (notepadNotes[i].name == name)
            {
                Destroy(notepadNotes[i]);
                notepadNotes.RemoveAt(i);
                return;
            }
        }
        Debug.LogWarning("Note with title '" + name + "' not found.");
    }

    public void OpenNote(string name)
    {
        currentOpenedNoteName = name;
        for (int i = 0; i < notes.Count; i++)
        {
            if (notes[i].title == name)
            {
                notePanelTitle.text = notes[i].title;
                notePanelDescription.text = notes[i].description;
                currentAudio = notes[i].audioClip;
                break;
            }
        }
        Debug.Log("Open "+ currentOpenedNoteName);
        notePantel.SetActive(true);
    }

    public void CloseNote()
    {
        currentOpenedNoteName = null;
        notePanelTitle.text = null;
        notePanelDescription.text = null;
        currentAudio = null;
        notePantel.SetActive(false);
    }

    public void DropNote()
    {
        

        if (droppedSoundPrefab == null)
        {
            Debug.LogWarning("DroppedSoundPrefab is not assigned.");
            return;
        }

        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogWarning("MainCamera not found.");
            return;
        }

        GameObject droppedSound = Instantiate(droppedSoundPrefab, mainCamera.transform.position, Quaternion.identity);
        var droppedSoundRecord = droppedSound.GetComponent<Record>();
        droppedSoundRecord.RecordName = currentOpenedNoteName;
        droppedSoundRecord.Description = notePanelDescription.text;
        droppedSoundRecord.recordAudio = currentAudio;
        droppedSoundRecord.isDestroyable = true;
        
        
        Rigidbody rb = droppedSound.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("DroppedSoundPrefab does not have a Rigidbody component.");
            return;
        }

        rb.AddForce(mainCamera.transform.forward * dropForce, ForceMode.Impulse);
        
        RemoveByName(currentOpenedNoteName);
        CloseNote();
    }
    
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
        
        notePantel.SetActive(false);
    }
}
