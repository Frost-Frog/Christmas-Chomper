using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOB : MonoBehaviour
{
    public float frequency;
    public float intensity;
    public float increase;
    // Start is called before the first frame update
    void Start()
    {
        
        //Time.time * frequency
    }

    // Update is called once per frame
    void Update()
    {
        increase += 0.02f;
        transform.Translate(Vector2.up * Mathf.Cos(increase * frequency) * intensity);
        //Debug.Log(Time.time);
    }
}
