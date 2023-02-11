using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM2BoomwhackerScript : MonoBehaviour
{
    //AUDIO RELATED
    [SerializeField] AudioClip bonk;
    private AudioSource audioSrc;

    //SPRITES
    [SerializeField] Sprite BWSwingSprite;
    [SerializeField] Sprite BWSprite;

    [SerializeField] Sprite MatisseWhackedSprite;
    [SerializeField] Sprite IsabelWhackedSprite;

    [SerializeField] Sprite MatisseSprite;
    [SerializeField] Sprite IsabelSprite;

    //SPRITE RENDERERS
    //"BoomWhacker _ Sprite Renderer _ Matisse"
    [SerializeField] SpriteRenderer BW_SR_MAT;
    [SerializeField] SpriteRenderer BW_SR_ISA;

    [SerializeField] SpriteRenderer Matisse_SR;
    [SerializeField] SpriteRenderer Isabel_SR;

    //PRIMITIVE DATA TYPES
    //This defines how long the swing sprite will stay visible
    public float swingTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //whack Isabel
            StartCoroutine(whackCoroutine(false));
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Whack Matisse
            StartCoroutine(whackCoroutine(true));
        }
    }

    IEnumerator whackCoroutine(bool whackMatisse)
    {
        audioSrc.Play();
        if (whackMatisse)
        {
            BW_SR_MAT.sprite = BWSwingSprite;
            Matisse_SR.sprite = MatisseWhackedSprite;
            yield return new WaitForSeconds(swingTime);
            BW_SR_MAT.sprite = BWSprite;
            Matisse_SR.sprite = MatisseSprite;
        }
        else
        {
            BW_SR_ISA.sprite = BWSwingSprite;
            Isabel_SR.sprite = IsabelWhackedSprite;
            yield return new WaitForSeconds(swingTime);
            Isabel_SR.sprite = IsabelSprite;
            BW_SR_ISA.sprite = BWSprite;
        }
    }
}
