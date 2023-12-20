using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    public Wallet walletScript;
    public ItemDisplay displayedItem;

    private Health PlayerHealth;
    private Gun PlayerAmmo;


    public void Start(){
        PlayerHealth = GameObject.Find("Player").GetComponent<Health>();
        PlayerAmmo = GameObject.Find("Player").GetComponent<Gun>();
    }

    public void Buy(){
        if (walletScript.gold >= displayedItem.itemCost){
            walletScript.gold -= displayedItem.itemCost;
            // KØB ITEM
            switch (displayedItem.itemType){
                case "IncreaseHealth":
                    PlayerHealth.IncreaseHealth(5);
                    Debug.Log("You got 5 increased health.");
                    break;
                case "RefillHealth":
                    PlayerHealth.RefillHealth();
                    Debug.Log("You refilled your health.");
                    break;
                //case "IncreaseAmmo":
                    //PlayerAmmo.IncreaseAmmo(5);
                    //Debug.Log("You bought a potion.");
                    //break;
                //case "RefillAmmo":
                    //PlayerAmmo.RefillAmmo();
                    //Debug.Log("You bought a potion.");
                    //break;
                default:
                    Debug.Log("You bought something.");
                    break;
            }
        } else {
            Debug.Log("You don't have enough gold to buy " + displayedItem.itemName + ".");
        }
    }
}
