using TMPro;
using UnityEngine;

public class NotepadNote : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void SetupNote()
    {
        text.text = gameObject.name;
    }
    
    public void OpenNote()
    {
        NoteManager.Instance.OpenNote(gameObject.name);
    }
}
