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

	private int score;
	private int[] highScoresArray = new int[10];

	void Start() {
		highScoresArray = PlayerPrefsX.GetIntArray("HighScoreArray");
		gainedPointsText.text = PointsManager.points.ToString();

		GameManager = GameObject.Find("GameManager");
		extraLifesBonusText.text = (GameManager.GetComponent<CarDurabilityManager>().lifes * everyExtraLifeBonus).ToString();

		if ((PlayerCar = GameObject.FindWithTag("Player")) != null) {
			if (PlayerCar.GetComponent<PlayerCarMovement>().durability == PlayerCar.GetComponent<PlayerCarMovement>().maxDurability) {
				noCollisionBonusText.text = noCollisionBonus.ToString();
			}
		}
		altogetherPointsText.text = (int.Parse(gainedPointsText.text) + int.Parse(extraLifesBonusText.text) + int.Parse(noCollisionBonusText.text)).ToString();

		score = int.Parse(altogetherPointsText.text);
		if (score > highScoresArray[9]) {
			for (int i = 0; i < 10; i++) {
				if (score > highScoresArray[i]) {
					for (int j = 9; j > i; j--) {
						highScoresArray[j] = highScoresArray[j - 1];
					}
					highScoresArray[i] = score;
					break;
				}
			}
		}
		PlayerPrefsX.SetIntArray("HighScoreArray", highScoresArray);
	}

	public void RetryButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
	}

	public void MenuExitButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
	}
}
