using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RehtseStudio.SimpleWaveSystem.Managers;


public class InteractUI : MonoBehaviour
{
    [SerializeField] public bool inRange;
    [SerializeField] public bool isPaused;

    [SerializeField] private GameObject shopUI;




    public void Start()
    {
        shopUI.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {  
        if (inRange && Input.GetKeyDown(KeyCode.B) && GameObject.Find("Managers").GetComponent<SpawnManager>().isBetweenRounds)
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                ActivateShop();
            }
            else
            {
                DeactivateShop();
                Debug.Log("Shop is inactive");
            }
        } else if (isPaused && GameObject.Find("Managers").GetComponent<SpawnManager>().isBetweenRounds == false)
        {
            DeactivateShop();
            isPaused = false;
            Debug.Log("Shop is inactive");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
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
        //Time.timeScale = 0;
        AudioListener.pause = true;
    
        //Debug.Log("Cursor is unlocked");
        //Debug.Log("Lockstate is " + Cursor.lockState);
        //Debug.Log("Cursor: " + Cursor.visible);
        var movementPlayer = GameObject.Find("Player").GetComponent<MovementPlayer>();
        movementPlayer.SetWantedMode(CursorLockMode.None);

        movementPlayer.enabled = false;

        GameObject.Find("TripleBarrelShotgun").GetComponent<GunMechanics>().enabled = false;
    
        Debug.Log("Player and Gun are disabled");

        shopUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        
    }

    void DeactivateShop()
    {
        //Time.timeScale = 1;
        AudioListener.pause = false;
    

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Debug.Log("Cursor is locked");
        //Debug.Log("Lockstate is " + Cursor.lockState);
        //Debug.Log("Cursor: " + Cursor.visible);

        var movementPlayer = GameObject.Find("Player").GetComponent<MovementPlayer>();
        movementPlayer.enabled = true;
        GameObject.Find("TripleBarrelShotgun").GetComponent<GunMechanics>().enabled = true;

        Debug.Log("Player and Gun are enabled");

        shopUI.SetActive(false);

        movementPlayer.SetWantedMode(CursorLockMode.Locked);

    }


   
}
