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
    private GunMechanics CurrentGun;


    public void playerSetup()
    {
        if (GameObject.Find("Player") != null)
        {
            PlayerHealth = GameObject.Find("Player").GetComponent<Health>();
        }
        if (GameObject.Find("TripleBarrelShotgun") != null)
        {
            CurrentGun = GameObject.Find("TripleBarrelShotgun").GetComponent<GunMechanics>();
        }
        else if (GameObject.Find("firstGun") != null)
        {
            CurrentGun = GameObject.Find("firstGun").GetComponent<GunMechanics>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        TextBox.text = "" + PlayerHealth.currentHealth;
        AmmoBox.text = "" + CurrentGun.GetCurrentAmmo() + "/" + CurrentGun.GetCurrentAmmoCount();


    }
}
