﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    public int hp = 2;
    public Sprite damageSprite;

    // Start is called before the first frame update
    public void TakeDamage() {
        hp -= 1;
        GetComponent<SpriteRenderer>().sprite = damageSprite;
        if (hp <= 0) {
            Destroy(this.gameObject);
         
        }
    }
}
