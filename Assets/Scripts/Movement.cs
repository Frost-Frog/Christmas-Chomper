using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]

public class Movement : MonoBehaviour //this is used for both Santa and the Elves
{
    public Animator animator;
    public float Speed = 8.0f;
    public Vector2 initialDirection;
    [SerializeField]
    public Vector2 direction {get; private set;}
    public Vector2 nextdirection {get; private set;} //if you hold right but a wall is in the way you can continue holding right until a passage opens up
    public Vector3 startingpos {get; private set;}
    public LayerMask obstacles;
    public float SpeedMult = 1.0f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.startingpos = this.transform.position;
        this.animator = GetComponent<Animator>();
    }
    void Start()
    {
        ResetState();
    }
    public void ResetState() //blopg
    {
        this.SpeedMult = 1.0f;
        this.direction = initialDirection;
        this.transform.position = startingpos;
        this.nextdirection = Vector2.zero;
        this.rb.isKinematic = false;
        this.enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate() //Responsible for moving pacman
    {
        Vector2 position = this.rb.position;
        Vector2 translation = this.direction * this.Speed * this.SpeedMult * Time.fixedDeltaTime;
        this.rb.MovePosition(position + translation);
    }

    void Update() //animates pacman and sets the next direction
    {
        if(this.nextdirection != Vector2.zero)
        {
            SetDirection(this.nextdirection);
        }
        if(this.gameObject.layer == LayerMask.NameToLayer("Santa"))
        {
            if(!Occupied(direction))
            {
                this.animator.SetFloat("Speed", 1);
            }
            else if(Occupied(direction))
            {
                this.animator.SetFloat("Speed", 0);
            }
        }
        
    }
    public void SetDirection(Vector2 direction, bool forced = false) //jhafg
    {
        
        if(forced || !Occupied(direction))
        {
            this.direction = direction;
            this.nextdirection = Vector2.zero;
        }
        else
        {
            this.nextdirection = direction;
        }
    }
    public bool Occupied(Vector2 direction) //returns if there is an occupied space (obstacle) in fron of you 
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.0f, this.obstacles);
        // if(hit.collider != null)
        // {
        //     Debug.DrawRay(this.transform.position,(Vector2.one.x * 0.75f + 0.5f) * direction, Color.red);
        // }
        // else
        // {
        //     Debug.DrawRay(this.transform.position,(Vector2.one.x * 0.75f + 0.5f) * direction, Color.green);   
        // }
        return hit.collider != null;
        // Debug.Log(hit.collider);
       
    }
}

