using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : MonoBehaviour
{
    public Movement movement {get; private set;}
    //bool facingRight = true;
    // Start is called before the first frame update
    void Awake()
    {
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            float xinput = Input.GetAxisRaw("Horizontal");
            this.movement.SetDirection(Vector2.right * xinput);
            // Debug.Log(facingRight);
            // if(this.movement.direction.x < 0 && facingRight)
            // {
            //     Flip();
            // }
            // else if(this.movement.direction.x > 0 && !facingRight)
            // {
            //     Flip();
            // }
            
        }
        else if(Input.GetAxisRaw("Vertical") != 0)
        {
            float input = Input.GetAxisRaw("Vertical");
            this.movement.SetDirection(Vector2.up * input);
            
        }
        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x) * Mathf.Rad2Deg;
        this.transform.eulerAngles = Vector3.forward * angle;
        //Debug.Log(angle);
        // if(angle == -180 || angle == 180)
        // {
        //     this.transform.rotation = Quaternion.identity;
        // }
        // else
        // {
        
        // }
        // if(transform.rotation.z == -90 || transform.rotation.z == 90)
        // {
        //     transform.localScale = Vector3.one;
        // }
        // if(this.movement.direction.y != 0)
        // {
        //     transform.localScale = Vector3.one; //me being du,b
        // }
        
    }
    // void Flip()
    //     {
    //         facingRight = !facingRight;

    //         Vector3 Scale = transform.localScale; //flips the character
    //         Scale.x *= -1;
    //         transform.localScale = Scale;
    //     }
    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();
    }
       
}
