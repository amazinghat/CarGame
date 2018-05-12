using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditCarBehaviour : MonoBehaviour {

	public GameObject bomb;
	public int bombsAmount;
	public int banditCarVerticalSpeed;
	public int banditCarHorizontalSpeed;
	public float bombDelay;
    [HideInInspector]
    public int pointsPerCar;

	private float delay;
	private GameObject playerCar;
	private Vector3 banditCarPos;

	void Start()
	{
		playerCar = GameObject.FindWithTag ("Player");
		delay = bombDelay;
	}

	void Update()
	{
		if (playerCar == null) 	// sprawdzaj czy auto gracza istnieje
		{
			playerCar = GameObject.FindWithTag ("Player");
		} 
		else 
		{
			if (gameObject.transform.position.y > 3.8f && bombsAmount > 0) {		// jesli zaczyna na gorze mapy i ma bomby
				this.gameObject.transform.Translate (new Vector3 (0, -1, 0) * banditCarVerticalSpeed * Time.deltaTime);		// jedzie w dol

			} else if (bombsAmount <= 0) {		// jesli skonczyly sie bomby
				this.gameObject.transform.Translate (new Vector3 (0, 1, 0) * banditCarVerticalSpeed * Time.deltaTime);		// wraca na gore
				if (gameObject.transform.position.y > 6.5f) 
				{
                    PointsManager.points += pointsPerCar;
					Destroy (this.gameObject);		// i znika
				}
			}
			else 
			{
				banditCarPos = Vector3.Lerp (transform.position, playerCar.transform.position, Time.fixedDeltaTime * banditCarHorizontalSpeed);
				transform.position = new Vector3 (banditCarPos.x, transform.position.y, 0);

				delay -= Time.deltaTime;
				if (delay <= 0 && bombsAmount > 5) {
					delay = bombDelay;
					bombsAmount--;
					Instantiate (bomb, transform.position, Quaternion.identity);
				} else if (delay <= 0 && bombsAmount <= 5 && bombsAmount > 0) 
				{
					delay = bombDelay / 2;
					bombsAmount--;
					Instantiate (bomb, transform.position, Quaternion.identity);
				}
			}
		}

	}

}
