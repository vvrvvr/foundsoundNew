using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    private Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    private bool isControlFrozen = false; // Flag to control if movement is frozen
    
    private void OnEnable() 
    {
        EventManager.OnNotePadOpened.AddListener(FreezeControl);
        EventManager.OnNotePadClosed.AddListener(UnfreezeControl);
    }
    
    private void OnDisable()
    {
        EventManager.OnNotePadOpened.RemoveListener(FreezeControl);
        EventManager.OnNotePadClosed.RemoveListener(UnfreezeControl);
    }

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isControlFrozen)
        {
            // If control is frozen, skip movement logic.
            return;
        }

        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        Vector3 velocity = transform.rotation * new Vector3(targetVelocity.x, 0, targetVelocity.y);
        rigidbody.velocity = new Vector3(velocity.x, rigidbody.velocity.y, velocity.z);
    }

    // Method to freeze control
    public void FreezeControl()
    {
        Debug.Log("заморозило");
        isControlFrozen = true;
        rigidbody.velocity = Vector3.zero; // Optionally stop the character's movement
        rigidbody.angularVelocity = Vector3.zero;
        
    }
    public void UnfreezeControl()
    {
        isControlFrozen = false;
    }
}