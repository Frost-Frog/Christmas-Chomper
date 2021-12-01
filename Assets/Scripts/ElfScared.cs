using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfScared : ElfBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer blue;
    public SpriteRenderer Santa;
    public SpriteRenderer Sleigh;
    public SpriteRenderer Hat;
    public Animator animator;

    public bool eaten;
    int directionnum = 0;
    
    public override void DurationEnable(float duration)
    {
        //Debug.Log(duration);
        base.DurationEnable(duration);

        this.body.enabled = false;
        this.blue.enabled = true;
        animator.SetFloat("Duration", duration);

        Invoke(nameof(Flash), duration/2);

    }
    public override void Disable()
    {
        base.Disable();

        this.body.enabled = true;
        this.blue.enabled = false;
        this.Hat.enabled = false;
        this.eaten = false;
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
        if(this.eaten && node != null && this.enabled)
        {
            Vector2 direction = Vector2.zero; //stores the direction
            float minDistance = float.MaxValue;
            this.elf.movement.SpeedMult = 3.5f;

            foreach(Vector2 possibledirection in node.possibleDirections)
            {
                directionnum++;
                Vector3 newPosition = this.transform.position + new Vector3(possibledirection.x, possibledirection.y, 0.0f);
                float distance = (this.elf.home.exit.position - newPosition).sqrMagnitude;
                if(distance < minDistance)
                {
                    minDistance = distance;
                    direction = possibledirection;
                }
                if(direction == -this.elf.movement.direction)
                {
                    if(directionnum++ >= node.possibleDirections.Count)
                    {
                        direction = node.possibleDirections[0];
                    }
                    else
                    {
                        direction = node.possibleDirections[directionnum++];
                    }
                }
            }
            this.elf.movement.SetDirection(direction);
        }
        else if(this.eaten && collider.gameObject.tag == "Return")
        {
            StartCoroutine(ReverseHome());
        }
        if(this.enabled && node != null && !this.eaten)
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
        Physics2D.IgnoreCollision(this.elf.GetComponent<CircleCollider2D>(), this.elf.Santa.gameObject.GetComponent<CircleCollider2D>());
        this.eaten = true;
        Hat.enabled = true;
        this.blue.enabled = false;
        //this.elf.movement.rb.isKinematic = true;
        //this.elf.transform.position = this.elf.home.inHome.position;
        //Disable();
        
    }
    void Update()
    {
        // if(new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y))  == new Vector2(Mathf.Round(this.elf.home.exit.position.x), Mathf.Round(this.elf.home.exit.position.y)))
        // {
            
        // }
    }
    public IEnumerator ReverseHome()
    {
        //Debug.Log(true);
        this.elf.movement.SetDirection(Vector2.right, true);
        this.elf.movement.rb.isKinematic = true;
        this.elf.movement.enabled = false;

        Vector3 position = this.elf.home.exit.position;
        float duration = 0.5f;
        float elapsed = 0.0f;

        // while(elapsed < duration)
        // {
        //     Vector3 newposition = Vector3.Lerp(position, this.elf.home.exit.position, elapsed/duration);
        //     newposition.z = position.z;
        //     this.elf.transform.position = newposition;
        //     elapsed += Time.deltaTime;
        //     yield return null;
        // }

        while(elapsed < duration)
        {
            Vector3 newposition = Vector3.Lerp(position, this.elf.home.inHome.position, elapsed/duration * 2);
            newposition.z = position.z;
            this.elf.transform.position = newposition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        this.elf.movement.rb.isKinematic = false;
        this.elf.movement.enabled = true;
        Physics2D.IgnoreCollision(this.elf.GetComponent<CircleCollider2D>(), this.elf.Santa.gameObject.GetComponent<CircleCollider2D>(), false);
        this.elf.movement.SpeedMult = 1.0f;
        this.elf.home.DurationEnable(this.duration);
        Disable();

    }
    
}
