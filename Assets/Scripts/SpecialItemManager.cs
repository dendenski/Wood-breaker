using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpecialItemManager : MonoBehaviour
{
    public ExtraBallManager extraBallManager;

    public BrickHealthManager brickHealthManager;
    public ScoreManager scoreManager;

    public GameManager gameManager;
    public CameraShake cameraShake;
    public Button balls2xButton;
    public Button damage2xButton;
    public Button halfHpButton;
    public Text balls2xButtonText;
    public int balls2x;
    public bool isballs2x;
    private int balls2xCost;
    private int damage2xCost;
    private int halfHPCost;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        isballs2x = false;
        balls2xCost = 30;
        halfHPCost = 20;
        damage2xCost = 10;
        damage = 1;
        balls2x = 0;
        scoreManager = FindObjectOfType<ScoreManager> ();
        cameraShake = FindObjectOfType<CameraShake> ();
        extraBallManager = FindObjectOfType<ExtraBallManager> ();
        brickHealthManager  = FindObjectOfType<BrickHealthManager>();
        gameManager = FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(scoreManager.diamondStarCount < balls2xCost){
            balls2xButton.interactable = false;
        }
        if(scoreManager.diamondStarCount < damage2xCost){
            damage2xButton.interactable = false;
        }
        if(scoreManager.diamondStarCount < halfHPCost){
            halfHpButton.interactable = false;
        }
    }

    public void Balls2x(){
        if(scoreManager.SubtractDiamondStarCount(balls2xCost)){
            extraBallManager.numberOfExtraBalls *= 2;
            extraBallManager.numberOfBallsToFire *= 2;
            balls2x = extraBallManager.numberOfExtraBalls;
            balls2xButtonText.color = new Color(1f,0f,0f);
            isballs2x = true;
        }
        balls2xButton.interactable = false;
    }
    public void BallsNormalize(){
        if(balls2xButton.interactable == false && isballs2x == true){ 
            balls2x /= 2;
            extraBallManager.numberOfExtraBalls -= balls2x;
            extraBallManager.numberOfBallsToFire -= balls2x;
            balls2xButtonText.color = new Color(1f,0.5f,0f);
            isballs2x = false;
            
        }
        if(damage2xButton.interactable == false){ 
            damage = 1;
        }

        if(scoreManager.diamondStarCount >= balls2xCost){ 
            balls2xButton.interactable = true;
        }
        if(scoreManager.diamondStarCount >= damage2xCost){ 
            damage2xButton.interactable = true;
        }
        if(halfHpButton.interactable == false && 
        scoreManager.diamondStarCount >= halfHPCost){ 
            halfHpButton.interactable = true;
        }
    }
    public void Damage2x(){
        if(scoreManager.SubtractDiamondStarCount(damage2xCost)){
            damage = 2;
        }
        damage2xButton.interactable = false;
    }
    public void HalfLife(){
        if(scoreManager.SubtractDiamondStarCount(halfHPCost)){
            for(int i = 0; i < gameManager.bricksInScene.Count; i++)
            {
                BrickHealthManager brickHealthManager = gameManager.bricksInScene[i].GetComponent<BrickHealthManager>();
                if(brickHealthManager == null){
                    continue;
                }
                brickHealthManager.brickHealth /=2;
                cameraShake.shakeDuration = 1f;
            }
        }
        halfHpButton.interactable = false;
    }
}
