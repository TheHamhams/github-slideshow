using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    GameManager gameManagerScript;
    Button button;
    public GameObject startScreen;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();

        button = GetComponent<Button>();
        button.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        startScreen.SetActive(false);
        gameManagerScript.StartGame();
    }
}
