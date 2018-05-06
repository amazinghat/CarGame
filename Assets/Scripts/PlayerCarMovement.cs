using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarMovement : MonoBehaviour {

    public float carHorizontalSpeed = 2f;
    private Vector3 carPosition;

    public float maxDurability = 100f;
    public float durability;

    private void Start()
    {
        carPosition = this.gameObject.transform.position;
        durability = maxDurability;
    }

    private void Update()
    {
        carPosition.x += Input.GetAxis("Horizontal") * carHorizontalSpeed * Time.deltaTime;
        carPosition.x = Mathf.Clamp(carPosition.x, -2.41f, 2.41f);      // trzymaj pozycje w przedziale
        this.gameObject.transform.position = carPosition;               // aktualizuj pozycje
    }

}
