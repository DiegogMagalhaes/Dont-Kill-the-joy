using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [Header("Obstacles")]
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject blade;
    [SerializeField] private GameObject missile;

    [Header("OtherManagers")]
    [SerializeField] private DialogueTrigger TextManager;
    [SerializeField] private UIManager UIManager;

    [Header("Timer Settings")]
    [SerializeField] private float MaxTimer;
    [SerializeField] private Text TimerText;

    [Header("SpawnReference")]
    [SerializeField] private float MaxY;
    [SerializeField] private float MinY;
    [SerializeField] private float MaxX;
    [SerializeField] private float MinX;

    [Header("Config")]
    [SerializeField] private int Act;
    [SerializeField]private int[] Obstacles = new int[3]; // Define o numero de obstaculos que tera em cada ato, onde 0- bomb 1-blade 2-missile     
    [SerializeField] private Act[] ActObstacles= new Act[7];

    //Variables
    private int CurrentAct;
    private GameObject joy;
    
    [SerializeField]private float SpawnTime;
    private bool Ingame;

    private float Timer;
    [SerializeField]private float SpawnTimer;

    private void Start()
    {
        joy = GameObject.FindWithTag("Player");
        CurrentAct = Act;
        ActUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Ingame)
        {
            if(SpawnTimer > 0) SpawnTimer -= Time.deltaTime;
            else
            {
                SpawnTimer = SpawnTime;
                Spawn();
            }

            if (Timer <= 0) EndGame();
            else Timer -= Time.deltaTime;

            UpdateTimer();  
        }
    }

    private void UpdateTimer()
    {
        TimerText.text = "" + (int)Timer;
    }

    private void ActUpdate()
    {
        
        
            for (int i = 0; i < 3; i++)
            {
                Obstacles[i] = ActObstacles[CurrentAct - 1].Obstacle[i];
            }

            UIManager.SwitchBlackScreen(true);
            TextManager.StartTexting(CurrentAct);

            int Obstaclesnum = Obstacles[0] + Obstacles[1] + Obstacles[2];
            SpawnTime = (MaxTimer - 3) / Obstaclesnum;
            SpawnTimer = SpawnTime;
            Timer = MaxTimer;
        
        
    }
    
    public bool IsOver()
    {
        if (CurrentAct >= 6)
        {
            UIManager.ReturnToMenu();
            return true;
        }
        return false;
    }

    public void StartGame()
    {
        if (!TimerText.gameObject.activeSelf) TimerText.gameObject.SetActive(true);

        joy.transform.position = new Vector3(-4.5f, -0.04f, 0f);
        UIManager.SwitchBlackScreen(false);
        joy.GetComponent<PlayerMoviment>().SwitchCanMove(true);
        Ingame = true;
    }

    public void StopGame()
    {
        Ingame = false;
    }

    public void ResetGame()
    {
        if (!TimerText.gameObject.activeSelf) TimerText.gameObject.SetActive(true);

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Line"))
            Destroy(obj);

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("DamageSource"))
            Destroy(obj);

        for (int i = 0; i < 3; i++)
        {
            Obstacles[i] = ActObstacles[CurrentAct - 1].Obstacle[i];
        }

        int Obstaclesnum = Obstacles[0] + Obstacles[1] + Obstacles[2];
        SpawnTime = (MaxTimer - 3) / Obstaclesnum;
        SpawnTimer = SpawnTime;
        Timer = MaxTimer;


        Ingame = true;
    }

    public void EndGame()
    {
        CurrentAct++;
        if (Act >= 6) UIManager.ReturnToMenu();
        Ingame = false;
        ActUpdate();
        joy.GetComponent<PlayerMoviment>().SwitchCanMove(false);
    }

    void Spawn()
    {
        int ObstacleID;
        List<int> numberChoises = new List<int>();

        for (int i = 0 ; i<3 ; i++)
        {
            numberChoises.Add(i);
        }

        while (numberChoises.Count != 0)
        {

                ObstacleID = Random.Range(0, numberChoises.Count);
                ObstacleID = numberChoises[ObstacleID];
               // Debug.Log("id = " + ObstacleID + "cont = " + numberChoises.Count);

                if (Obstacles[ObstacleID] != 0)
                {
                    Obstacles[ObstacleID] -= 1;
                    


                    switch (ObstacleID)
                    {

                        case 0:
                            SpawnBomb();
                            break;

                        case 1:
                        
                            List<GameObject> objlist = new List<GameObject>();
                           
                            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Line"))
                            {
                                objlist.Add(obj);
                            }
                        

                            if (objlist.Count < 1)SpawnBlade();
                            else
                            {
                                SpawnBomb();
                            }

                            break;

                        case 2:
                           
                            SpawnMissile();
                            break;

                    }


                    break;
                }

                else
                {
                    numberChoises.Remove(ObstacleID);
                }
            

        }

    }

    private void SpawnBomb()
    {
        Vector2 norm =  2*joy.GetComponent<Rigidbody2D>().velocity.normalized;
        float posx = 0;

        if (norm.x > 0)
            posx = Random.Range(joy.transform.position.x + 1, joy.transform.position.x + norm.x);
        else if (norm.x < 0)
            posx = Random.Range(joy.transform.position.x + norm.x , joy.transform.position.x - 1);
        else
            posx = joy.transform.position.x;

        if (posx > MaxX)
        {
            posx = MaxX;
        }
        else if (posx < MinX)
        {
            posx = MinX;
        }
        Instantiate(bomb).transform.position = new Vector2(posx, MaxY);
    }

    private void SpawnBlade()
    {
        
        float posy = 0;

        posy = joy.transform.position.y;

        Instantiate(blade).transform.position = new Vector2(0, posy);
    }
     
    private void SpawnMissile()
    {
        Vector2 norm = 2 * joy.GetComponent<Rigidbody2D>().velocity.normalized;
        float posx = 0;

        if (norm.x > 0)
            posx = Random.Range(joy.transform.position.x + 1, joy.transform.position.x + norm.x);
        else if (norm.x < 0)
            posx = Random.Range(joy.transform.position.x + norm.x * 2, joy.transform.position.x - 1);
        else
            posx = joy.transform.position.x;

        if (posx > MaxX)
        {
            posx = MaxX;
        }
        else if (posx < MinX)
        {
            posx = MinX;
        }

        Instantiate(missile).transform.position = new Vector2(posx, MinY-0.7f);
    }


}
