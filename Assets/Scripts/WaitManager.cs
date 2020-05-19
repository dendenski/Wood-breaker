using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WaitManager : MonoBehaviour
{
    private float waitTime = 3.0f;
    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
