﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilCarBehaviour : MonoBehaviour {

    public float civilCarSpeed = 5f;
    public int direction = -1;

    public float crashDamage = 20f;

    private Vector3 civilCarPosition;

    void Update()
    {
        this.gameObject.transform.Translate(new Vector3(0, direction, 0) * civilCarSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D obj)    // kolizja bokiem z graczem (duży BoxCollider)
    {
        if(obj.gameObject.tag == "Player")
        {
            obj.gameObject.GetComponent<PlayerCarMovement>().durability -= crashDamage / 5;
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)       // kolizja przodem lub tylem z graczem (małe BoxCollidery na przodzie i tyle tylko jako triggery)
    {
        if(obj.gameObject.tag == "Player")
        {
            obj.gameObject.GetComponent<PlayerCarMovement>().durability -= crashDamage;
            Debug.Log("Gracz w nas wjechał");
            Destroy(this.gameObject);
        } else if (obj.gameObject.tag == "EndOfTheRoad")
        {
            Destroy(this.gameObject);
        }
    }
}