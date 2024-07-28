using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

public class Record : MonoBehaviour, IInteractable
{
    public string RecordName = "test";
    public string Description = "Some description";
    public AudioClip recordAudio;
    public bool isDestroyable = false;
    public Outline outline;
    private bool shouldOutlineBeEnabled;
    public Transform emitterTransform;
    private AudioSource audioSource;
    private bool isActive = true;
    public ParticleSystem effect; // Добавляем партикл эффект

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = recordAudio;
        audioSource.Play();
    }

    public void See()
    {
        if (!isActive)
            return;
      
        Debug.Log("вижу: " + gameObject.name);
        if (outline != null)
            shouldOutlineBeEnabled = true;
    }
   
    private void Update()
    {
        if (!isActive)
            return;
      
        outline.enabled = shouldOutlineBeEnabled;
        shouldOutlineBeEnabled = false;
    }

    public void Interact()
    {
        if (!isActive)
            return;
      
        TakeRecord();
        Debug.Log("кликнул на: " + gameObject.name);
    }
   
    public void TakeRecord()
    {
        bool canTakeRecord = NoteManager.Instance.AddNote(RecordName, Description, recordAudio);
        if (canTakeRecord)
        {
            if (isDestroyable) // для выбрасываемого звука
            {
                DestroyWithDelay(0f);
            }
            else // для звучащих предметов на уровне
            {
                outline.enabled = false;
                SignalController signalController = FindObjectOfType<SignalController>();
                signalController.RemoveEmiter(emitterTransform);
                emitterTransform.gameObject.SetActive(false);
                audioSource.Stop();
                isActive = false;
            }
        }
        else
        {
            Debug.Log("не могу взять запись не хватает места в блокноте");
            // логика если не могу взять запись
        }
    }

    public void  DestroyWithDelay(float delay)
    {
        StartCoroutine(DestroyRecordWithDelay(delay));
    }
    
    private IEnumerator DestroyRecordWithDelay(float destroyDelay)
    {
        isActive = false;
        // Вызываем партикл эффект
        if (effect != null)
        {
            ParticleSystem instantiatedEffect = Instantiate(effect, transform.position, Quaternion.identity);
            instantiatedEffect.Play();
            //Destroy(instantiatedEffect.gameObject, instantiatedEffect.main.duration);
        }

        // Ждём заданное время перед уничтожением
        yield return new WaitForSeconds(destroyDelay);

        DestroyRecord();
    }

    public void DestroyRecord()
    {
        SignalController signalController = FindObjectOfType<SignalController>();
        signalController.RemoveEmiter(emitterTransform);
        Destroy(gameObject);
    }
}
