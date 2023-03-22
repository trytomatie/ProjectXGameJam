using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnimator : MonoBehaviour
{
    public TextMeshProUGUI textToAnimate;
    public string text;
    public int amountOfLetters;
    public bool isRunning = false;

    private void Update()
    {
        if(isRunning)
        {
            textToAnimate.text = text.Substring(0, Mathf.Clamp(amountOfLetters, 0, text.Length));
        }
    }
}
