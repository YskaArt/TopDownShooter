using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handsposition : MonoBehaviour
{
    public SpriteRenderer weaponSpriteRenderer;
    public SpriteRenderer Hand1SpriteRenderer;
    public SpriteRenderer Hand2SpriteRenderer;
    void Update()
    {

        Hand1SpriteRenderer.sortingOrder = weaponSpriteRenderer.sortingOrder;
        Hand2SpriteRenderer.sortingOrder = weaponSpriteRenderer.sortingOrder;

    }
}
