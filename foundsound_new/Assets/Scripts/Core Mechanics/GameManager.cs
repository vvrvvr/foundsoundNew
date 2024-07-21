using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        ToggleObject(KeyCode.R, NotepadObj);
        ToggleObject(KeyCode.F, RecorderObj);
    }

    public void ToggleObject(KeyCode key, GameObject obj)
    {
        if (Input.GetKeyDown(key))
        {
            if (obj != null && obj.activeSelf)
            {
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(true);
            }
        }
    }
}