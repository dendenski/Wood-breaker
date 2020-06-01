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
     
    public Button resumeButton;
    private ScoreManager score;
    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        sampleAds = FindObjectOfType<GoogleMobileAdsDemoScript>();
        ball = FindObjectOfType<BallControl>();
        score = FindObjectOfType<ScoreManager>();
        endGamePanel.SetActive (false);
    }

    // Update is called once per frame
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
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Square Brick" || other.gameObject.tag == "Triangle Brick"){
            ball.currentBallState = BallControl.ballState.endGame;
            score.SetHighScore();
            
            menuText.text = "GAME OVER!";
            stopButton.interactable = false;
            resumeButton.gameObject.SetActive (false);
            endGamePanel.SetActive (true);
        }
        if(other.gameObject.tag == "Extra Ball Up"){
            other.gameObject.SetActive(false);
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
