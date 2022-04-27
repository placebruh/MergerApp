using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int fieldW = 5;
    public int fieldH = 6;
    public GameObject fieldPref;
    private Transform[] fields;
    [SerializeField]private AudioSource[] popFX;
    public TextMeshProUGUI textScore;
    public GameObject startButton;
 
   

    bool isClicked = false;
    public GameObject qube1;
    public GameObject qube2;
    public GameObject qube3;
    public GameObject qube4;
    public GameObject qube5;
    private int counter1 = 0;
    private int counter2 = 0;
    private int counter3 = 0;
    private int counter4 = 0;
    private int counter5 = 0;
    private int scoreCounter = 0;

    [SerializeField] private Animator anim;

    private void Update()
    {
        
        
        SpawnSquares();
        textScore.text = scoreCounter.ToString();
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            startButton.SetActive(false);
        }
        if (PauseMenu.GameIsPaused == true)
        {
            Time.timeScale = 0f;
           
        }
        else 
        {
            PauseMenu.GameIsPaused = false;
            Time.timeScale = 1f;
            
        }
    }

    private void Start()
    {
        popFX[1].Play();
        startButton.SetActive(true);
        
       
        int br = 0;
        fields = new Transform[fieldW*fieldH];
        for(int i = 0; i < fieldH; i++)
        {
            for(int j = 0; j < fieldW; j++)
            {
                fields[br] = Instantiate(fieldPref, new Vector3(j, i, 0f), Quaternion.Euler(0, 0, 0)).transform;
                br++;
            }
        }
    }

    private void SpawnSquares()
    {

        //ubacivanje kocke
        if (Input.GetKeyDown(KeyCode.Mouse0) && PauseMenu.GameIsPaused == false)
        {
           
                scoreCounter++;
                isClicked = true;
                if (isClicked)
                {
                    isClicked = false;
                    Debug.Log("Qube1 is spawned");
                    SpawnQube(qube1);

                }
            
        }

    }

    private int FirstFreePlace()
    {
        return counter1 + counter2 + counter3 + counter4 + counter5;
    }

    private void SpawnQube(GameObject q)
    {
        Instantiate(q, fields[FirstFreePlace()]);
        if(q.Equals(qube1))
            counter1++;
        FixStructure();
    }

    private void FixStructure()
    {
        if(counter1 > 9)
        {
            popFX[0].Play();
            counter1 = 0;
            DeleteQubes("Qube1");
            SpawnQube(qube2);
            counter2++;
        }
        if (counter2 > 9)
        {
            counter2 = 0;
            DeleteQubes("Qube2");
            SpawnQube(qube3);
            counter3++;
            scoreCounter = scoreCounter + 50;
        }
        if (counter3 > 9)
        {
            counter3 = 0;
            DeleteQubes("Qube3");
            SpawnQube(qube4);
            counter4++;
            scoreCounter = scoreCounter + 100;
        }
        if (counter4 > 9)
        {
            counter4 = 0;
            DeleteQubes("Qube4");
            SpawnQube(qube5);
            counter5++;
            scoreCounter = scoreCounter + 150;
        }
        if (counter5 > 9)
        {
            counter5 = 0;
            DeleteQubes("Qube5");
            
            
        }

    }

    private void DeleteQubes(string tag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Animator>().Play("death");
           
        }
            
    }

  
  
   

   
}

