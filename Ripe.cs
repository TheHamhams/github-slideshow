using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripe : MonoBehaviour
{
    public GameObject ripe;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RipeTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RipeTimer()
    {
        yield return new WaitForSeconds(Random.Range(3, 5));
        Destroy(gameObject);
        Instantiate(ripe, transform.position, transform.rotation);
    }
}
