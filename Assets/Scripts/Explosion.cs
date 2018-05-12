using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    private float delay = 0.7f;


	void Update () {

        delay -= Time.deltaTime;
        if(delay <= 0)
        {
            Destroy(gameObject);
        }

	}
}
