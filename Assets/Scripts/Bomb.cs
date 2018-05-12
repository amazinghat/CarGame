using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public GameObject explosion;    // obiek eksplozji
	public int bombDamage;
	public float bombSpeed;
    [HideInInspector]
    public int pointsPerBomb;

	void Update(){
		this.gameObject.transform.Translate (new Vector3 (0, -1, 0) * bombSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D obj) {
		if (obj.gameObject.tag == "Player")
        {
            PointsManager.points -= pointsPerBomb;
            obj.gameObject.GetComponent<PlayerCarMovement> ().durability -= bombDamage;
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);     //spawnuj explozje na miejscu bomby
			Destroy (this.gameObject);
		} 
		else if (obj.gameObject.tag == "Shield") {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy (this.gameObject);
		} 
		else if (obj.gameObject.tag == "EndOfTheRoad") {
            PointsManager.points += pointsPerBomb;
			Destroy (this.gameObject);
		}
	}

}
