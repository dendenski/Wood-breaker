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
    
    private ScoreManager score;
    // Start is called before the first frame update
    void Start()
    {
        blockImage = GetComponent<SpriteRenderer>();
        brickHealthText = GetComponentInChildren<Text> ();
        gameManager = FindObjectOfType<GameManager>();
        score = FindObjectOfType<ScoreManager>();
        brickHealth = gameManager.level;
        //chance to double health
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
    // Update is called once per frame
    void Update()
    {
        brickHealthText.text = "" + brickHealth;
        if(brickHealth <= 0){
            //destroy brick
            score.ScoreIncrease();
            this.gameObject.SetActive(false);
        }
    }
    void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Ball" || other.gameObject.tag == "Extra Ball"){
            blockImage.sprite = spriteBlock;
            Instantiate(brickDestroyParticle, transform.position, Quaternion.identity);
            TakeDamage(1);
        }
    }
}
