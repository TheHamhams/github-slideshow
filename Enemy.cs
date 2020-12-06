using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public GameObject soil;
    
    int border = 16;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        BorderLimit();
    }

    void BorderLimit()
    {
        if (transform.position.x > border)
        {
            Destroy(this.gameObject);
        }
    }

    
}
