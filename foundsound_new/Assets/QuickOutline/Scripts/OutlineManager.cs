using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineManager : MonoBehaviour
{
    private Outline outline;
    public float outlineWidth = 10f;
    private void Awake()
    {
        outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = outlineWidth;
        outline.enabled = false;
    }

    void Start()
    {
        // Find the ParentScript on the parent object
        var record = GetComponentInParent<Record>();

        // Check if the script was found
        if (record != null)
        {
            // Call a method on the parent script
            record.outline = outline;
        }
        else
        {
            Debug.LogError("Record not found in parent hierarchy.");
        }
    }
}
