using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    GameObject initButton;

    public Text BallHits;

    Vector2 buttonPosition;
 
    private bool first;
    private int buttonCount;

    void Start()
    {
        gameObject.SetActive(Game.menuVisible);
        initButton = transform.Find("Button").gameObject;
        
        Debug.Log("So initbutton="+initButton);
    
        first = true;
        
    }

   public void AddButton(string planetName, int count)
    {
        if(!initButton)
            initButton = transform.Find("Button").gameObject;
        buttonCount++;
        float buttonY = Screen.height / count;


        buttonPosition = new Vector2(Screen.width / 2, buttonY*buttonCount-buttonY/2);

        GameObject nextButton;
        if (first)
        {
            first = false;
            nextButton = initButton;
        }
        else
        {
           // buttonsPosition += new Vector2(0, -buttonsDeltaY);
            nextButton = GameObject.Instantiate(initButton, buttonPosition, Quaternion.identity);
        }
        nextButton.transform.position = buttonPosition;
        NameButton(nextButton, planetName);
        nextButton.transform.SetParent(initButton.transform.parent);

    }

    private void NameButton(GameObject button, string planetName)
    {
        button.name = planetName + "_Button";
        button.transform.Find("PlanetName").GetComponent<Text>().text = planetName;
    }

    void Update()
    {
       
       
    }
    private void OnEnable()
    {
        int hits=0;
        BallHits = transform.Find("BallHits").GetComponent<Text>();
        if(PlayerPrefs.HasKey("BallHit"))
        {
            hits = PlayerPrefs.GetInt("BallHit");
        }

        BallHits.text = "BallHit:" +hits.ToString();
    }
}
