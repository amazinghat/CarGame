using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDurabilityManager : MonoBehaviour {

    public GameObject playerCarPrefab;
    public GameObject spawnPoint;
    public TextMesh durabilityText;
    public int lifes;
	public GameObject EndGameScreen;
	[HideInInspector]
	public int maxLifes;
    private GameObject playerCar;

    void Start()
    {
		maxLifes = lifes;
        playerCar = (GameObject)Instantiate(playerCarPrefab, spawnPoint.transform.position, Quaternion.identity);
    }

    void Update()
    {
        if(playerCar.GetComponent<PlayerCarMovement>().durability <= 0)     // sprawdzanie wytrzymalosci, niszczenie przy 0 i odejmowanie zyc
        {
            Destroy(playerCar);
            lifes--;
            if(lifes > 0)
            {
                StartCoroutine("SpawnaCar");    // ochrona po spawnie, zeby od razu nie zginac
			} else if (lifes <= 0)
			{
				Time.timeScale = 0;		//zamrażamy grę
				EndGameScreen.SetActive(true);
			}
        }

        else if (playerCar.GetComponent<PlayerCarMovement>().durability > playerCar.GetComponent<PlayerCarMovement>().maxDurability)
        {
            playerCar.GetComponent<PlayerCarMovement>().durability = playerCar.GetComponent<PlayerCarMovement>().maxDurability;     // ograniczenie max wytrzymalosci
        }

        durabilityText.text = "Durability: " + playerCar.GetComponent<PlayerCarMovement>().durability + "/" + playerCar.GetComponent<PlayerCarMovement>().maxDurability;
    }

    IEnumerator SpawnaCar()     // ochrona po spawnie, zeby od razu nie zginac
    {
        playerCar = (GameObject)Instantiate(playerCarPrefab, spawnPoint.transform.position, Quaternion.identity);
        playerCar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);   // zmiana przezroczystosci po respawnie
        playerCar.GetComponent<BoxCollider2D>().isTrigger = true;                       // zmiana z collidera na tylko trigger
        playerCar.tag = "Untouchable";                                                  // zeby nie odejmowalo nawet jak dotknie trigger, co jest zakodowane w skypcie CivilCarBehavior - funkcja OnTrigger
        yield return new WaitForSeconds(3);
        playerCar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        playerCar.GetComponent<BoxCollider2D>().isTrigger = false;
        playerCar.tag = "Player";
    }

}
