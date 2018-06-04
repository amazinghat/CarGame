using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour {

    static public int points;
    private float secondDelay = 1;

	void Start () {
        points = 0;
        this.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Points";
        this.gameObject.GetComponent<TextMesh>().color = new Color(1f, 1f, 1f, 0.7f);
	}

    private void Update()
    {
        this.gameObject.GetComponent<TextMesh>().text = points.ToString();
        secondDelay -= Time.deltaTime;
        if (secondDelay <= 0)
        {
            points += 1;
            secondDelay = 1;
        }
        if (points < 0)
        {
            points = 0;
        }
    }
}
