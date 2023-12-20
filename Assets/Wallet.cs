using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{

    public int gold;
    public TextMeshProUGUI goldBalance;

    private void Update(){
        goldBalance.text = "Your balance: " + gold.ToString();
    }
}
