using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    [Header("Wave 1 (Civil Cars")]
    public GameObject civilCar;
    public float civilCarSpawnDelay;
    public int civilCarsAmount;

    [Header("Wave 2 (Bandit Cars")]
    public GameObject banditCar;
    public int bombsAmount;                 // kopie zmiennych z banditBehaviora
    public int banditCarVerticalSpeed;      // zeby moc ustawiac z WaveManagera bezposrednio
    public int banditCarHorizontalSpeed;    // zmienne odnoszace sie do banditBehaviora
    public float bombDelay;
    private GameObject spawnedBanditCar;
    private bool isSpawned;
    private bool is2ndSpawned;

    [Header("Wave 3 (Police Cars")]
    public GameObject policeCar;
    public int policeCarAmount;
    public float shootingSeriesDelay;       // takie same kopie j/w
    public float singleShotDelay;
    public float policeCarVerticalSpeed;
    public int bulletsInSeries;
    [HideInInspector]
    static public bool isLeft;
    [HideInInspector]
    static public bool isRight;
    private GameObject spawnedPoliceCar;

    [Header("Points")]
    public int pointsPerCivilCar;
    public int pointsPerBanditCar;
    public int pointsPerBomb;
    public int pointsPerPoliceCar;

	public GameObject EndGameScreen;

    private float[] lanesArray;
    private float spawnDelay;

    private void Start()
    {
        lanesArray = new float[4];
        lanesArray[0] = -2.12f;
        lanesArray[1] = -0.75f;
        lanesArray[2] = 0.75f;
        lanesArray[3] = 2.12f;
        spawnDelay = civilCarSpawnDelay;
    }

    void Update()
    {
        spawnDelay -= Time.deltaTime;
        if (spawnDelay <= 0 && civilCarsAmount > 0)
        {
            spawnCar();                         // spawnuj cywili
            spawnDelay = civilCarSpawnDelay;
        }
        else if (civilCarsAmount <= 0 && is2ndSpawned == false)     // jak sie skoncza cywile
        {        
            if (isSpawned == false)     
            {
                spawnBanditCar();
            }
            else if (isSpawned == true && spawnedBanditCar.GetComponent<BanditCarBehaviour>().bombsAmount < 10 && is2ndSpawned == false)
            {
                spawnBanditCar();
            }
        } else if (civilCarsAmount <= 0 && policeCarAmount > 0 && spawnedBanditCar == null)
        {
            spawnPoliceCar();
		} else if (policeCarAmount <= 0 && isLeft == false && isRight == false)
		{
			Time.timeScale = 0;
			EndGameScreen.SetActive(true);
		}
    }

    void spawnPoliceCar()
    {
        Transform playerCarPosition;
        if(GameObject.FindWithTag("Player"))
        {
            playerCarPosition = GameObject.FindWithTag("Player").transform;
        } else if (GameObject.FindWithTag("Shield"))
        {
            playerCarPosition = GameObject.FindWithTag("Shield").transform;

        } else if (GameObject.FindWithTag("Untouchable"))
        {
            playerCarPosition = GameObject.FindWithTag("Untouchable").transform;

        } else
        {
            playerCarPosition = null;
        }
        if (playerCarPosition.position.x <= -0.51f && isRight == false)
        {
            spawnedPoliceCar = (GameObject)Instantiate(policeCar, new Vector3(2.05f, -7f, 0), Quaternion.identity);
            spawnedPoliceCar.GetComponent<PoliceCarBehaviour>().isLeft = false;
            isRight = true;
            policeCarAmount--;
        } else if (GameObject.FindWithTag("Player").gameObject.transform.position.x > -0.51f && isLeft == false)
        {
            spawnedPoliceCar = (GameObject)Instantiate(policeCar, new Vector3(-2.05f, -7f, 0), Quaternion.identity);
            spawnedPoliceCar.GetComponent<PoliceCarBehaviour>().isLeft = true;
            isLeft = true;
            policeCarAmount--;
        }
        spawnedPoliceCar.GetComponent<PoliceCarBehaviour>().shootingSeriesDelay = shootingSeriesDelay;      // przypisanie kopii do prawdziwych zmiennych
        spawnedPoliceCar.GetComponent<PoliceCarBehaviour>().singleShotDelay = singleShotDelay;              // w policeCarBehaviour
        spawnedPoliceCar.GetComponent<PoliceCarBehaviour>().bulletsInSeries = bulletsInSeries;
        spawnedPoliceCar.GetComponent<PoliceCarBehaviour>().policeCarVerticalSpeed = policeCarVerticalSpeed;
        spawnedPoliceCar.GetComponent<PoliceCarBehaviour>().pointsPerCar = pointsPerPoliceCar;
    }

    void spawnBanditCar()       // f spawnujaca dwa auta bandytów
    {
        if (isSpawned == false)
        {
            spawnedBanditCar = (GameObject)Instantiate(banditCar, new Vector3(Random.Range(-2.25f, 2.25f), 7f, 0), Quaternion.identity);
            spawnedBanditCar.GetComponent<BanditCarBehaviour>().bombDelay = bombDelay;

            isSpawned = true;
        } else if (isSpawned == true && is2ndSpawned == false)
        {
            if (spawnedBanditCar.transform.position.x < 0.45f)
            {
                spawnedBanditCar = (GameObject)Instantiate(banditCar, new Vector3(2.2f, 7f, 0), Quaternion.identity);
                is2ndSpawned = true;
            } else if (spawnedBanditCar.transform.position.x >= 0.45f)
            {
                spawnedBanditCar = (GameObject)Instantiate(banditCar, new Vector3(-2.2f, 7f, 0), Quaternion.identity);
                is2ndSpawned = true;
            }
            spawnedBanditCar.GetComponent<BanditCarBehaviour>().bombDelay = bombDelay / 1.5f;

        }
        spawnedBanditCar.GetComponent<BanditCarBehaviour>().bombsAmount = bombsAmount;                          // przypisanie kopi do prawdziwych
        spawnedBanditCar.GetComponent<BanditCarBehaviour>().banditCarVerticalSpeed = banditCarVerticalSpeed;    // w bandirCarBehaviorze
        spawnedBanditCar.GetComponent<BanditCarBehaviour>().banditCarHorizontalSpeed = banditCarHorizontalSpeed;
        spawnedBanditCar.GetComponent<BanditCarBehaviour>().pointsPerCar = pointsPerBanditCar;
        spawnedBanditCar.GetComponent<BanditCarBehaviour>().bomb.GetComponent<Bomb>().pointsPerBomb = pointsPerBomb;
    }

    void spawnCar()
    {
        int lane = Random.Range(0, 4);      // spawn na losowym pasie
        if (lane == 0 || lane == 1)
        {
            GameObject car = (GameObject)Instantiate(civilCar, new Vector3(lanesArray[lane], 6f, 0), Quaternion.Euler(new Vector3(0,0,180)));
            car.GetComponent<CivilCarBehaviour>().direction = 1;
            car.GetComponent<CivilCarBehaviour>().civilCarSpeed = 10;
            car.GetComponent<CivilCarBehaviour>().pointsPerCar = pointsPerCivilCar;
        }
        if (lane == 2 || lane == 3)
        {
            GameObject car = (GameObject)Instantiate(civilCar, new Vector3(lanesArray[lane], 6f, 0), Quaternion.identity);
            car.GetComponent<CivilCarBehaviour>().pointsPerCar = pointsPerCivilCar;
        }
        civilCarsAmount--;
    }

}
