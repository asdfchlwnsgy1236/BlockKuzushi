using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager: MonoBehaviour {
	public static GameManager instance = null;
	public GameObject wallsPrefab, deadZonesPrefab, bricksPrefab, paddlePrefab;
	public GameObject walls, deadZones, bricks, paddle;
	public List<GameObject> balls = new List<GameObject>();
	public Text bricksText, ballsText, livesText, victoryText, defeatText;
	public int brickCount = 0, ballCount = 0, lives = 0;

	// Awake is called when the script instance is being loaded
	private void Awake() {
		if(instance == null) {
			instance = this;
		}
		else {
			if(instance != this) {
				Destroy(gameObject);
			}
		}
	}

	/// <summary>
	/// BallLost is called upon the loss of a ball.
	/// </summary>
	public void BallLost() {
		ballCount--;
		UpdateBallCount();
		CheckBalls();
	}

	/// <summary>
	/// BrickBroken is called upon the destruction of a brick.
	/// </summary>
	public void BrickBroken() {
		brickCount--;
		UpdateBrickCount();
		CheckGameOver();
	}

	/// <summary>
	/// CheckBalls is called upon the loss of a ball to determine whether no balls remain.
	/// If no balls remain, then a life is lost.
	/// </summary>
	private void CheckBalls() {
		if(ballCount < 1) {
			lives--;
			UpdateLives();
			Destroy(paddle);
			CheckGameOver();
		}
	}

	/// <summary>
	/// CheckGameOver is called upon the destruction of a brick or the loss of a life.
	/// If no bricks remain, it shows the victory scene.
	/// If no lives remain, it shows the defeat scene.
	/// </summary>
	private void CheckGameOver() {
		if(brickCount < 1) {
			victoryText.gameObject.SetActive(true);
			Time.timeScale = 0.1f;
			Invoke("ResetLevel", 0.2f);
		}
		else if(lives < 1) {
			defeatText.gameObject.SetActive(true);
			Time.timeScale = 0.1f;
			Invoke("ResetLevel", 0.2f);
		}
		else if(ballCount < 1) {
			Invoke("CreatePaddle", 2.0f);
		}
	}

	/// <summary>
	/// CleanUpLevel is called upon the end of a level as part of the ResetLevel function.
	/// </summary>
	public void CleanUpLevel() {
		Destroy(walls);
		Destroy(deadZones);
		Destroy(bricks);
		Destroy(paddle);
		foreach(var ball in balls) {
			Destroy(ball);
		}
		balls.Clear();
		bricksText.gameObject.SetActive(false);
		ballsText.gameObject.SetActive(false);
		livesText.gameObject.SetActive(false);
		victoryText.gameObject.SetActive(false);
		defeatText.gameObject.SetActive(false);
	}

	/// <summary>
	/// CreatePaddle is called whenever a new paddle is required.
	/// </summary>
	private void CreatePaddle() {
		paddle = Instantiate(paddlePrefab) as GameObject;
		ballCount++;
		UpdateBallCount();
	}

	/// <summary>
	/// ResetLevel is called upon the end of a level to restart it.
	/// </summary>
	public void ResetLevel() {
		Time.timeScale = 1.0f;
		CleanUpLevel();
		Invoke("SetupLevel", 1.0f);
	}

	/// <summary>
	/// SetupLevel is called upon the start of a level.
	/// </summary>
	public void SetupLevel() {
		walls = Instantiate(wallsPrefab) as GameObject;
		deadZones = Instantiate(deadZonesPrefab) as GameObject;
		bricks = Instantiate(bricksPrefab) as GameObject;
		CreatePaddle();
		brickCount = 12;
		lives = 1;
		UpdateBrickCount();
		UpdateBallCount();
		UpdateLives();
		bricksText.gameObject.SetActive(true);
		ballsText.gameObject.SetActive(true);
		livesText.gameObject.SetActive(true);
		victoryText.gameObject.SetActive(false);
		defeatText.gameObject.SetActive(false);
	}

	// Start is called just before any of the Update methods is called the first time
	private void Start() {
		SetupLevel();
	}

	/// <summary>
	/// UpdateBallCount is called upon a change in the number of balls.
	/// </summary>
	private void UpdateBallCount() {
		ballsText.text = "Balls: " + ballCount;
	}

	/// <summary>
	/// UpdateBrickCount is called upon a change in the number of bricks.
	/// </summary>
	private void UpdateBrickCount() {
		bricksText.text = "Bricks: " + brickCount;
	}

	/// <summary>
	/// UpdateLives is called upon a change in the number of lives.
	/// </summary>
	private void UpdateLives() {
		livesText.text = "Lives: " + lives;
	}
}
