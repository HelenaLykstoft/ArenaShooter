using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject shopObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // code to open shop when the player presses the "b" key and is near the shop
        if (Input.GetKeyDown(KeyCode.B) && shopObject.GetComponent<ShopTrigger>().isNearShop)
        {
            OpenShop();
        }
    }

    void OpenShop()
    {
        shopPanel.SetActive(true);
    }

}
