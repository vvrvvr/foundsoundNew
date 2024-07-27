using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotepadNote : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Button button;
    public float offset = 20f;
    public void SetupNote()
    {
        text.text = gameObject.name;
        SetRandomOffset(button, offset);
    }
    
    public void OpenNote()
    {
        NoteManager.Instance.OpenNote(gameObject.name);
    }
    
    void SetRandomOffset(Button button, float offset)
    {
        if (button != null)
        {
            Vector3 localPosition = button.transform.localPosition;
            float randomOffsetX = Random.Range(-offset, offset);
            localPosition.x += randomOffsetX;
            button.transform.localPosition = localPosition;
        }
        else
        {
            Debug.LogWarning("Button reference is not set.");
        }
    }
}
