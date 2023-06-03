using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GM1GameOptionsStorer : MonoBehaviour
{
    //Levelloader
    [SerializeField] LevelLoaderScript levelLoader;

    //game options
    public static bool doFlashes = false;
    public static int startingPointAmount = 100;
   
    [SerializeField] string sceneToLoad; // this might be a game option in the future, perhaphs a different game mode could be selected

    //UI element references in OptionsMenu scene
    [SerializeField] TMP_InputField pointsToWinInputfield;
    [SerializeField] Toggle doFlashingToggle;
    [SerializeField] GameObject noDecimalsErrorGameObject;


    //this function is called when the "continue" button in the OptionsMenu scene is pressed
    public void SaveGameOptionsGM1() 
    {
        string ptwText = pointsToWinInputfield.text;

        //check if pointsToWin Input field text element doesn't have an integer typed in
        // if it doesn't, an error shows
        //if it does, the if statement gets skipped and the decimal error text gets removed
        if (!int.TryParse(ptwText, out startingPointAmount))
        {
            noDecimalsErrorGameObject.SetActive(true);
            return;
        }
        else
        {
            levelLoader.LoadLevel("GameMode1Scene");
        }
        noDecimalsErrorGameObject.SetActive(false);

        //store all the game options
        doFlashes = doFlashingToggle.isOn;
        
        startingPointAmount = int.Parse(ptwText);

        //Load the last scene
        //SceneManager.LoadScene(sceneToLoad);
    }
}
