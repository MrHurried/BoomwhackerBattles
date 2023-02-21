using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] Transform heartHolder;

    [SerializeField] Sprite fullHeartSprite;
    [SerializeField] Sprite halfHeartSprite;
    [SerializeField] Sprite emptyHeartSprite;

    private int health = 6;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        displayHealth();
    }

    public void removeHealth(int amount)
    {
        health -= amount;
    }

    private void displayHealth()
    {
        switch (health)
        {
            case 6:
                heartHolder.GetChild(0).GetComponent<SpriteRenderer>().sprite = fullHeartSprite;
                heartHolder.GetChild(1).GetComponent<SpriteRenderer>().sprite = fullHeartSprite;
                heartHolder.GetChild(2).GetComponent<SpriteRenderer>().sprite = fullHeartSprite;
                break;
            case 5:
                Debug.Log("we at five health lolol");
                heartHolder.GetChild(0).GetComponent<SpriteRenderer>().sprite = fullHeartSprite;
                heartHolder.GetChild(1).GetComponent<SpriteRenderer>().sprite = fullHeartSprite;
                heartHolder.GetChild(2).GetComponent<SpriteRenderer>().sprite = halfHeartSprite;
                break;
            case 4:
                heartHolder.GetChild(0).GetComponent<SpriteRenderer>().sprite = fullHeartSprite;
                heartHolder.GetChild(1).GetComponent<SpriteRenderer>().sprite = fullHeartSprite;
                heartHolder.GetChild(2).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                break;
            case 3:
                heartHolder.GetChild(0).GetComponent<SpriteRenderer>().sprite = fullHeartSprite;
                heartHolder.GetChild(1).GetComponent<SpriteRenderer>().sprite = halfHeartSprite;
                heartHolder.GetChild(2).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                break;
            case 2:
                heartHolder.GetChild(0).GetComponent<SpriteRenderer>().sprite = fullHeartSprite;
                heartHolder.GetChild(1).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                heartHolder.GetChild(2).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                break;
            case 1:
                heartHolder.GetChild(0).GetComponent<SpriteRenderer>().sprite = halfHeartSprite;
                heartHolder.GetChild(1).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                heartHolder.GetChild(2).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                break;
            case 0 :
                heartHolder.GetChild(0).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                heartHolder.GetChild(1).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                heartHolder.GetChild(2).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                break;
            default:
                heartHolder.GetChild(0).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                heartHolder.GetChild(1).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                heartHolder.GetChild(2).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                break;
        }
    }
}
