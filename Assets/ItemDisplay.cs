using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDisplay : MonoBehaviour
{
    public ItemObject displayedItem;

    // Logic
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public int itemCost;
    public string itemType;

    // Graphics

    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public Image itemIconImage;
    public TextMeshProUGUI itemCostText;

    public void Start(){
        itemName = displayedItem.name;
        itemDescription = displayedItem.description;
        itemIcon = displayedItem.icon;
        itemCost = displayedItem.cost;
        itemType = displayedItem.selectedItemType.ToString();

        itemNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemIconImage.sprite = itemIcon;
        itemCostText.text = itemCost.ToString();
    }

    public void Display(){
        itemName = displayedItem.name;
        itemDescription = displayedItem.description;
        itemIcon = displayedItem.icon;
        itemCost = displayedItem.cost;
        itemType = displayedItem.selectedItemType.ToString();

        itemNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemIconImage.sprite = itemIcon;
        itemCostText.text = itemCost.ToString();
    }

}
