using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarBehaviour : MonoBehaviour {

    public Light redLight;
    public Light blueLight;
    public float lightDelay;
    public GameObject bullet;
    public float shootingSeriesDelay;
    public float singleShotDelay;
    public bool isLeft;
    public float policeCarVerticalSpeed;
    public int bulletsInSeries;
    public GameObject explosion;
    public AudioClip shotSound;
    [HideInInspector]
    public int pointsPerCar;
    private AudioSource audio;
    private float lightShowDelay;
    private float shootDelay;
    private Vector3 policeCarPos;
    private GameObject bulletObj;

    void Start()
    {
        lightShowDelay = 2 * lightShowDelay;
        shootDelay = shootingSeriesDelay;
        audio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        lightShowDelay -= Time.deltaTime;       // zarzadzanie miganiem swiatel
        if (lightShowDelay > lightDelay)
        {
            blueLight.enabled = false;
            redLight.enabled = true;
        } else if (lightShowDelay <= lightDelay && lightShowDelay > 0)
        {
            blueLight.enabled = true;
            redLight.enabled = false;
        } else if (lightShowDelay <= 0)
        {
            lightShowDelay = 2 * lightDelay;
        }

        if(gameObject.transform.position.y < -3.8f)     // policyjny podjezdza na pozycje gracza
        {
            gameObject.transform.Translate(new Vector3(0, 1, 0) * policeCarVerticalSpeed * Time.deltaTime);
        } else
        {
            shootDelay -= Time.deltaTime;               // jak podjechal to zaczyna strzelac
            if (shootDelay <= 0 && GameObject.FindWithTag("Untouchable") == false)
            {
                StartCoroutine("Shoot");
                shootDelay = shootingSeriesDelay;
            }
        }
    }

    IEnumerator Shoot()
    {
        for (int i = bulletsInSeries; i > 0; i--)
        {
            audio.PlayOneShot(shotSound, 0.5f);
            bulletObj = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            if (isLeft == true)
            {
                bulletObj.GetComponent<Bullet>().direction = 1;    // strzelaj w dobra strone
            } else if (isLeft == false)
            {
                bulletObj.GetComponent<Bullet>().direction = -1;
            }
            yield return new WaitForSeconds(singleShotDelay);
        }
    }

    private void OnCollisionEnter2D(Collision2D obj)        // niszczenie na barierce
    {
        if(obj.gameObject.tag == "Barrier")
        {
            if(isLeft == true)
            {
                WaveManager.isLeft = false;
            } else if (isLeft == false)
            {
                WaveManager.isRight = false;
            }
            PointsManager.points += pointsPerCar;
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
