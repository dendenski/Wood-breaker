using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExtraBallManager : MonoBehaviour
{
    private BallControl ballControl;
    private GameManager gameManager;
    public float ballWaitTime;
    private float ballWaitTimeSeconds;
    public int numberOfExtraBalls;
    public int numberOfBallsToFire;
    public ObjectPool objectPool;
    public Text numberOFBallsText;
    public SpecialItemManager specialItemManager;
    void Start()
    {
        specialItemManager = FindObjectOfType<SpecialItemManager>();
        ballControl = FindObjectOfType<BallControl>();
        gameManager = FindObjectOfType<GameManager> ();
        ballWaitTimeSeconds = ballWaitTime;
        numberOfExtraBalls = 1;
        numberOfBallsToFire = 1;
    }
    void Update()
    {
        numberOFBallsText.text = "x" + (numberOfBallsToFire);
        numberOFBallsText.transform.position = new Vector2( ballControl.transform.position.x, numberOFBallsText.transform.position.y);
        if(ballControl.currentBallState == BallControl.ballState.fire ||
            ballControl.currentBallState == BallControl.ballState.wait){
            if(numberOfBallsToFire > 0){
                ballWaitTimeSeconds -=Time.deltaTime;
                if(ballWaitTimeSeconds <= 0){
                    GameObject ball = objectPool.GetPooledObject("Extra Ball");
                    if(ball != null){
                        if(specialItemManager.damage == 2){
                            ball.GetComponent<SpriteRenderer>().color = new Color(1f,0f,0f);
                        }else if(specialItemManager.damage == 1){
                            ball.GetComponent<SpriteRenderer>().color = new Color(0.9716981f,0.8459923f,0.7196066f);
                        }
                        ball.transform.position = ballControl.ballLaunchPosition;
                        ball.SetActive (true);
                        gameManager.ballsInScene.Add(ball);
                        ball.GetComponent<Rigidbody2D>().velocity = ballControl.constantSpeed * ballControl.tempVelocity;
                        ballWaitTimeSeconds = ballWaitTime;
                        numberOfBallsToFire--;
                    }
                    ballWaitTimeSeconds = ballWaitTime;
                }
            }
        }
        if(ballControl.currentBallState == BallControl.ballState.endShot){
            numberOfBallsToFire = numberOfExtraBalls;
        }
    }
}
