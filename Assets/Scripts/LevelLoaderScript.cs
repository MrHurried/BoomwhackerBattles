using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoaderScript : MonoBehaviour
{
    public Animator transition;

    [Tooltip("The audiosource playing the music to fade out")]
    [SerializeField] AudioSource audioSource;

    public float transitionTime = 1f;

    public float fadeoutMagnitude = .01f;

    public void LoadLevel(string sceneToLoad)
    {
        
        StartCoroutine(LoadLevelCoroutine(sceneToLoad, audioSource));  
    }


    IEnumerator LoadLevelCoroutine(string sceneToLoad, AudioSource AS)
    {


        //play animation
        transition.SetTrigger("Start");

        //FADE BG MUSIC
        float basevolume = AS.volume;

        Debug.Log(fadeoutMagnitude / transitionTime);

        while(AS.volume > 0f)
        {
            AS.volume -= fadeoutMagnitude;
            yield return new WaitForSeconds(fadeoutMagnitude / transitionTime);
            Debug.Log("when the");
        }
        //wacht
        //yield return new WaitForSeconds(transitionTime);

        //Load de scene
        SceneManager.LoadScene(sceneToLoad);
    }

}
