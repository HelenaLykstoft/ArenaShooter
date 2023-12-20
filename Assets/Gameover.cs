using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Gameover : MonoBehaviour
{
    bool isGameOver = false;
    [SerializeField] private GameObject GameoverUI;
    

    float timeToInvoke = 5f;
    float remainingTime;

    void Update()
    {
        if (isGameOver)
        {
            if (remainingTime >= 0)
            {
                remainingTime -= Time.deltaTime;
                if (remainingTime >= 0){
                    //set timer text box in gameoverUI to remaining time
                    TextMeshProUGUI text =  GameoverUI.GetComponentInChildren<TextMeshProUGUI>();
                    text.text = "Returning to Main Menu in " + Mathf.RoundToInt(remainingTime) + " seconds";
                }
            }
            
        }
    }
    
    public void Endgame()
    {
        isGameOver = true;
        GameoverUI.SetActive(true);
        remainingTime = timeToInvoke;
        Invoke("BackToMainMenu", timeToInvoke);

    }

    public void BackToMainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        
    }
}
