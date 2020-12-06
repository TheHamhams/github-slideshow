using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    GameManager gameManagerScript;
    public GameObject soil;
    
    public int scoreToAdd;

    private void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            scoreToAdd = 10;
            gameManagerScript.foodCollected++;
            gameManagerScript.UpdateScore(scoreToAdd);
            Instantiate(soil, transform.position, transform.rotation);

        }

        if (other.tag == "Enemy")
        {
            Destroy(this.gameObject);
            scoreToAdd = -15;
            gameManagerScript.foodCollected--;
            gameManagerScript.UpdateScore(scoreToAdd);
            Instantiate(soil, transform.position, transform.rotation);

        }
    }
}