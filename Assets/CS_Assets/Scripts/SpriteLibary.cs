using System.Collections;
using UnityEngine;

public class SpriteLibary : MonoBehaviour
{
    public Sprite[] sprites;
    public static SpriteLibary instance;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static Sprite GetSprite(int id)
    {
        return instance.sprites[id];
    }
}
