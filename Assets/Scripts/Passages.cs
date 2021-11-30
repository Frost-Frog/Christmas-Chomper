using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passages : MonoBehaviour
{
    public Transform connection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        Vector3 position = collider.transform.position;
        collider.transform.position = connection.position; 
    }
}
