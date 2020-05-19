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
    private float waitTime = 15.0f;
    private float timer = 0.0f;
    float boostSpeed = 2.0f;
    bool boostSpeedFlag = true;
    // Start is called before the first frame update
    void Start()
    {
        ballControl = FindObjectOfType<BallControl>();
        gameManager = FindObjectOfType<GameManager> ();
        ballWaitTimeSeconds = ballWaitTime;
        numberOfExtraBalls = 0;
        numberOfBallsToFire = 0;
        boostSpeedFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if((numberOfExtraBalls+1) < 10){
            numberOFBallsText.text = "0000" + (numberOfExtraBalls + 1);
        }
        else if((numberOfExtraBalls+1) < 100){
            numberOFBallsText.text = "000" + (numberOfExtraBalls + 1);
        }
        else if((numberOfExtraBalls+1) < 1000){
            numberOFBallsText.text = "00" + (numberOfExtraBalls + 1);
        }
        else if((numberOfExtraBalls+1) < 10000){
            numberOFBallsText.text = "0" + (numberOfExtraBalls + 1);
        }
        timer += Time.deltaTime;
        if ((timer > waitTime) && boostSpeedFlag)
        {
                gameManager.ballsInScene.ForEach(c => c.GetComponent<Rigidbody2D>().velocity 
                    = boostSpeed * c.GetComponent<Rigidbody2D>().velocity);
                boostSpeedFlag = false;
        }
        if(ballControl.currentBallState == BallControl.ballState.fire ||
            ballControl.currentBallState == BallControl.ballState.wait){
            if(numberOfBallsToFire > 0){
                ballWaitTimeSeconds -=Time.deltaTime;
                if(ballWaitTimeSeconds <= 0){
                    GameObject ball = objectPool.GetPooledObject("Extra Ball");
                    if(ball != null){
                        ball.transform.position = ballControl.ballLaunchPosition;
                        ball.SetActive (true);
                        gameManager.ballsInScene.Add(ball);
                        ball.GetComponent<Rigidbody2D>().velocity = 18 * ballControl.tempVelocity;
                        ballWaitTimeSeconds = ballWaitTime;
                        numberOfBallsToFire--;
                    }
                    ballWaitTimeSeconds = ballWaitTime;
                }
            }
        }

        if(ballControl.currentBallState == BallControl.ballState.endShot){
            numberOfBallsToFire = numberOfExtraBalls;
            timer = 0.0f;
            boostSpeedFlag = true;
        }
    }
}
