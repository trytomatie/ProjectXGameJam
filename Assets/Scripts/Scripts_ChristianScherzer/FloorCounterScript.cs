using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloorCounterScript : MonoBehaviour
{
    public TextMeshProUGUI floorText;
    public bool isAnimating = false;
    // Start is called before the first frame update
    void Start()
    {
        RefreshFloorText();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAnimating)
        {
            RefreshFloorText();
        }
        
    }

    void RefreshFloorText()
    {
        floorText.text = "U " + Mathf.Abs(Mathf.RoundToInt((-200 + transform.position.y /12)));
    }
}
