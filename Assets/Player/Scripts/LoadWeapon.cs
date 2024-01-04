using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadWeapon : MonoBehaviour
{
    public GameObject[] weaponPrefabs;
    void Start()
    {

        int selectedWeapon = PlayerPrefs.GetInt("SelectedWeapon");
       
       switch(selectedWeapon)
       {
        case 0:
            GameObject.Find("TripleBarrelShotgun").SetActive(false);
            break;
        case 1:
            Debug.Log("Selected weapon is: " + GameObject.Find("TripleBarrelShotgun"));
            GameObject.Find("firstGun").SetActive(false);
            break;
            
        default:
            GameObject.Find("firstGun").SetActive(false);
            break;
       }
       GameObject.Find("PlayerUI").GetComponent<PlayerUIScript>().playerSetup();
    }
}
