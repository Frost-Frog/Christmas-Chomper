using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    public GameObject present;
    public SpriteRenderer PresentSprite;

    public Sprite[] PresentSprites;//goes Cherry, Strawberry, Orange, Apple, Melon, Galaxian, Bell, Key
    public int points; //goes 100, 300, 500, 700, 100, 200, 3000, 5000 respectively

    void GenerateFruit()//Generates fruits with diffferent chances
    {
        int RandFruit = Random.Range(1, 100);

        if(RandFruit > 14)
        {
            GameObject FruitObject = Instantiate(present);
            points = 100;
            PresentSprite.sprite = PresentSprites[0];
        }
        // else if()
        // {

        // }
        // else if()
        // {

        // }
        // else if()
        // {

        // }
        // else if()
        // {

        // }
        // else if()
        // {

        // }
        // else if()
        // {

        // }
        // else if()
        // {

        // }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
