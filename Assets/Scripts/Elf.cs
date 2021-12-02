using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf : MonoBehaviour
{
    public Movement movement;
    public ElfHome home {get; private set;}
    public ElfChase chase {get; private set;}
    public ElfScatter scatter {get; private set;}
    public ElfScared scared {get; private set;}
    public BlinkyChase Bchase;
    public ElfBehavior initbehavior; //what a ghost does at the start of the game
    public Transform Santa;
    public float points = 200.0f;

    void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.home = GetComponent<ElfHome>();
        this.chase = GetComponent<ElfChase>();
        Bchase = FindObjectOfType<BlinkyChase>().GetComponent<BlinkyChase>();
        this.scatter = GetComponent<ElfScatter>();
        this.scared = GetComponent<ElfScared>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetState();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Santa"))
        {
            if(this.scared.enabled)
            {
                FindObjectOfType<GameManager>().ElfDecked(this);
            }
            else
            {
                FindObjectOfType<GameManager>().SantaKilled();
            }
        }
    }
    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.scared.Disable();
        if(this.chase != null)
        {
            this.chase.Disable();
        }
        else
        {
            FindObjectOfType<BlinkyChase>().GetComponent<BlinkyChase>().Disable();
        }
        this.scatter.Enable();

        if(this.initbehavior != this.home)
        {
            this.home.Disable();
        }
        if(this.initbehavior != null)
        {
            this.initbehavior.Enable();
        }
    }
    void Update()
    {
        if(this.movement.direction.x == 1)
        {
            this.transform.localScale = new Vector2(-1, this.transform.localScale.y);
        }
        else
        {
            this.transform.localScale = Vector2.one;
        }
    }
}
