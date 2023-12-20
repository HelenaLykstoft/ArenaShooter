using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractUI : MonoBehaviour
{
    [SerializeField] public bool inRange;
    [SerializeField] public bool UIactive;

    public GameObject shopUI;

    void Start()
    {
        shopUI.SetActive(false);
        Cursor.visible = false;
    }

    public void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.B))
        {
            UIactive = true;
            PauseGame();
            Debug.Log("UIactive is true");
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ResumeGame();
            Debug.Log("UIactive is false");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("inRange is true");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")){
            inRange = false;
            Debug.Log("inRange is false");
        }
    }

    public void PauseGame(){
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        AudioListener.pause = true;
        shopUI.SetActive(true);
    
    }

    public void ResumeGame(){
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        AudioListener.pause = false;
        shopUI.SetActive(false);
        UIactive = false;
        
    }
}
