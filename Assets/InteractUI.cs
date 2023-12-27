using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractUI : MonoBehaviour
{
    [SerializeField] public bool inRange;
    [SerializeField] public bool isPaused;

    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject MovementPlayer;
    [SerializeField] private GameObject Gun;

    public void Start()
    {
        shopUI.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {
        
        if (inRange && Input.GetKeyDown(KeyCode.B))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                ActivateShop();
                Debug.Log("Shop is active");
            }
            else
            {
                DeactivateShop();
                Debug.Log("Shop is inactive");
            }
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
        Cursor.visible = true;
        Debug.Log("Cursor is unlocked");
        Debug.Log("Lockstate is " + Cursor.lockState);
        Debug.Log("Cursor: " + Cursor.visible);

        GameObject.Find("Player").GetComponent<MovementPlayer>().enabled = false;
    
        GameObject.Find("firstGun").GetComponent<Gun>().enabled = false;
    
        Debug.Log("Player and Gun are disabled");

        shopUI.SetActive(true);
    }

    void DeactivateShop()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
    

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("Cursor is locked");
        Debug.Log("Lockstate is " + Cursor.lockState);
        Debug.Log("Cursor: " + Cursor.visible);


        GameObject.Find("Player").GetComponent<MovementPlayer>().enabled = true;
        GameObject.Find("firstGun").GetComponent<Gun>().enabled = true;

        Debug.Log("Player and Gun are enabled");

        shopUI.SetActive(false);

    }


   
}
