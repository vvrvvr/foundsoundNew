using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public bool isHasControl = true;

    [Space(10)] public GameObject NotepadObj;
    public GameObject RecorderObj;

    public float delayTime = 2.0f; // Время задержки в секундах

    // Start is called before the first frame update
    void Start()
    {
        NotepadObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHasControl)
            return;

        ToggleObject(KeyCode.R, RecorderObj, null, null);
        ToggleNotepad(KeyCode.F, NotepadObj, EventManager.OnNotePadOpened, EventManager.OnNotePadClosed);
    }

    public void ToggleObject(KeyCode key, GameObject obj, UnityEvent onOpen, UnityEvent onClose)
    {
        if (Input.GetKeyDown(key))
        {
            if (obj != null && obj.activeSelf)
            {
                obj.SetActive(false);
                onClose?.Invoke();
            }
            else
            {
                obj.SetActive(true);
                onOpen?.Invoke();
            }
        }
    }

    public void ToggleNotepad(KeyCode key, GameObject obj, UnityEvent onOpen, UnityEvent onClose)
    {
        if (Input.GetKeyDown(key))
        {
            StartCoroutine(ToggleNotepadCoroutine(obj, onOpen, onClose));
        }
    }

    private IEnumerator ToggleNotepadCoroutine(GameObject obj, UnityEvent onOpen, UnityEvent onClose)
    {
        if (obj != null && obj.activeSelf)
        {
            onClose?.Invoke();
            yield return new WaitForSeconds(delayTime);
            obj.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(0);
            obj.SetActive(true);
            onOpen?.Invoke();
        }
    }
}