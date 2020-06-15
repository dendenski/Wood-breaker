using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickMovementControl : MonoBehaviour
{
    public enum brickState{
        stop,
        move
    }
    private bool hasMoved;
    public brickState currentState;
    void Start()
    {
        hasMoved = false;
        currentState = brickState.stop;
    }
    private void OnEnable(){
        hasMoved = false;
        currentState = brickState.stop;
    }
    void Update()
    {
        if(currentState == brickState.stop){
            hasMoved = false;
        }
        if(currentState == brickState.move){
            if(hasMoved == false){
                
                transform.position = new Vector2(transform.position.x, transform.position.y - 1);
                currentState = brickState.stop;
                hasMoved = true;
            }
        }
    }
}
