using System;
using UnityEngine;

public class Record : MonoBehaviour, IInteractable
{
   public string RecordName = "test";
   public string Description = "Some decscription";
   public AudioClip recordAudio;
   public bool isDestroyable = false;

   private void Update() //удалить
   {
      if (Input.GetKeyDown(KeyCode.P))
      {
         NoteManager.Instance.AddNote(RecordName, Description, recordAudio);
      }
   }
   
   public void Interact()
   {
      TakeRecord();
   }
   
   public void TakeRecord()
   {
      NoteManager.Instance.AddNote(RecordName, Description, recordAudio);
      //удалить эмиттер звука
      // выключить звук и интерактивность
   }
}
