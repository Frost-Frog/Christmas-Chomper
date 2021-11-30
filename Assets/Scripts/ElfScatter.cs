using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfScatter : ElfBehavior
{
    public BlinkyChase Bchase;
    void Start()
    {
        Bchase = FindObjectOfType<BlinkyChase>().GetComponent<BlinkyChase>();
    }
    private void OnDisable()
    {
        if(this.elf.chase != null)
        {
            this.elf.chase.Enable();
        }
        else
        {
            Bchase.Enable();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Node node = collider.GetComponent<Node>();

        if(node != null && this.enabled && !this.elf.scared.enabled)//function is always called even if this is disabled
        {
            int Randirection = Random.Range(0, node.possibleDirections.Count);//generates a random number for the direction
            if(node.possibleDirections[Randirection] == -this.elf.movement.direction && node.possibleDirections.Count > 1)
            {
                Randirection++;
                if(Randirection >= node.possibleDirections.Count)
                {
                    Randirection = 0;
                }
            }
            this.elf.movement.SetDirection(node.possibleDirections[Randirection]);
        }
        
    }
   
}
