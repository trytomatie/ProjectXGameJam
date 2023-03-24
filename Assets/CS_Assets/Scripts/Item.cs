using System;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;


[System.Serializable]
public class Item
{
    public int id = 0;
    public string itemName = "Empty";
    private int stacks = 0;
    public int maxStacks = 0;
    public Sprite sprite;

    public event EventHandler StackEvent;

    public Item()
    {

    }


    public int Stacks 
    { get => stacks; 
        set
        {
            if (stacks != value)
            {
                stacks = value;
                StackEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }

}