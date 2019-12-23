using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
	public GameObject GameOverPanel;
	public Text scoreTxt;
    public Text highScoreTxt;
	public Text distanceTxt;
	public Text speedTxt;

	//Global Values
	public static float distance = 0f;
	public static float speed = 5f;

	private CharacterLogic characterLogic;
	private EnemyGenerator enemyGenerator;
	private GemGenerator gemGenerator;
	private AudioLogic audioLogic;

	private float score = 0;
	private float highScore = 0;

	private float incValue = 100;
	private float coinValue = 1;
	private float gemValue = 10;

    private void Start()
    {
		Time.timeScale = 1;

		score = 0;
		distance = 0;
		speed = 5;

		characterLogic = FindObjectOfType<CharacterLogic>();
		enemyGenerator = FindObjectOfType<EnemyGenerator>();
		gemGenerator = FindObjectOfType<GemGenerator>();
		audioLogic = FindObjectOfType<AudioLogic>();

		if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetFloat("HighScore", 0);
        }

		GameOverPanel.SetActive(false);
	}


    private void FixedUpdate()
    {
		speed = Mathf.Floor(distance / incValue) + 5f;
		float inc = speed * Time.deltaTime;

		distance += inc;
		score += inc;

        if (score > highScore)
        {
            highScore = score;
        }

		scoreTxt.text = "Score: " + Mathf.Round(score);
        highScoreTxt.text = "Highscore: " + Mathf.Round(highScore);
		distanceTxt.text = "Distance: " + Mathf.Round(distance) + " m";
		speedTxt.text = "Speed: " + Mathf.Round(speed) + " m/s";
	}

	public void EatCoin()
	{
		score += speed * coinValue;
	}

	public void EatGem()
	{
		score += speed * gemValue;
	}

	public void Lose()
	{
		GameOverPanel.SetActive(true);
		Time.timeScale = 0;
		if (score >= highScore)
		{
			PlayerPrefs.SetFloat("HighScore", highScore);
		}
		audioLogic.StopBGM();
	}

	public void Restart()
	{
		GameOverPanel.SetActive(false);
		Time.timeScale = 1;

		score = 0;
		distance = 0;
		speed = 5;

		characterLogic.Restart();
		enemyGenerator.Restart();
		gemGenerator.Restart();
		audioLogic.StartBGM();
	}

	public void Menu()
	{
		SceneManager.LoadScene("Menu");
	}
}
