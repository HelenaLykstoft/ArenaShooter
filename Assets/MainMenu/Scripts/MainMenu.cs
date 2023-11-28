using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    //Load spil scenen
  public void Play()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

//Quit spillet
  public void Quit()
  {
    Debug.Log("Quitting Game...");
    Application.Quit();
  }
}
