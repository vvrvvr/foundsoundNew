using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float interactionDistance = 2f; // Расстояние взаимодействия
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Проверка нажатия клавиши "E"
        {
            Interact();
        }
    }

    void Interact()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}