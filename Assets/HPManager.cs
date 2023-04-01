using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public Image hearts;
    public float offset = 10;
    public float increments = 22;

    public StatusManager target;

    public void Update()
    {
        hearts.rectTransform.sizeDelta = new Vector2(target.Hp * increments + offset,32);
    }
}
