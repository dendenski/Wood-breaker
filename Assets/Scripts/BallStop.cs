using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStop : MonoBehaviour
{
    public Rigidbody2D ball;
    public BallControl ballControl;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ball"){
            //stop the ball
            ball.velocity = Vector2.zero;
            other.transform.position = new Vector2(other.transform.position.x, -6.74f);
            ballControl.currentBallState = BallControl.ballState.wait;
        }
        if(other.gameObject.tag == "Extra Ball"){
            gameManager.ballsInScene.Remove (other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
