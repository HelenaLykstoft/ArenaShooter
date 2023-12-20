using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractUI : MonoBehaviour
{
    [SerializeField] public bool inRange;
    [SerializeField] public bool isPaused;

    [SerializeField] private GameObject shopUI;

    public void Start()
    {
        shopUI.SetActive(false);
        isPaused = false;
        Cursor.visible = false;
    }

    public void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.B))
        {
            isPaused = !isPaused;
        }
        if (isPaused)
        {
            ActivateShop();
        }
        else
        {
            DeactivateShop();
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

    void ActivateShop()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        Cursor.lockState = CursorLockMode.None;
   }

   public void DeactivateShop()
   {
        Time.timeScale = 1;
        AudioListener.pause = false;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
   }
}
