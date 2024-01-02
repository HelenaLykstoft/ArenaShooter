using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  [SerializeField] private GameObject pauseMenuUI;
  [SerializeField] private bool isPaused;


  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      isPaused = !isPaused;


      if (isPaused)
      {
        ActivateMenu();
      }
      else
      {
        DeactivateMenu();
      }
    }
    Debug.Log("Is paused: " + isPaused);
  }

  void ActivateMenu()
  {
    Time.timeScale = 0;
    AudioListener.pause = true;
    pauseMenuUI.SetActive(true);
    Cursor.lockState = CursorLockMode.None;
  }

  public void DeactivateMenu()
  {
    Time.timeScale = 1;
    AudioListener.pause = false;

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
    Debug.Log("Menu is inactive");
    Debug.Log("Cursorlockmode: " + Cursor.lockState);
    Debug.Log("Cursor visible: " + Cursor.visible);

    pauseMenuUI.SetActive(false);
    isPaused = false;

  }

  public void BackToMainMenu()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    Cursor.lockState = CursorLockMode.None;
  }


}