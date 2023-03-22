using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int keys = 0;
    private int cheese = 0;

    public TextMeshProUGUI keyText;
    public TextMeshProUGUI cheeseText;

    public int Keys
    {
        get => keys;
        set
        {
            keyText.text = value.ToString();
            keys = value;
        }
    }
    public int Cheese
    {
        get => cheese;
        set
        {
            cheeseText.text = value.ToString();
            cheese = value;
        }
    }

}
