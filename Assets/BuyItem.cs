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
                    Debug.Log("You bought a weapon.");
                    break;
                case "RefillHealth":
                    PlayerHealth.RefillHealth();
                    Debug.Log("You bought armor.");
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
            Debug.Log("You bought " + displayedItem.itemName + " for " + displayedItem.itemCost + " gold.");
        } else {
            Debug.Log("You don't have enough gold to buy " + displayedItem.itemName + ".");
        }
    }
}
