using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool canreset;
    public Elf[] Elves;
    public Santa Santa;
    public Transform pellets;

    public float GhostMult {get; private set;} = 1;
    public float score {get; private set;}
    public int lives {get; private set;}
    Vector2 initialpos;
    float score_mulitiplier; //The more rounds you do the mnore points you earn from objects
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
        score_mulitiplier = 1 + (round/4);
    }

    void ResetRound() // if Santa dies or any circumstance where pellets don't need to be reset but everything else does
    {
        ResetGhostMult();
        for( int i = 0; i < this.Elves.Length; i++)
        {
            this.Elves[i].ResetState();
        }
        this.Santa.ResetState();
    }
    void GameOver() // when you lose all your lives
    {
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
    }
    void SetLives(int lives)//sets your lives when starting a newg game
    {
        this.lives = lives;
    }

    public void ElfDecked(Elf elf)//When Santa absolutely decks an elf; GOing to be called by other scripts
    {
        SetScore(this.score + (elf.points * score_mulitiplier * GhostMult));
        GhostMult *= 2;
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
        SetScore(this.score + pellet.points);
        pellet.gameObject.SetActive(false);
        if(!StillHasPellets())
        {
            this.Santa.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.5f);
        }
    }
    public void PowerEaten(PowerPellet pellet) //does what pellet does but changes ghost state
    {
        for(int i = 0; i < this.Elves.Length; i++)
        {
            this.Elves[i].scared.DurationEnable(pellet.duration);
        }
        PelletEaten(pellet);
        CancelInvoke(nameof(ResetGhostMult));
        Invoke(nameof(ResetGhostMult), pellet.duration);
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
}
