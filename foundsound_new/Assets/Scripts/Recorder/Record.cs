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
   public bool shouldOutlineBeEnabled;
   
   public void See()
   {
      Debug.Log("вижу: "+ gameObject.name);
      if (outline != null)
         shouldOutlineBeEnabled = true;
   }
   
   private void Update()
   {
      if (shouldOutlineBeEnabled)
         outline.enabled = true;
      else
         outline.enabled = false;
      
      shouldOutlineBeEnabled = false;
   }
   
   
   public void Interact()
   {
      TakeRecord();
      Debug.Log("кликнул на: "+ gameObject.name);
     
   }
   
   public void TakeRecord()
   {
      NoteManager.Instance.AddNote(RecordName, Description, recordAudio);
      //удалить эмиттер звука
      // выключить звук и интерактивность
      Destroy(gameObject);
   }
}
