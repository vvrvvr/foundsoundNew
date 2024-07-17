using TMPro;
using UnityEngine;

public class NotepadNote : MonoBehaviour
{
    
    public string RecordName = "test";
    public string Description = "Some decscription";
    public AudioClip recordAudio;
    public TextMeshProUGUI text;

    public void SetupNote(string title, string desc, AudioClip audioForNote)
    {
        RecordName = title;
        Description = desc;
        recordAudio = audioForNote;
        
        text.text = RecordName;
    }
    
    public void OpenNote()
    {
        Debug.Log("Open "+ RecordName);
    }
}
