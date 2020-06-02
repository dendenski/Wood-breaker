using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public bool isBallStopped;
    public Vector2 targetPosition;
    public Vector2 currentVelocity;
    private float waitTime = 5.0f;
    private float timer = 5.0f;
    public SpecialItemManager specialItemManager;
    // Start is called before the first frame update
    void Start()
    {
        specialItemManager = FindObjectOfType<SpecialItemManager>();
        isBallStopped = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(isBallStopped){
            transform.position = Vector2.MoveTowards(transform.position, targetPosition,  20 * Time.deltaTime);
        }
        currentVelocity = this.GetComponent<Rigidbody2D>().velocity;
        timer += Time.deltaTime;
        if (timer >= waitTime && !isBallStopped)
        {
            Vector2 tempVelocity = this.GetComponent<Rigidbody2D>().velocity;
            float sample = Mathf.Round(tempVelocity.y * 10f) / 10f;

            if(sample == 0f){
                //Debug.Log("R1 sample : " + sample);
                tempVelocity.y = -0.2f;
                this.GetComponent<Rigidbody2D>().velocity = tempVelocity;
            }
            else{
                //Debug.Log("R2 sample : " + sample);
                tempVelocity.y = sample;
                this.GetComponent<Rigidbody2D>().velocity = tempVelocity;
            }
            timer = 0f;
        }
    }
    public IEnumerator BallMoveToNewPosition(){
        yield return new WaitForSeconds(.5f);
        isBallStopped = false;
        this.gameObject.SetActive(false);
        
    }
    void OnCollisionEnter2D(Collision2D other) {
        if(specialItemManager.damage == 2){
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.value + .1f,0f,0f);
        }else if(specialItemManager.damage == 1){
            this.GetComponent<SpriteRenderer>().color = new Color(0f,Random.value + .2f, Random.value + .2f);
        }

    }
}
