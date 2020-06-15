using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndGameManager : MonoBehaviour
{
    private BallControl ball;
    private GoogleMobileAdsDemoScript sampleAds;
    public GameObject endGamePanel;
    public Button stopButton;
    public Button balls2xButton;
    public Button damage2xButton;
    public Button halfLifeButton;
    public Text menuText;
    public GameManager gameManager;
    public Button resumeButton;
    private ScoreManager score;
    private int soundState;
    private int musicState;
    public GameObject soundToggle;
    public GameObject musicToggle;
    public bool isPaused;
    void Start()
    {
        isPaused = false;
        gameManager = FindObjectOfType<GameManager>();
        sampleAds = FindObjectOfType<GoogleMobileAdsDemoScript>();
        ball = FindObjectOfType<BallControl>();
        score = FindObjectOfType<ScoreManager>();
        endGamePanel.SetActive (false);
        soundState = PlayerPrefs.GetInt ("soundOption");
        musicState = PlayerPrefs.GetInt ("musicOption");
        if(soundState == 0){
            soundToggle.GetComponent<Toggle>().isOn = true;
        }
        else if(soundState == 1){
            soundToggle.GetComponent<Toggle>().isOn = false;
        }
        if(musicState == 0){
            musicToggle.GetComponent<Toggle>().isOn = true;
        }
        else if(musicState == 1){
            musicToggle.GetComponent<Toggle>().isOn = false;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(isPaused && ball.currentBallState != BallControl.ballState.endGame){
                isPaused = false;
                resumeButton.gameObject.SetActive (false);
                stopButton.interactable = true;
                balls2xButton.interactable = true;
                damage2xButton.interactable = true;
                halfLifeButton.interactable = true;
                endGamePanel.SetActive (false);
                Time.timeScale = 1;
                ball.currentBallState = ball.tempBallState;
            }
            else if(!isPaused  && ball.currentBallState != BallControl.ballState.endGame){
                menuText.text = "PAUSED";
                ball.tempBallState = ball.currentBallState;
                ball.currentBallState = BallControl.ballState.pause;
                isPaused = true;
                resumeButton.gameObject.SetActive (true);
                stopButton.interactable = false;
                balls2xButton.interactable = false;
                damage2xButton.interactable = false;
                halfLifeButton.interactable = false;
                endGamePanel.SetActive (true);
                Time.timeScale = 0;
            }
        }
        if(soundToggle.GetComponent<Toggle>().isOn == true){
            soundState = 0;
            PlayerPrefs.SetInt ("soundOption", soundState);
            PlayerPrefs.Save();
        }
        else if(soundToggle.GetComponent<Toggle>().isOn == false){
            soundState = 1;
            PlayerPrefs.SetInt ("soundOption", soundState);
            PlayerPrefs.Save();
        }
        if(musicToggle.GetComponent<Toggle>().isOn == true){
            musicState = 0;
            PlayerPrefs.SetInt ("musicOption", musicState);
            PlayerPrefs.Save();
        }
        else if(musicToggle.GetComponent<Toggle>().isOn == false){
            musicState = 1;
            PlayerPrefs.SetInt ("musicOption", musicState);
            PlayerPrefs.Save();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Square Brick" || other.gameObject.tag == "Triangle Brick"){
            ball.currentBallState = BallControl.ballState.endGame;
            score.SetHighScore();
            menuText.text = "GAME OVER!";
            stopButton.interactable = false;
            balls2xButton.interactable = false;
            damage2xButton.interactable = false;
            halfLifeButton.interactable = false;
            resumeButton.gameObject.SetActive (false);
            endGamePanel.SetActive (true);
        }
        if(other.gameObject.tag == "Extra Ball Up"){
            other.gameObject.SetActive(false);
            gameManager.bricksInScene.Remove(other.gameObject);
        }
        if(other.gameObject.tag == "Extra Ball Up"){
            other.gameObject.SetActive(false);
            gameManager.bricksInScene.Remove(other.gameObject);
        }
    }
    public void Retry(){
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
    }
    public void Quit(){
        sampleAds.destroyAds();
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    public void Resume(){
        if(isPaused && ball.currentBallState != BallControl.ballState.endGame){
            isPaused = false;
            resumeButton.gameObject.SetActive (false);
            stopButton.interactable = true;
            balls2xButton.interactable = true;
            damage2xButton.interactable = true;
            halfLifeButton.interactable = true;
            Time.timeScale = 1;
            endGamePanel.SetActive (false);
        }
    }
}
