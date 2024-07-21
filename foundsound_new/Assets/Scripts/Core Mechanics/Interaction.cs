using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float interactionDistance = 2f; // Расстояние взаимодействия
    private Camera mainCamera;
    [SerializeField] private LayerMask layerMaskInteract;
    
    public bool canInteract = true;
    
    // private void OnEnable() //удалить
    // {
    //     EventManager.OnPause.AddListener(()=> isPause = true);
    //     EventManager.OnResumeGame.AddListener(()=> isPause = false);
    // }
    //
    // private void OnDisable()
    // {
    //     EventManager.OnPause.RemoveListener(()=> isPause = true);
    //     EventManager.OnResumeGame.AddListener(()=> isPause = false);
    // }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (!canInteract)
            return;
        Interact();
    }

    void Interact()
    {
        RaycastHit hit;
        Vector3 fwd = mainCamera.transform.TransformDirection(Vector3.forward);
        
        if (Physics.Raycast(mainCamera.transform.position, fwd, out hit, interactionDistance, layerMaskInteract.value))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}