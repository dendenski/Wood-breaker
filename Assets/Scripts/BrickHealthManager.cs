using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BrickHealthManager : MonoBehaviour
{
    public int brickHealth;
    private Text brickHealthText;
    private GameManager gameManager;
    public GameObject brickDestroyParticle;
    private SpriteRenderer blockImage;
    public Sprite spriteBlock;
    public SpecialItemManager specialItemManager;
    private ScoreManager score;
    void Start()
    {
        specialItemManager = FindObjectOfType<SpecialItemManager>();
        blockImage = GetComponent<SpriteRenderer>();
        brickHealthText = GetComponentInChildren<Text> ();
        gameManager = FindObjectOfType<GameManager>();
        score = FindObjectOfType<ScoreManager>();
        brickHealth = gameManager.level;
        if(Random.value <= gameManager.probabilityOfDoubleHealth && gameManager.level >= 5){
            brickHealth = gameManager.level*2;
        }
        brickHealthText.text = "" + brickHealth;
    }
    void OnEnable() {
        gameManager = FindObjectOfType<GameManager>();
        brickHealth = gameManager.level;
    }
    void TakeDamage( int  damageToTake){
        brickHealth -= damageToTake;
    }
    void Update()
    {
        brickHealthText.text = "" + brickHealth;
        if(brickHealth <= 0){
            score.ScoreIncrease();
            gameManager.bricksInScene.Remove(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
    void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Ball" || other.gameObject.tag == "Extra Ball"){
            blockImage.sprite = spriteBlock;
            Instantiate(brickDestroyParticle, transform.position, Quaternion.identity);
            TakeDamage(specialItemManager.damage);
        }
    }
}
