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


    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Player") != null)
        {
            PlayerHealth = GameObject.Find("Player").GetComponent<Health>();
        }
        if (GameObject.Find("TrippleBarrelShotgun") != null)
        {
            CurrentGun = GameObject.Find("TrippleBarrelShotgun").GetComponent<GunMechanics>();
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
