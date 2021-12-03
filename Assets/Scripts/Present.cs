using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    public GameObject FruitObject;
    public GameObject present;
    public bool edible;
    public SpriteRenderer PresentSprite;

    public Sprite[] PresentSprites;//goes Cherry, Strawberry, Orange, Apple, Melon, Galaxian, Bell, Key
    public float points; //goes 100, 300, 500, 700, 100, 200, 3000, 5000 respectively

    void Start()
    {
        Invoke(nameof(GenerateFruit), Random.value > 0.5f ? 10.0f : 15.0f);
    }

    void GenerateFruit()//Generates fruits with diffferent chances
    {
        Invoke(nameof(GenerateFruit), Random.value > 0.5f ? 10.0f : 15.0f);
        int RandFruit = Random.Range(1, 100);

        if(RandFruit < 14)
        {
            FruitObject = Instantiate(present);
            points = 100.0f;
            FruitObject.GetComponent<SpriteRenderer>().sprite = PresentSprites[0];
        }
        else if(RandFruit < 20)
        {
            FruitObject = Instantiate(present);
            points = 300.0f;
            FruitObject.GetComponent<SpriteRenderer>().sprite = PresentSprites[1];
        }
        else if(RandFruit < 40 && RandFruit >= 20)
        {
            FruitObject = Instantiate(present);
            points = 500.0f;
            FruitObject.GetComponent<SpriteRenderer>().sprite = PresentSprites[2];
        }
        else if(RandFruit < 55 && RandFruit >= 40)
        {
            FruitObject = Instantiate(present);
            points = 700.0f;
            FruitObject.GetComponent<SpriteRenderer>().sprite = PresentSprites[3];
        }
        else if(RandFruit < 70 && RandFruit >= 55)
        {
            FruitObject = Instantiate(present);
            points = 1000.0f;
            FruitObject.GetComponent<SpriteRenderer>().sprite = PresentSprites[4];
        }
        else if(RandFruit < 90 && RandFruit >= 70)
        {
            FruitObject = Instantiate(present);
            points = 2000.0f;
            FruitObject.GetComponent<SpriteRenderer>().sprite = PresentSprites[5];
        }
        else if(RandFruit < 95 && RandFruit >= 90)
        {
            FruitObject = Instantiate(present);
            points = 3000.0f;
            FruitObject.GetComponent<SpriteRenderer>().sprite = PresentSprites[6];
        }
        else if(RandFruit < 100 && RandFruit >= 95)
        {
            FruitObject = Instantiate(present);
            points = 5000.0f;
            FruitObject.GetComponent<SpriteRenderer>().sprite = PresentSprites[7];
        }
        edible = true;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Santa") && edible)
        {
            FindObjectOfType<GameManager>().PresentEaten(this);
            this.FruitObject.SetActive(false);
        }
    }
}
