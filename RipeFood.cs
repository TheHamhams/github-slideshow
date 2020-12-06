using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipeFood : MonoBehaviour
{
    public GameObject soil;
    GameManager gameManagerScript;

    public int scoreToAdd;


    private void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
     

        if (other.tag == "Enemy")
        {
            Destroy(this.gameObject);
            
            if (gameManagerScript.foodCollected < gameManagerScript.foodLeft)
            {
                scoreToAdd = -15;
            }
            
            gameManagerScript.UpdateScore(scoreToAdd);
            Instantiate(soil, transform.position, transform.rotation);

        }
    }
}
