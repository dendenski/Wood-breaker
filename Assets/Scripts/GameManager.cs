﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{   
    public Transform[] spawnPoints;
    public GameObject squareBrick;
    public GameObject triangleBrick;
    public GameObject extraBallPowerup;
    public int numberOfBricksToStart;
    public int level;
    public List<GameObject> bricksInScene;
    public List<GameObject> ballsInScene;
    //private ObjectPool objectPool;
    public int numberOfExtraBallsInRow = 0;
    public int numberOfStarsInRow = 0;
    private ObjectPool objectPool;
    public Sprite spriteSquare; 
    public Sprite spriteTriangle; 
    public float probabilityOfBricks;
    public float probabilityOfDoubleHealth;
    private AudioSource audioState;
    private BallControl ballControl;
    void Start()
    {
        ballControl = FindObjectOfType<BallControl>();
        audioState = this.GetComponent<AudioSource>();
        objectPool = FindObjectOfType<ObjectPool>();
        level = 1;
        probabilityOfBricks = 0.20f;
        probabilityOfDoubleHealth = 0.05f;
        for(int i = 0; i < spawnPoints.Length; i++){
            int brickToCreate = Random.Range(0,4);
            if(brickToCreate == 0){
                bricksInScene.Add(Instantiate(squareBrick, spawnPoints[i].position, Quaternion.identity));
            }
            else if(brickToCreate == 1){
                bricksInScene.Add(Instantiate(triangleBrick, spawnPoints[i].position, Quaternion.identity));
            }
            else if(brickToCreate == 2 && numberOfExtraBallsInRow == 0){
                bricksInScene.Add(Instantiate(extraBallPowerup, spawnPoints[i].position, Quaternion.identity));
                numberOfExtraBallsInRow++;
            }
        }
        numberOfExtraBallsInRow = 0;
        numberOfStarsInRow = 0;
    }
    void Update()
    {
        if(PlayerPrefs.GetInt ("musicOption") == 0 && audioState.volume != 1.0f){
            audioState.volume = 1.0f;
        }
        else if(PlayerPrefs.GetInt ("musicOption") == 1  && audioState.volume != 0f){
            audioState.volume = 0.0f;
        }
    }
    public void PlaceBricks(){
        level++;
        int counter = 0;
        if(level % 10 == 0 && probabilityOfBricks < 0.80f)
        {
            probabilityOfBricks += 0.025f;
            probabilityOfDoubleHealth += 0.005f;
        }
        foreach(Transform pos in spawnPoints){
            if(level % 50 == 0 && numberOfStarsInRow == 0){
                GameObject brick = objectPool.GetPooledObject("Star Up");
                AddBrick(brick, pos);
                numberOfStarsInRow++;
            }
            else if(numberOfExtraBallsInRow == 0 && counter == 7){
                GameObject ballObject = objectPool.GetPooledObject("Extra Ball Up");
                AddBrick(ballObject, pos);
                numberOfExtraBallsInRow++;
            }
            else if(Random.value <= probabilityOfBricks){
                GameObject brick = objectPool.GetPooledObject("Square Brick");
                SpriteRenderer spriteBlock;
                spriteBlock = brick.GetComponent<SpriteRenderer>();
                spriteBlock.sprite = spriteSquare;
                AddBrick(brick, pos);
            }
            else if(Random.value <= probabilityOfBricks){
                GameObject brick = objectPool.GetPooledObject("Triangle Brick");
                SpriteRenderer spriteBlock;
                spriteBlock = brick.GetComponent<SpriteRenderer>();
                spriteBlock.sprite = spriteTriangle;
                AddBrick(brick, pos);
            }
            else if(Random.value <= (probabilityOfBricks+0.35f) && numberOfExtraBallsInRow == 0){
                GameObject brick = objectPool.GetPooledObject("Extra Ball Up");
                AddBrick(brick, pos);
                numberOfExtraBallsInRow++;
            }
            else if(Random.value <= (.05f) && numberOfStarsInRow == 0){
                GameObject brick = objectPool.GetPooledObject("Star Up");
                AddBrick(brick, pos);
                numberOfStarsInRow++;
            }
            counter++;
        }
        numberOfStarsInRow = 0;
        numberOfExtraBallsInRow = 0;
    }
    private void AddBrick(GameObject brick,Transform pos){
        bricksInScene.Add(brick);
        if(brick != null){
            brick.transform.position = pos.position;
            brick.transform.rotation = Quaternion.identity;
            brick.SetActive(true);
        }
    }
    public IEnumerator FastBall(){
        yield return new WaitForSeconds(30f);
        Debug.Log("fastball");
        ballsInScene.ForEach(c => c.GetComponent<Rigidbody2D>().velocity 
                    = 2 * c.GetComponent<Rigidbody2D>().velocity);
        ballControl.constantSpeed *= 2;
    }
}
