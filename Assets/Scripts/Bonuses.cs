using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonuses : MonoBehaviour {

    public float directionBonus = -0.7f;

    [Header("Type of bonus")]
    public bool isDurability;
    public bool isShield;
    public bool isSpeed;
    public bool isHole;

    [Header("Bonuses Settings")]
    public float bonusSpeed = 10f;

    [Header("Durability Settings")]
    public float repairPoints;

    [Header("Hole Settings")]
    public float damagePoints;

    [Header("Shield Settings")]
    public GameObject shield;
    private GameObject playerCar;
    private Vector3 playerCarPos;

    [Header("Speed Settings")]
    public float speedBoost;
    public float duration;
    private bool isActivated = false;

    void Update()
    {
        this.gameObject.transform.Translate(new Vector3(0, directionBonus, 0) * bonusSpeed * Time.deltaTime);   // ruch bonusów z drogą
    }

    private void OnTriggerEnter2D(Collider2D obj)   // rozroznienie działania bonusow przy zebraniu
    {
        if (obj.gameObject.tag == "Player" || obj.gameObject.tag == "Shield")
        {
            if (isDurability == true)
            {
                obj.gameObject.GetComponent<PlayerCarMovement>().durability += repairPoints;
                Destroy(this.gameObject);
            } else if (isShield == true)
            {
                playerCar = GameObject.FindWithTag("Player");
                obj.gameObject.tag = "Shield";                  // zmiana taga zeby najpierw zbijalo tarcze
                playerCarPos = playerCar.transform.position;
                playerCarPos.z = -0.1f;                         // tarcza troche wyzej niz auto
                GameObject shieldObj = Instantiate(shield, playerCarPos, Quaternion.identity);  // zespawnuj tarcze w miejscu gracza
                shieldObj.transform.parent = playerCar.transform;   // zmiana rodzica zeby tarcza poruszala sie z graczem
                Destroy(this.gameObject);
            } else if (isSpeed == true)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;  // zamiast niszczenia obiektu ukrywany go (bo zniszczy dopiero korutyna na koniec bonusu)
                isActivated = true;
                StartCoroutine("SpeedBoostActivated");  // korutyna do speedboosta
            } else if (isHole == true)
            {
                obj.gameObject.GetComponent<PlayerCarMovement>().durability -= damagePoints;
                Destroy(this.gameObject);
            }
        } else if(obj.gameObject.tag == "EndOfTheRoad" && isActivated == false)      // niezebrane bonusy czyscimy na colliderze za mapa
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator SpeedBoostActivated()
    {
        while(duration > 0)
        {
            duration -= Time.deltaTime / speedBoost;    // to nie auto jedzie do przodu, tylko otoczenie sie porusza!
            Time.timeScale = speedBoost;                //  timeScale odpowiada za szybkosc czasu gry, 1 to default
            yield return null;
        }
        Time.timeScale = 1f;                            // koniec boosta -> powrot do normalnego czasu
        Destroy(this.gameObject);
    }
}
