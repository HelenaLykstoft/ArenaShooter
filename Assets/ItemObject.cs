using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class ItemObject : ScriptableObject
{
    public string itemName;
    public string description;
    //public Sprite icon;
    public int cost;

    public ItemTypes selectedItemType = new ItemTypes();

    public enum ItemTypes
    {
        increaseHealth,
        refillHealth,
        increaseAmmo,
        refillAmmo,
    }
    
}
