using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyChase : ElfBehavior
{
     private void OnDisable()
    {
        this.elf.scatter.Enable();
    }

    void OnTriggerEnter2D(Collider2D collider) //loops through all directions and find the closest to santa
    {
        Node node = collider.GetComponent<Node>();

        if(node != null && this.enabled && !this.elf.scared.enabled)//function is always called even if this is disabled
        {
            Vector2 direction = Vector2.zero; //stores the direction
            float minDistance = float.MaxValue;

            foreach(Vector2 possibledirection in node.possibleDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(possibledirection.x, possibledirection.y, 0.0f);
                Vector3 target;
                if(this.elf.Santa.gameObject.GetComponent<Movement>().direction.x == 0)
                {
                    target = new Vector3(this.elf.Santa.gameObject.GetComponent<Movement>().direction.x + this.elf.Santa.position.x,this.elf.Santa.gameObject.GetComponent<Movement>().direction.y * 6 + this.elf.Santa.position.y, 0.0f);
                }
                else
                {
                    target = new Vector3(this.elf.Santa.gameObject.GetComponent<Movement>().direction.x * 6 + this.elf.Santa.position.x, this.elf.Santa.gameObject.GetComponent<Movement>().direction.y + this.elf.Santa.position.y, 0.0f);
                }
                float distance = (target - newPosition).sqrMagnitude;
                if(distance < minDistance)
                {
                    minDistance = distance;
                    direction = possibledirection;
                }
            }
            this.elf.movement.SetDirection(direction);
        }
    }

}

