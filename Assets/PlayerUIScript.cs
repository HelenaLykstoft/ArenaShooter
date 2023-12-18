using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIScript : MonoBehaviour
{
    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private bool isPaused;
    

    [SerializeField] private TMP_Text TextBox;
    [SerializeField] private TMP_Text AmmoBox;
    private Health PlayerHealth;
    private Gun PlayerAmmo;
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = GetComponentInParent<Health>();
        GameObject firstGun = GameObject.Find("firstGun");
        PlayerAmmo = firstGun.GetComponent<Gun>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
         {
              isPaused = !isPaused;
         }
            if(isPaused){
                ActivateShop();
            }
            else{
                DeactivateShop();

         }
        if (PlayerHealth != null){
            TextBox.text = "" + PlayerHealth.currentHealth;
        }

        if (PlayerAmmo != null){
            AmmoBox.text = "" + PlayerAmmo.GetCurrentAmmo() + "/" + PlayerAmmo.GetTotalAmmo();
        }
    
    }

    void ActivateShop()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        shopCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
   }

   public void DeactivateShop()
   {
        Time.timeScale = 1;
        AudioListener.pause = false;
        shopCanvas.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
   }
}
