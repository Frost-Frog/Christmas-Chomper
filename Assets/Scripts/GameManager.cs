using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoretext;
    public TextMeshProUGUI showscore;
    public TextMeshProUGUI GameOverText;
    bool canreset;
    public Elf[] Elves;
    public Santa Santa;
    public Transform pellets;

    public float GhostMult {get; private set;} = 1;
    public float score {get; private set;}
    public int lives {get; private set;}
    public float offset;
    Vector2 initialpos;
    float score_mulitiplier = 1; //The more rounds you do the mnore points you earn from objects
    int round;
    
    void Awake()
    {
        initialpos = Santa.gameObject.transform.position;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        round = 0;
        NewGame();
    }
    void NewGame()//what happens wneh you start a new game
    {
        GameOverText.enabled = false;
        canreset = false;
        SetScore(0);
        SetLives(3);
        NewRound();
    }


    void NewRound()//recreates pellets and Elves once you win a round
    {
        //Santa.gameObject.GetComponent<Movement>().ResetState();
        foreach(Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetRound();
        round++;
        if(round % 2 == 0)
        {
            score_mulitiplier = 1 + (round/4);
        }
    }

    void ResetRound() // if Santa dies or any circumstance where pellets don't need to be reset but everything else does
    {
        ResetPower();
        ResetGhostMult();
        ResetPower();
        for( int i = 0; i < this.Elves.Length; i++)
        {
            this.Elves[i].ResetState();
        }
        this.Santa.ResetState();
    }
    void GameOver() // when you lose all your lives
    {
        GameOverText.enabled = true;
        for( int i = 0; i < this.Elves.Length; i++)
        {
            this.Elves[i].gameObject.SetActive(false);
        }
        this.Santa.gameObject.SetActive(false);
        canreset = true;
    }

    void SetScore(float score)//sets your score when starting a new game
    {
        this.score = score;
        scoretext.text = ("score: " + this.score.ToString());
    }
    void SetLives(int lives)//sets your lives when starting a newg game
    {
        this.lives = lives;
    }

    public void ElfDecked(Elf elf)//When Santa absolutely decks an elf; GOing to be called by other scripts
    {
        ShowScore(elf.transform, elf.gameObject);
        SetScore(this.score + (elf.points * score_mulitiplier * GhostMult));
        GhostMult *= 2; 
        StartCoroutine(Play(true));
    }
    public void SantaKilled()//this one is self explanitory; GOing to be called by oher scripts
    {
        this.Santa.gameObject.SetActive(false);
        SetLives(this.lives - 1);
        if(this.lives > 0)
        {
            Invoke(nameof(ResetRound), 3.5f);
        }
        else
        {
            GameOver();
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(canreset && Input.anyKeyDown)
        {
            NewGame();
        }
    }
    public void PelletEaten(Pellet pellet) //eats pellet
    {
        SetScore(this.score + (pellet.points * score_mulitiplier));
        pellet.gameObject.SetActive(false);
        if(!StillHasPellets())
        {
            this.Santa.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.5f);
        }
    }
    public void PowerEaten(PowerPellet pellet) //does what pellet does but changes ghost state
    {
        this.Elves[0].scared.Santa.enabled = false;
        this.Elves[0].scared.Sleigh.enabled = true;
        this.Santa.movement.SpeedMult = 1.5f;
        for(int i = 0; i < this.Elves.Length; i++)
        {
            this.Elves[i].scared.DurationEnable(pellet.duration);
        }
        PelletEaten(pellet);
        CancelInvoke(nameof(ResetGhostMult));
        CancelInvoke(nameof(ResetPower));
        Invoke(nameof(ResetGhostMult), pellet.duration);
        Invoke(nameof(ResetPower), pellet.duration);
    }

    bool StillHasPellets()
    {
        foreach(Transform pellet in this.pellets)
        {
            if(pellet.gameObject.activeSelf)
            {
                return true;
            }
            
        }
        return false;
    }

    void ResetGhostMult()
    {
        this.GhostMult = 1.0f;
    }
    public void ShowScore(Transform position, GameObject score)
    {
        showscore.enabled = true;
        float points;
        showscore.transform.position = position.position;
        if(score.GetComponent<Elf>() != null)
        {
            points = score.GetComponent<Elf>().points * GhostMult * score_mulitiplier;
        }
        else
        {
            points = score.GetComponent<Present>().points * score_mulitiplier;
        }
        showscore.text = points.ToString();
    }
    public IEnumerator Play(bool pause)
    {
        if(pause)
        {
            this.Elves[0].scared.Santa.enabled = false;
            this.Elves[0].scared.Sleigh.enabled = false;
            Time.timeScale = 0.0f;
            yield return new WaitForSecondsRealtime(0.7f);
            Time.timeScale = 1.0f;
            this.Elves[0].scared.Santa.enabled = true;
            this.Elves[0].scared.Sleigh.enabled = true;
            showscore.enabled = false;
        }
        else
        {
            yield return new WaitForSecondsRealtime(1.0f);
            showscore.enabled = false;
        }
        
    }
    void ResetPower()
    {
        this.Elves[0].scared.Santa.enabled = true;
        this.Elves[0].scared.Sleigh.enabled = false;;
        this.Santa.movement.SpeedMult = 1.0f;
    }
    public void PresentEaten(Present present)
    {

        SetScore(score + (present.points * score_mulitiplier));
        ShowScore(present.gameObject.transform, present.gameObject);
        showscore.enabled = true;
        StartCoroutine(Play(false));
    }
}
