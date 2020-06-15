using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public GameManager gameManager;
    public SpecialItemManager specialItemManager;
    public EndGameManager endGameManager;
    private bool isClicked;
    LineRenderer lineRenderer;
    public Vector2 targetPositionOfStopButton;
    public Vector2 initialPositionOfStopButton;
    public Button stopButton;
    public GameObject specialItem;
    void Start()
    {
        targetPositionOfStopButton = new Vector2(0f,-9f);
        initialPositionOfStopButton = new Vector2(0f,-13f);
        constantSpeed = 20.0f;
        isClicked = false;
        specialItemManager = FindObjectOfType<SpecialItemManager>();
        lineRenderer = this.GetComponent<LineRenderer>();
        endGameManager = FindObjectOfType<EndGameManager>();
        gameManager = FindObjectOfType<GameManager>();
        currentBallState = ballState.aim;
        lineRenderer.enabled =false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            ballDown();
        }
        switch(currentBallState)
        {
            case ballState.aim:
                MouseInput();
                TouchInput2();
                if(specialItemManager.damage == 2){
                    this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,0f,0f);
                }else if(specialItemManager.damage == 1){
                    this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.9716981f,0.8459923f,0.7196066f);
                }
                stopButton.interactable = true;
                stopButton.transform.position = Vector2.MoveTowards(stopButton.transform.position, initialPositionOfStopButton,  20 * Time.deltaTime);
                specialItem.transform.position = Vector2.MoveTowards(specialItem.transform.position, new Vector2(0f,0f),  20 * Time.deltaTime);
                break;
            case ballState.fire:
                stopButton.transform.position = Vector2.MoveTowards(stopButton.transform.position, targetPositionOfStopButton,  20 * Time.deltaTime);
                specialItem.transform.position = Vector2.MoveTowards(specialItem.transform.position, new Vector2(0f,-2.5f),  20 * Time.deltaTime);
                break;
            case ballState.wait:
                if(gameManager.ballsInScene.Count == 0){
                    StopAllCoroutines();
                    currentBallState = ballState.endShot;
                }
                break;
            case ballState.endShot:
                constantSpeed = 20.0f;
                
                currentBallState = ballState.aim;
                for(int i = 0; i < gameManager.bricksInScene.Count; i++)
                {
                    gameManager.bricksInScene[i].GetComponent<BrickMovementControl>().currentState 
                        = BrickMovementControl.brickState.move;
                }
                ColliderChangeEnable(true);
                gameManager.PlaceBricks();
                transform.position = FindObjectOfType<BallStop>().firstBalltoLand.transform.position;
                FindObjectOfType<BallStop>().firstBalltoLand.GetComponent<BallMovement>().firstBall = false;
                FindObjectOfType<BallStop>().firstBalltoLand.SetActive(false);
                FindObjectOfType<BallStop>().isFirstBallLanded = false;
                FindObjectOfType<BallStop>().firstBalltoLand = null;
                specialItemManager.BallsNormalize();
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
            lineRenderer.enabled =false;
        }
        if(Input.GetMouseButtonUp(0) && playArea(playPosition)  && isClicked){
            mouseEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ReleaseMouse ();
        }else if(Input.GetMouseButtonUp(0) && !playArea(playPosition)  && isClicked){
            lineRenderer.enabled =false;
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
        lineRenderer.enabled = true;
    }
    public void MouseDragged(Vector2 tempMousePosition){
        lineRenderer.enabled = true;
        float diffX = mouseStartPosition.x - tempMousePosition.x;
        float diffY = Mathf.Abs(mouseStartPosition.y - tempMousePosition.y);
        RayCastGuide(diffX, diffY);
    }
    private void RayCastGuide(float diffX, float diffY){
        RaycastHit2D[] hit = new RaycastHit2D[2];
        int layerMask = (1 << 8) | (1 << 9);
        hit[0] = Physics2D.Raycast(this.transform.position, new Vector2(-diffX,diffY), Mathf.Infinity,~layerMask);
        if (hit[0])
        {
            Vector2 adjustVector = hit[0].point;
            if(adjustVector.x < 0){
                adjustVector.x -= -0.0001f;
                adjustVector.y -= 0.0001f;
            }
            else if(adjustVector.x >= 0){
                adjustVector.x -= 0.0001f;
                adjustVector.y -= 0.0001f;
            }
            hit[1] = Physics2D.Raycast(adjustVector, 
                Vector2.Reflect(adjustVector - (Vector2)this.transform.position, hit[0].normal), Mathf.Infinity,~layerMask);
            lineRenderer.SetPosition(0, this.transform.position);
            if(adjustVector.x < 0){
                Vector2 offset =  new Vector2(-.25f, .25f);
                lineRenderer.SetPosition(1, hit[0].point - offset);
                lineRenderer.SetPosition(2, hit[1].point - offset);
            }
            else if(adjustVector.x >= 0){
                Vector2 offset =  new Vector2(.25f, .25f);
                lineRenderer.SetPosition(1, hit[0].point - offset);
                lineRenderer.SetPosition(2, hit[1].point - offset);
            }
            
        }
    }
    public void ReleaseMouse(){
        lineRenderer.enabled =false;
        isClicked = false;
        ballVelocityX = mouseStartPosition.x - mouseEndPosition.x;
        ballVelocityY = Mathf.Abs(mouseStartPosition.y - mouseEndPosition.y);
        tempVelocity = new Vector2(-ballVelocityX, ballVelocityY).normalized;
        ballLaunchPosition = transform.position;
        StartCoroutine(gameManager.FastBall());
        currentBallState = ballState.fire;
    }
    public void TouchInput2(){
        if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            Vector2 playPosition =  Camera.main.ScreenToWorldPoint(touch.position);
            if(touch.phase == TouchPhase.Began && playArea(playPosition)){
                mouseStartPosition = this.transform.position;
                isClicked = true;
                lineRenderer.enabled = true;
            }
            if(touch.phase == TouchPhase.Moved && playArea(playPosition) && isClicked){
                lineRenderer.enabled = true;
                Vector2 tempMousePosition = Camera.main.ScreenToWorldPoint(touch.position);
                float diffX = mouseStartPosition.x - tempMousePosition.x;
                float diffY = Mathf.Abs(mouseStartPosition.y - tempMousePosition.y);
                RayCastGuide(diffX, diffY);

            }else if(touch.phase == TouchPhase.Moved && !playArea(playPosition) && isClicked){
                lineRenderer.enabled = false;
            }
            if(touch.phase == TouchPhase.Ended && playArea(playPosition)  && isClicked){
                lineRenderer.enabled = false;
                isClicked = false;
                mouseEndPosition = Camera.main.ScreenToWorldPoint(touch.position);
                ballVelocityX = mouseStartPosition.x - mouseEndPosition.x;
                ballVelocityY = Mathf.Abs(mouseStartPosition.y - mouseEndPosition.y);
                tempVelocity = new Vector2(-ballVelocityX, ballVelocityY).normalized;
                ballLaunchPosition = transform.position;
                currentBallState = ballState.fire;
                StartCoroutine(gameManager.FastBall());

            }else if(touch.phase == TouchPhase.Ended && !playArea(playPosition) && isClicked){
                lineRenderer.enabled = false;
            }
        }
    }
    public void ballDown(){
        tempVelocity = new Vector2(0, -ballVelocityY).normalized;
        if(currentBallState == ballState.fire || currentBallState == ballState.wait){
            stopButton.interactable = false;
            gameManager.ballsInScene.ForEach(c => c.GetComponent<Rigidbody2D>().velocity 
                        = constantSpeed * tempVelocity*2);
            FindObjectOfType<ExtraBallManager>().numberOfBallsToFire = 0;
            currentBallState = BallControl.ballState.wait;
            ColliderChangeEnable(false);
        }
    }
    private void ColliderChangeEnable(bool status){
        for(int i = 0; i < gameManager.bricksInScene.Count; i++)
        {
            if(gameManager.bricksInScene[i].tag =="Square Brick"){
                gameManager.bricksInScene[i].GetComponent<BoxCollider2D>().enabled 
                    = status;
            }
            else if(gameManager.bricksInScene[i].tag =="Triangle Brick")
            {
                gameManager.bricksInScene[i].GetComponent<PolygonCollider2D>().enabled 
                    = status;
            }
            else if(gameManager.bricksInScene[i].tag =="Extra Ball Up")
            {
                gameManager.bricksInScene[i].GetComponent<CircleCollider2D>().enabled 
                    = status;
            }
            else if(gameManager.bricksInScene[i].tag =="Star Up")
            {
                gameManager.bricksInScene[i].GetComponent<PolygonCollider2D>().enabled 
                    = status;
            }
        }
    }
}
