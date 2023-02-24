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

    public SpriteRenderer heart0SpriteRenderer;
    public SpriteRenderer heart1SpriteRenderer;
    public SpriteRenderer heart2SpriteRenderer;

    public GM2WinnerScript winScript;

    // Start is called before the first frame update
    void Awake()
    {
        heart0SpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        heart1SpriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        heart2SpriteRenderer = transform.GetChild(2).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        displayHealth();
        checkForLossAndDoProcedure();
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
                heart0SpriteRenderer.sprite = fullHeartSprite;
                heart1SpriteRenderer.sprite = fullHeartSprite;
                heart2SpriteRenderer.sprite = fullHeartSprite;
                break;
            case 5:
                Debug.Log("we at five health lolol");
                heart0SpriteRenderer.sprite = fullHeartSprite;
                heart1SpriteRenderer.sprite = fullHeartSprite;
                heart2SpriteRenderer.sprite = halfHeartSprite;
                break;
            case 4:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = fullHeartSprite;
                transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = fullHeartSprite;
                transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                break;
            case 3:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = fullHeartSprite;
                transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = halfHeartSprite;
                transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                break;
            case 2:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = fullHeartSprite;
                transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                break;
            case 1:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = halfHeartSprite;
                transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                break;
            case 0 :
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                break;
            default:
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
                break;
        }
    }

    void checkForLossAndDoProcedure()
    {
        if (health <= 0 && heartHolder.gameObject.name.Contains("Isa"))
        {
            winScript.doMatWinSequence();
        }
        else if( health <= 0 && heartHolder.gameObject.name.Contains("Mat"))
        {
            winScript.doIsaWinSequence();
        }
    }
}
