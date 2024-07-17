using System;
using UnityEngine;

public class Record : MonoBehaviour
{
   public string RecordName = "test";
   public string Description = "Some decscription";
   public AudioClip recordAudio;

   private void Update() //удалить
   {
      if (Input.GetKeyDown(KeyCode.P))
      {
         NoteManager.Instance.AddNote(RecordName, Description, recordAudio);
      }
      
   }
}
