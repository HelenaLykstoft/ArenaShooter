using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIScript : MonoBehaviour
{
    // Text box.
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
        if (PlayerHealth != null){
            TextBox.text = "" + PlayerHealth.currentHealth;
        }

        if (PlayerAmmo != null){
            AmmoBox.text = "" + PlayerAmmo.GetCurrentAmmo() + "/" + PlayerAmmo.GetTotalAmmo();
        }
    
    }
}
