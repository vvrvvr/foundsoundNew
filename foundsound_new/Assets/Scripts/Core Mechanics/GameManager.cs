using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public bool isHasControl = true;

    [Space(10)] public GameObject NotepadObj;

    public GameObject RecorderObj;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHasControl)
            return;
        ToggleObject(KeyCode.R, RecorderObj, null, null);
        ToggleObject(KeyCode.F, NotepadObj,EventManager.OnNotePadOpened, EventManager.OnNotePadClosed );
    }

    public void ToggleObject(KeyCode key, GameObject obj, UnityEvent onOpen, UnityEvent onClose)
    {
        if (Input.GetKeyDown(key))
        {
            if (obj != null && obj.activeSelf)
            {
                obj.SetActive(false);
                if (onClose != null)
                {
                    onClose.Invoke();
                }
            }
            else
            {
                obj.SetActive(true);
                if (onOpen != null)
                {
                    onOpen.Invoke();
                }
            }
        }
    }
}