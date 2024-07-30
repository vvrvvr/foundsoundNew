using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneInteractable : MonoBehaviour, IInteractable
{
   public string SceneName = "";
   public void Interact()
   {
      SceneManager.LoadScene(SceneName);
   }

   public void See()
   {
      Debug.Log("See Exit");
   }
}
