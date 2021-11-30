using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfScared : ElfBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer blue;
    public Animator animator;

    public bool eaten;
    
    public override void DurationEnable(float duration)
    {
        //Debug.Log(duration);
        base.DurationEnable(duration);

        this.body.enabled = false;
        this.blue.enabled = true;

        Invoke(nameof(Flash), duration/2);

    }
    public override void Disable()
    {
        base.Disable();

        this.body.enabled = true;
        this.blue.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Santa"))
        {
            if(this.elf.scared.enabled)
            {
                Eaten();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Node node = collider.GetComponent<Node>();
        // if(this.eaten)
        // {
            

        //     if(node != null && this.enabled)
        //     {
        //         Vector2 direction = Vector2.zero; //stores the direction
        //         float minDistance = float.MaxValue;

        //         foreach(Vector2 possibledirection in node.possibleDirections)
        //         {
        //             Vector3 newPosition = this.transform.position + new Vector3(possibledirection.x, possibledirection.y, 0.0f);
        //             float distance = (this.elf.home.inHome.position - newPosition).sqrMagnitude;
        //             if(distance < minDistance)
        //             {
        //                 minDistance = distance;
        //                 direction = possibledirection;
        //             }
        //         }
        //         this.elf.movement.SetDirection(direction);
        //     }
        // }
        if(this.enabled && node != null)
        {
            if(node != null && this.enabled)//function is always called even if this is disabled
            {
                Vector2 direction = Vector2.zero; //stores the direction
                float maxDistance = float.MinValue;

                foreach(Vector2 possibledirection in node.possibleDirections)
                {
                    Vector3 newPosition = this.transform.position + new Vector3(possibledirection.x, possibledirection.y, 0.0f);
                    float distance = (this.elf.Santa.position - newPosition).sqrMagnitude;
                    if(distance > maxDistance)
                    {
                        maxDistance = distance;
                        direction = possibledirection;
                    }
                }
                this.elf.movement.SetDirection(direction);
            }
        }
        
    }
    void Flash()
    {
        if(!this.eaten)
        {
            animator.SetFloat("Duration", 4);
        }
    }
    private void OnEnable()
    {
        this.eaten = false;
    }

    public void Eaten()
    {
        this.eaten = true;
        this.elf.transform.position = this.elf.home.inHome.position;
        this.elf.home.DurationEnable(this.duration);

        this.body.enabled = true;
        this.blue.enabled = false;
    }
    
}
