using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    

    public GameObject soil;
    public GameObject seeds;
    public GameObject seedling;
    public GameObject food;
    public GameObject needMoreDialog;
    public GameObject bringFoodDialog;
    public GameObject victoryDialog;
   
    
    Rigidbody2D rigidbody2d;

    GameManager gameManagerScript;
    
    Vector2 lookDirection = new Vector2(1, 0);

    public float speed = 3.0f;
    float horizontal;
    float vertical;
    float yBoundry = 7;
    float leftXBoundry = -8.5f;
    float rightXBoundry = 12.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 move = new Vector2(horizontal, vertical);

        MovePlayer();
        Boundries();       
        
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1) && gameManagerScript.isActive)
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("Ground"));
            if (hit.collider != null)
            {
                
                Destroy(hit.transform.gameObject);
                Instantiate(seeds, hit.transform.position, hit.collider.transform.rotation);
                
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Keypad2) && gameManagerScript.isActive)
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("Seeds"));
            if (hit.collider != null)
            {

                Destroy(hit.transform.gameObject);
                Instantiate(seedling, hit.transform.position, hit.collider.transform.rotation);
                
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Keypad3) && gameManagerScript.isActive)
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("Ripe"));
            if (hit.collider != null)
            {

                Destroy(hit.transform.gameObject);
                Instantiate(food, hit.transform.position, hit.collider.transform.rotation);
                
            }
        }

        

      
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        if (gameManagerScript.isActive)
        {
            position.x = position.x + speed * horizontal * Time.deltaTime;
            position.y = position.y + speed * vertical * Time.deltaTime;
        }
        else
        {
            position.x = transform.position.x;
            position.y = transform.position.y;
        }
        rigidbody2d.MovePosition(position);
    }

    void MovePlayer()
    {
        if (gameManagerScript.isActive)
        { 
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");        
        }
        
    }

    void Boundries()
    {
        if (transform.position.y < -yBoundry)
        {
            transform.position = new Vector2(transform.position.x, -yBoundry);
        }

        if (transform.position.y > yBoundry)
        {
            transform.position = new Vector2(transform.position.x, yBoundry);
        }

        if (transform.position.x < leftXBoundry)
        {
            transform.position = new Vector2(leftXBoundry, transform.position.y);
        }

        if (transform.position.x > rightXBoundry)
        {
            transform.position = new Vector2(rightXBoundry, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Idol")
        {
            if (gameManagerScript.foodCollected < gameManagerScript.foodLeft)
            {
                StartCoroutine(NeedMoreFood());
            }

            if (gameManagerScript.foodCollected == gameManagerScript.foodLeft)
            {
                StartCoroutine(Victory());
            }
        }
    }

    IEnumerator NeedMoreFood()
    {
        needMoreDialog.SetActive(true);
        yield return new WaitForSeconds(3);
        needMoreDialog.SetActive(false);
    }

    IEnumerator Victory()
    {
        gameManagerScript.GameOver();
        victoryDialog.SetActive(true);
        yield return new WaitForSeconds(3);
        victoryDialog.SetActive(false);

    }




}