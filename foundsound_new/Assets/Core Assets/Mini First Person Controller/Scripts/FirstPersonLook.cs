using UnityEngine;
using System.Collections;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;
    public float lockTime = 1.0f;

    Vector2 velocity;
    Vector2 frameVelocity;
    bool isLocked = false;

    private void OnEnable() 
    {
        EventManager.OnNotePadOpened.AddListener(LockView);
        EventManager.OnNotePadClosed.AddListener(UnlockView);
    }
    
    private void OnDisable()
    {
        EventManager.OnNotePadOpened.RemoveListener(LockView);
        EventManager.OnNotePadClosed.RemoveListener(UnlockView);
    }

    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (isLocked)
            return;

        // Get smooth velocity.
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Rotate camera up-down from velocity.
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        
        // Rotate character left-right from velocity.
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }

    public void LockView()
    {
        Cursor.lockState = CursorLockMode.None;
        isLocked = true;
        StartCoroutine(CenterCamera(lockTime));
        Debug.Log("камера заблокирована");
    }

    public void UnlockView()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isLocked = false;
        Debug.Log("камера разблокирована");
    }

    IEnumerator CenterCamera(float duration)
    {
        float elapsedTime = 0;
        float startYRotation = velocity.y;

        // Сброс значения velocity.x при блокировке камеры
        //velocity.x = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            velocity.y = Mathf.Lerp(startYRotation, 0, t);

            // Rotate camera up-down from velocity.
            transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);

            yield return null;
        }

        velocity.y = 0;
    }
}
