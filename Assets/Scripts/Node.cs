using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacles;
    public List<Vector2> possibleDirections {get; private set;}
    // Start is called before the first frame update
    void Start()
    {
        this.possibleDirections = new List<Vector2>();

        CheckDirections(Vector2.up);
        CheckDirections(Vector2.down);
        CheckDirections(Vector2.left);
        CheckDirections(Vector2.right);
    }

    public void CheckDirections(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, direction, 1f, this.obstacles);

        if(hit.collider == null)
        {
            possibleDirections.Add(direction);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
