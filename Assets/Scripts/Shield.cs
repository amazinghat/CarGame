using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public float duration;

    void Update()
    {
        duration -= Time.deltaTime;
        if(duration <= 0)
        {
            this.gameObject.transform.parent.tag = "Player";
            Destroy(this.gameObject);
        }
    }
}
