using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public enum ballState
    {
        aim,
        fire,
        wait,
        endShot,
        endGame,
        pause
    }
    public ballState currentBallState;
    public ballState tempBallState;
    public Rigidbody2D ball;
    private Vector2 mouseStartPosition;
    private Vector2 mouseEndPosition;
    public Vector3 ballLaunchPosition;
    public Vector2 tempVelocity;
    private float ballVelocityX;
    private float ballVelocityY;
    public float constantSpeed;
    public GameObject arrow;
    public GameManager gameManager;

    public EndGameManager endGameManager;
    private bool isClicked;
    void Start()
    {
        isClicked = false;
        endGameManager = FindObjectOfType<EndGameManager>();
        gameManager = FindObjectOfType<GameManager>();
        currentBallState = ballState.aim;
        gameManager.ballsInScene.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentBallState)
        {
            case ballState.aim:
                MouseInput();
                TouchInput2();
                break;
            case ballState.fire:
                break;
            case ballState.wait:
                if(gameManager.ballsInScene.Count == 1){
                    currentBallState = ballState.endShot;
                }
                break;
            case ballState.endShot:
                for(int i = 0; i < gameManager.bricksInScene.Count; i++)
                {
                    gameManager.bricksInScene[i].GetComponent<BrickMovementControl>().currentState 
                        = BrickMovementControl.brickState.move;
                }
                constantSpeed = 18.0f;
                ColliderChangeEnable(true);
                gameManager.PlaceBricks();
                currentBallState = ballState.aim;
                break;
            case ballState.endGame:
                break;
            case ballState.pause:
                if(endGameManager.isPaused == false){
                    currentBallState = tempBallState;
                }
                break;
            default:
                break;
        }
    }
    public void MouseInput(){
        Vector2 playPosition =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if(Input.GetMouseButtonDown(0) && playArea(playPosition)){
            MouseClicked();
        }
        if(Input.GetMouseButton(0) && playArea(playPosition) && isClicked){
            Vector2 tempMousePosition =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MouseDragged(tempMousePosition);
        }else if(Input.GetMouseButton(0) && !playArea(playPosition) && isClicked){
            arrow.SetActive(false);
        }
        if(Input.GetMouseButtonUp(0) && playArea(playPosition)  && isClicked){
            mouseEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ReleaseMouse ();
        }else if(Input.GetMouseButtonUp(0) && !playArea(playPosition)  && isClicked){
            arrow.SetActive(false);
        }
    }
    private bool playArea(Vector2 playPosition){
        if(playPosition.x > -4.0f && playPosition.x < 4.0f &&
           playPosition.y > -7.0f && playPosition.y < 7.0f){
            return true;
           }
        else{
            return false;
        }
    }

    public void MouseClicked(){
        mouseStartPosition = this.transform.position;
        isClicked = true;
    }

    public void MouseDragged(Vector2 tempMousePosition){
        float hypotenuse = 0.0f;
        arrow.SetActive(true);
        
        float diffX = mouseStartPosition.x - tempMousePosition.x;
        float diffY = Mathf.Abs(mouseStartPosition.y - tempMousePosition.y);
        if(diffY <= 0){
            diffY = 0.001f;
        }
        float theta = Mathf.Rad2Deg * Mathf.Atan(diffX/diffY);
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, theta);
        
        hypotenuse = Mathf.Sqrt((diffY*diffY) + (diffX*diffX));

        arrow.transform.localScale = new Vector2(1, hypotenuse *.2f);
    }

    public void ReleaseMouse(){
        arrow.SetActive(false);
        isClicked = false;
        ballVelocityX = mouseStartPosition.x - mouseEndPosition.x;
        ballVelocityY = Mathf.Abs(mouseStartPosition.y - mouseEndPosition.y);
        tempVelocity = new Vector2(-ballVelocityX, ballVelocityY).normalized;
        
        ball.velocity = constantSpeed * tempVelocity;
        if(ball.velocity == Vector2.zero){
            return;
        }
        ballLaunchPosition = transform.position;
        currentBallState = ballState.fire;
    }
    public void TouchInput2(){
        if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            Vector2 playPosition =  Camera.main.ScreenToWorldPoint(touch.position);
            if(touch.phase == TouchPhase.Began && playArea(playPosition)){
                mouseStartPosition = this.transform.position;
                isClicked = true;
            }

            if(touch.phase == TouchPhase.Moved && playArea(playPosition) && isClicked){
                float hypotenuse = 0.0f;
                arrow.SetActive(true);
                Vector2 tempMousePosition = Camera.main.ScreenToWorldPoint(touch.position);
                float diffX = mouseStartPosition.x - tempMousePosition.x;
                float diffY = Mathf.Abs(mouseStartPosition.y - tempMousePosition.y);
                if(diffY <= 0){
                    diffY = 0.001f;
                }
                float theta = Mathf.Rad2Deg * Mathf.Atan(diffX/diffY);
                hypotenuse = Mathf.Sqrt((diffY*diffY) + (diffX*diffX));
                arrow.transform.rotation = Quaternion.Euler(0f, 0f, theta);
                arrow.transform.localScale = new Vector2(1, hypotenuse *.2f);
            }else if(touch.phase == TouchPhase.Moved && !playArea(playPosition) && isClicked){
                arrow.SetActive(false);
            }

            if(touch.phase == TouchPhase.Ended && playArea(playPosition)  && isClicked){
                arrow.SetActive(false);
                isClicked = false;
                mouseEndPosition = Camera.main.ScreenToWorldPoint(touch.position);
                ballVelocityX = mouseStartPosition.x - mouseEndPosition.x;
                ballVelocityY = Mathf.Abs(mouseStartPosition.y - mouseEndPosition.y);
                tempVelocity = new Vector2(-ballVelocityX, ballVelocityY).normalized;
                ball.velocity = constantSpeed * tempVelocity;
                if(ball.velocity == Vector2.zero){
                    return;
                }
                ballLaunchPosition = transform.position;
                currentBallState = ballState.fire;

            }else if(touch.phase == TouchPhase.Ended && !playArea(playPosition) && isClicked){
                arrow.SetActive(false);
            }
        }

        
    }

    public void ballDown(){
        tempVelocity = new Vector2(0, -ballVelocityY).normalized;
        if(currentBallState == ballState.fire || currentBallState == ballState.wait){
            gameManager.ballsInScene.ForEach(c => c.GetComponent<Rigidbody2D>().velocity 
                        = constantSpeed * tempVelocity*2);
            ColliderChangeEnable(false);

            ball.velocity = constantSpeed *tempVelocity*2;  
        }

    }

    private void ColliderChangeEnable(bool status){
        for(int i = 0; i < gameManager.bricksInScene.Count; i++)
        {
            if(gameManager.bricksInScene[i].tag =="Square Brick"){
                gameManager.bricksInScene[i].GetComponent<BoxCollider2D>().enabled 
                    = status;
            }else if(gameManager.bricksInScene[i].tag =="Triangle Brick")
            {
                gameManager.bricksInScene[i].GetComponent<PolygonCollider2D>().enabled 
                    = status;
            }
        }
    }
}
