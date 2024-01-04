using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    public Wallet walletScript;
    public ItemDisplay displayedItem;

    private Health PlayerHealth;
    private GunMechanics CurrentGun;


    public void Start(){
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

    public void Buy(){
        if (walletScript.gold >= displayedItem.itemCost){
            walletScript.gold -= (int)displayedItem.itemCost;

            Debug.Log(displayedItem.itemName);
            // KØB ITEM
            switch (displayedItem.itemName){
                case "IncreaseHealth":
                    PlayerHealth.IncreaseHealth(5);
                    Debug.Log("You got 5 increased health.");
                    break;
                case "RefillHealth":
                    PlayerHealth.RefillHealth();
                    Debug.Log("You refilled your health.");
                    break;
                case "IncreaseAmmo":
                    CurrentGun.IncreaseAmmo(3);
                    Debug.Log("You increased your max ammo.");
                    break;
                case "RefillAmmo":
                    CurrentGun.RefillAmmo();
                    Debug.Log("You refilled your ammo.");
                    break;
                case "IncreaseDamage":
                    CurrentGun.IncreaseDamage(2);
                    Debug.Log("You increased your damage.");
                    break;
                default:
                    Debug.Log("You bought something.");
                    break;
            }
        } else {
            Debug.Log("You don't have enough gold to buy " + displayedItem.itemName + ".");
        }
    }
}
