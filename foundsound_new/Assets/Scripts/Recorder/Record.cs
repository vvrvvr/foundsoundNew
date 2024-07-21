using System;
using Unity.VisualScripting;
using UnityEngine;

public class Record : MonoBehaviour, IInteractable
{
   public string RecordName = "test";
   public string Description = "Some decscription";
   public AudioClip recordAudio;
   public bool isDestroyable = false;
   public Outline outline;
   private bool shouldOutlineBeEnabled;
   public Transform emitterTransform;
   private AudioSource audioSource;
   private bool isActive = true;

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
      
      Debug.Log("вижу: "+ gameObject.name);
      if (outline != null)
         shouldOutlineBeEnabled = true;
   }
   
   private void Update()
   {
      if (!isActive)
         return;
      
      if (shouldOutlineBeEnabled)
         outline.enabled = true;
      else
         outline.enabled = false;
      
      shouldOutlineBeEnabled = false;
   }
   
   
   public void Interact()
   {
      if (!isActive)
         return;
      
      TakeRecord();
      Debug.Log("кликнул на: "+ gameObject.name);
     
   }
   
   public void TakeRecord()
   {
      NoteManager.Instance.AddNote(RecordName, Description, recordAudio);
      
      if (isDestroyable) //для выбрасываемого звука
      {
         SignalController signalController = FindObjectOfType<SignalController>();
         signalController.RemoveEmiter(emitterTransform);
         Destroy(gameObject);
      }
      else //для звучащих предметов на уровне
      {
         outline.enabled = false;
         SignalController signalController = FindObjectOfType<SignalController>();
         signalController.RemoveEmiter(emitterTransform);
         emitterTransform.gameObject.SetActive(false);
         audioSource.Stop();
         isActive = false;
      }
      
   }
}
