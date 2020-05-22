using System.Collections;
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
    
    private ObjectPool objectPool;
    public Sprite spriteSquare; 
    public Sprite spriteTriangle; 
    public float probabilityOfBricks;
    
    public float probabilityOfDoubleHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        
        objectPool = FindObjectOfType<ObjectPool>();
        level = 1;
        probabilityOfBricks = 0.25f;
        probabilityOfDoubleHealth = 0.2f;
        for(int i = 0; i < spawnPoints.Length; i++){
            int brickToCreate = Random.Range(0,4);
            if(brickToCreate == 0){
                bricksInScene.Add(Instantiate(squareBrick, spawnPoints[i].position, Quaternion.identity));
            }
            else if(brickToCreate == 1){
                bricksInScene.Add(Instantiate(triangleBrick, spawnPoints[i].position, Quaternion.identity));
            }
        }
        numberOfExtraBallsInRow = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlaceBricks(){
        level++;
        int counter = 0;
        if(level % 10 == 0 && probabilityOfBricks < 0.80f)
        {
            probabilityOfBricks += 0.025f;
            probabilityOfDoubleHealth += 0.02f;
        }
        foreach(Transform pos in spawnPoints){
            if(numberOfExtraBallsInRow == 0 && counter == 7){
                GameObject brick = objectPool.GetPooledObject("Extra Ball Up");
                AddBrick(brick, pos);
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
            else if(numberOfExtraBallsInRow == 0){
                GameObject brick = objectPool.GetPooledObject("Extra Ball Up");
                AddBrick(brick, pos);
                numberOfExtraBallsInRow++;
            }
            counter++;
        }
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
}
