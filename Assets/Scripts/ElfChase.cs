using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfChase : ElfBehavior
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
                float distance = (this.elf.Santa.position - newPosition).sqrMagnitude;
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
