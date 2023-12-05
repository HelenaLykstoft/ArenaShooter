using UnityEngine;
using System.Collections;

public class ShopScript : MonoBehaviour
{
    public ArrayList shopItems = new ArrayList();

    void Start()
    {
        // SKal nok initialiseres efter hvert wave
        InitializeShopItems();
    }

    void InitializeShopItems()
    {
        // EKsempel på ting vi kan sælge
        AddShopItem("Wood Armor", 1, 100, "Armor");
        AddShopItem("Health Potion", 2, 10, "Health");
        AddShopItem("Speed Boost", 3, 50, "Speed");
    }

    void AddShopItem(string itemName, int itemID, int itemPrice, string itemType)
    {
        // Hashable to be able to store key value pairs
        Hashtable item = new Hashtable()
        {
            { "Name", itemName },
            { "ID", itemID },
            { "Price", itemPrice },
            { "Type", itemType }
        };

        shopItems.Add(item);
    }

    public void OpenShop()
    {
        Debug.Log("Shop opened!");


        // Tjek for at tingene bliver printet ud
        foreach (Hashtable item in shopItems)
        {
            Debug.Log($"{item["Name"]} - ID: {item["ID"]}, Price: {item["Price"]}, Type: {item["Type"]}");
        }

    }
}
