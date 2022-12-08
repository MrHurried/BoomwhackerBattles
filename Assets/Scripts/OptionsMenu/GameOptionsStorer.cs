using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameOptionsStorer : MonoBehaviour
{
    //game options
    public static bool doFlashes;
    public static int pointsToWin;
   
    [SerializeField] string sceneToLoad; // this might be a game option in the future, perhaphs a different game mode could be selected

    //UI element references in OptionsMenu scene
    [SerializeField] TMP_InputField pointsToWinInputfield;
    [SerializeField] Toggle doFlashingToggle;
    [SerializeField] GameObject noDecimalsErrorGameObject;


    //this function is called when the "continue" button in the OptionsMenu scene is pressed
    public void SaveGameOptions() 
    {
        string ptwText = pointsToWinInputfield.text;

        //check if pointsToWin Input field text element doesn't have an integer typed in
        // if it doesn't, an error shows
        //if it does, the if statement gets skipped and the decimal error text gets removed
        if (!int.TryParse(ptwText, out pointsToWin))
        {
            noDecimalsErrorGameObject.SetActive(true);
            return;
        }
        noDecimalsErrorGameObject.SetActive(false);

        //store all the game options
        doFlashes = doFlashingToggle.isOn;
        
        pointsToWin = int.Parse(ptwText);

        //Load the last scene
        //SceneManager.LoadScene(sceneToLoad);
    }
}
