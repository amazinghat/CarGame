using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	public Text gainedPointsText;
	public Text extraLifesBonusText;
	public Text noCollisionBonusText;
	public Text altogetherPointsText;

	public int everyExtraLifeBonus;
	public int noCollisionBonus;

	private GameObject GameManager;
	private GameObject PlayerCar;

	void Start() {
		gainedPointsText.text = PointsManager.points.ToString();

		GameManager = GameObject.Find("GameManager");
		extraLifesBonusText.text = (GameManager.GetComponent<CarDurabilityManager>().lifes * everyExtraLifeBonus).ToString();

		if ((PlayerCar = GameObject.FindWithTag("Player")) != null) {
			if (PlayerCar.GetComponent<PlayerCarMovement>().durability == PlayerCar.GetComponent<PlayerCarMovement>().maxDurability) {
				noCollisionBonusText.text = noCollisionBonus.ToString();
			}
		}
		altogetherPointsText.text = (int.Parse(gainedPointsText.text) + int.Parse(extraLifesBonusText.text) + int.Parse(noCollisionBonusText.text)).ToString();
	}

	public void RetryButton() {
		SceneManager.LoadScene(0);
	}

	public void MenuExitButton() {
		SceneManager.LoadScene(1);
	}
}
