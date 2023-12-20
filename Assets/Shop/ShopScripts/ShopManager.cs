using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int coins;
    public TMP_Text coinUI;
    public ShopItemsSO[] shopItemsSO;
    public GameObject[] shopPanelsGO;
    public ShopTemplate[] shopPanels;
    public Button[] myPurchaseBtns;

    private GameObject Player; 

    private Health PlayerHealth;
    private Gun PlayerAmmo;



    void Start()
    {
        for (int i = 0; i < shopItemsSO.Length; i++){
            shopPanelsGO[i].SetActive(true);
        }
        coinUI.text = "Coins test: " + coins.ToString();
        LoadPanels();
        CheckPurchaseable();

        GameObject Player = GameObject.Find("Player");
        if (Player != null){
            PlayerHealth = Player.GetComponent<Health>();
            Debug.Log(PlayerHealth.currentHealth);
        }

        
            
            //GameObject firstGun = GameObject.Find("firstGun");
            //PlayerAmmo = firstGun.GetComponent<Gun>();
            //Debug.Log("Cannot find Gun component");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoins()
    {
        coins++;
        coinUI.text = "Coins: " + coins.ToString();
        CheckPurchaseable();
    }

    public void CheckPurchaseable(){
        for (int i = 0; i < shopItemsSO.Length; i++){
            if (coins >= shopItemsSO[i].baseCost){
                myPurchaseBtns[i].interactable = true;
            }
            else {
                myPurchaseBtns[i].interactable = false;
            }
        }
    }

    public void PurchaseItem(int btnNbr){
        if (coins >= shopItemsSO[btnNbr].baseCost)
        {
            coins = coins - shopItemsSO[btnNbr].baseCost;
            coinUI.text = "Coins: " + coins.ToString();
            CheckPurchaseable();
        }

            switch (shopItemsSO[btnNbr].itemType){
            case ShopItemType.Health:
                Debug.Log("Error");
                PlayerHealth.IncreaseHealth(5);
                break;

            case ShopItemType.MaxHealth:
                PlayerHealth.GetMaxHealth();
                break;


            //case ShopItemType.Ammo:
                //PlayerAmmo.AddAmmo(5);
                //break;

            // Add flere til armor osv

            //default:
                //break;
            
        }

    }



    public void LoadPanels()
    {
        for (int i = 0; i < shopItemsSO.Length; i++){
            shopPanels[i].titleTxt.text = shopItemsSO[i].title;
            shopPanels[i].descriptionTxt.text = shopItemsSO[i].description;
            shopPanels[i].costTxt.text = "Coins: " + shopItemsSO[i].baseCost.ToString();


        }
    }

}

public enum ShopItemType{
        Health,
        MaxHealth,
        Ammo,
        // Add flere til armor osv
    }
