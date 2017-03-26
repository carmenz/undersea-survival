using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text scoreText;
	public Text gameOverText;
	public Text timerText;
	private bool started = false;
	private int count;
	private int goal = 20; //kill 3 fishes
	private float timeNow = 0f;
	private float timeLimit = 30f; //60 sec to play
	public GameObject startFishManager;
	public GameObject mainMenu;
	public GameObject About;

	//link to buttonInteraction:
	private ButtonInteraction startScript;
	private ButtonInteraction aboutScript;
	private fishglobal fishManagerScript;
	private bool aboutClicked = false;

	//audio:
	private AudioSource soundSource;
	private bool soundPlayed = false;

//	void Awake() {
//		DontDestroyOnLoad(transform.gameObject);
//	}

	void Start () {

		//link to object scripts:
		startScript = GameObject.Find("MainMenu/MainCanvas/StartButton").GetComponent<ButtonInteraction>();
		aboutScript = GameObject.Find("MainMenu/MainCanvas/AboutButton").GetComponent<ButtonInteraction>();
		fishManagerScript = GameObject.Find("fishManager").GetComponent<fishglobal>();

		//preset objects:
		mainMenu.SetActive(true);
		About.SetActive(false);
		startFishManager.SetActive(true);

		//initiate variables:
		started = startScript.startGame;
		aboutClicked = aboutScript.startGame;

		//set variables:
		reset ();

		//set audio:
		soundSource = GetComponent<AudioSource>();
	}
		
	void Update () {

		if (!started) {	//waiting to start: 
			started = startScript.startGame;
			if (started) {	//menu start button clicked:
				//disable menu:
				mainMenu.SetActive (false);
				About.SetActive (false);
				RenderSettings.ambientIntensity = 1.3f;
				//start putting fish:
				fishManagerScript.startCreateFish = true;
				reset ();
			}
		}

		if (started) {	//menu start button clicked:
			timeNow = timeNow + Time.deltaTime;
			if (timeNow <= timeLimit) {
				timerText.text = "Time Left: " + (timeLimit - timeNow).ToString ("0.0");
			}

			if (timeNow > (timeLimit - 5f) && timeNow < timeLimit && !soundPlayed) {
				soundPlayed = true;
				soundSource.Play();

			}

			if ((timeNow >= timeLimit) && (count < goal)) {
				//game over:
				endGame ();
				//play audio:
				soundSource.Stop();
				soundPlayed = false;
			}
		}

		if (aboutClicked) {	//menu about is clicked:
			About.SetActive (true);
			aboutClicked = false;
			aboutScript.Start ();
		} else {
			aboutClicked = aboutScript.startGame;
		}
	}

	public void AddOne(){
		count++;
		SetScorceText ();
	}

	void reset(){
		timeNow = 0f;
		count = 0;
		SetScorceText ();
		gameOverText.text = "";

	}

	void endGame(){
		started = false;
		gameOverText.text = "Game Over :(";
		startScript.Start ();

		//clean fish:
		fishManagerScript.Reset ();

		//enable menu:
		mainMenu.SetActive (true);

		RenderSettings.ambientIntensity = 0f;

	}

	void SetScorceText (){
		scoreText.text = "Score: " + count.ToString () + " /20";

		//check if game goal achived:
		if (count >= goal) {
			started = false;
			count = 0;
			gameOverText.text = "You Won!";
			startScript.Start ();
			soundSource.Stop();
			soundPlayed = false;
			//clean fish:
			fishManagerScript.Reset ();

			//enable menu:
			mainMenu.SetActive(true);
		}
	}
}
