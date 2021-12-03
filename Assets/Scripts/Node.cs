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
    void Update()
    {
        // DirectionCheck(Vector2.up);
        // DirectionCheck(Vector2.down);
        // DirectionCheck(Vector2.left);
        // DirectionCheck(Vector2.right);
    }

    public void CheckDirections(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, direction, 0.5f, this.obstacles);
        
        if(hit.collider == null)
        {
            possibleDirections.Add(direction);
        }
    }
    bool DirectionCheck(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, direction, 1.0f, this.obstacles);
        Debug.DrawRay(this.transform.position, direction * (Vector2.one.x * 0.5f + 0.5f), Color.green);
        return hit.collider != null;
    }
}
