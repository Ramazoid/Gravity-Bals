using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    internal static bool menuVisible=true;
    public static MainMenu menu;
    public static Ball ball;
    public bool vis;
    public Planet[] planets;
    public static int hitCounter=0;

    // Start is called before the first frame update
    void Start()
    {
       
        menu = GameObject.FindObjectOfType<MainMenu>();
        menu.gameObject.SetActive(true);

        ball = GameObject.FindObjectOfType<Ball>();
        ball.EnableMoving(false);
        foreach(Planet p in planets)
        {
            menu.AddButton(p.PlanetName,planets.Length);
           
         
        }
    

    }

    internal static void Repaint(GameObject platform)
    {
        if (platform.name.IndexOf("platform_") == -1)
            return;
        SpriteRenderer sp = platform.GetComponent<SpriteRenderer>();
        if(sp)
        {
            Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            sp.color = color;
        }
        hitCounter++; ;
        PlayerPrefs.SetInt("BallHit", hitCounter);
    }

    public static void StopAndMenu()
    {
        ball.EnableMoving(false);
        menu.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopAndMenu();
            /*
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
*/
        }
    }
    public void SetgravityAndStart(float g)
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        string planet = name.Substring(0, name.IndexOf('_'));
        Debug.Log("planet=" + planet);

        Planet selectedPlanet = Array.Find(planets, (p)=>{
            return p.PlanetName == planet;

        });
        Debug.Log("founded" + selectedPlanet.gravity);
        Physics2D.gravity = new Vector2(0, -selectedPlanet.gravity);
        SpriteRenderer[] oldPlatforms = GameObject.FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer sr in oldPlatforms)
        {
            if (sr.gameObject.name.IndexOf("platform_") != -1)
                Destroy(sr.gameObject);
        }
        for (int i = 0; i < selectedPlanet.NumberOfPlatforms; i++)
        {
            GameObject platform = new GameObject();
            RectTransform rt = platform.AddComponent<RectTransform>();
            SpriteRenderer sprite =  platform.AddComponent<SpriteRenderer>();
            platform.name = "platform_" + i;
            Sprite sp = Resources.Load<Sprite>("Square");
            sprite.sprite = sp;
            sprite.color = Color.black;
            float width = Random.Range(1, selectedPlanet.platformMaxWidth);
            float height = Random.Range(0,selectedPlanet.platformMaxHeight);
            float panelX = Random.Range(-4, 4);
            float panelY =Random.Range(-4, 4);
            platform.AddComponent<BoxCollider2D>();
            Debug.Log("pos=" + panelX + ":" + panelY);
            sprite.transform.position = new Vector3(panelX, panelY, 0);
            sprite.transform.localScale = new Vector3(width,height, 1);
            //rt.SetPositionAndRotation(new Vector3(20, 20),Quaternion.identity);
            Debug.Log("loaded="+sp);
        }
        menu.gameObject.SetActive(false);
        ball.EnableMoving(true);
        ball.transform.position = new Vector3(-5, 6, 0);
        Camera.main.backgroundColor = selectedPlanet.SkyColor;
        hitCounter = 0;
      
    }
  
}
