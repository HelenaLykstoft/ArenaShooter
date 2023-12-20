using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeItems : MonoBehaviour
{
    public ItemDisplay itemDisplayScript;
    public ItemObject itemToChange;

    public void ChangeItem(){
        itemDisplayScript.displayedItem = itemToChange;
        itemDisplayScript.Display();
    }
}
