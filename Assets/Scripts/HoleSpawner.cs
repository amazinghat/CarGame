using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSpawner : MonoBehaviour {

    public GameObject hole;
    public int minStart;
    public int maxStart;

    private float[] PositionArray;

    private float delay;

    void Start()
    {
        PositionArray = new float[2];
        PositionArray[0] = -2.4f;
        PositionArray[1] = 2.4f;
        delay = Random.Range(minStart, maxStart);

    }

    void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0)
        {
            delay = Random.Range(minStart, maxStart);
            SpawnHole();
        }
    }

    void SpawnHole()
    {
        int pos = Random.Range(0, 2);
        if (pos == 0)
        {
            GameObject holeleft = (GameObject)Instantiate(hole, new Vector3(PositionArray[pos], 6f, 0), Quaternion.Euler(new Vector3(0, 0, 180)));
            holeleft.GetComponent<Bonuses>().directionBonus = 1;
        }
        if (pos == 1)
        {

            Instantiate(hole, new Vector3(PositionArray[pos], 6f, 0), Quaternion.identity);
        }

    }
    
}
