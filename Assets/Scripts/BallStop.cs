using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStop : MonoBehaviour
{
    public Rigidbody2D ball;
    public BallControl ballControl;
    private GameManager gameManager;
    public GameObject firstBalltoLand;
    public bool isFirstBallLanded;
    // Start is called before the first frame update
    void Start()
    {
        isFirstBallLanded = false;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Extra Ball"){
            gameManager.ballsInScene.Remove(other.gameObject);
            other.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.9716981f,0.8459923f,0.7196066f);
            if(isFirstBallLanded){
                other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                other.transform.position = new Vector2(other.transform.position.x, -6.74f);
                other.gameObject.GetComponent<BallMovement>().isBallStopped = true;
                other.gameObject.GetComponent<BallMovement>().targetPosition = firstBalltoLand.transform.position;
                StartCoroutine(other.gameObject.GetComponent<BallMovement>().BallMoveToNewPosition());
            }
            else
            {
                isFirstBallLanded = true;
                other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                other.transform.position = new Vector2(other.transform.position.x, -6.74f);
                firstBalltoLand = other.gameObject;
                ballControl.currentBallState = BallControl.ballState.wait;
            }
        }
    }


}
