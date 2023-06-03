using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GM2GameOptionsStorer : MonoBehaviour
{
    //OptionsScript
    private OptionsMenuScript optionsMenu;

    //Not a number error UI
    [SerializeField] GameObject NaN_BpmIncrease;
    [SerializeField] GameObject NaN_StartingBpm;

    //settings
    public static int bpmIncrease;
    public static int startingBpm;
    public static bool doCameraSway;

    //SETTINGS UI
    [SerializeField] TMP_InputField bpmIncreaseIPField;
    [SerializeField] TMP_InputField startingBpmIPField;
    [SerializeField] Toggle doScreenSwayToggle;

    //IPField text
    private string bpmIncreaseText;
    private string startingBpmText;

    private void Start()
    {
        optionsMenu = GetComponent<OptionsMenuScript>();
    }

    public void SaveGameOptionsGM2()
    {
        //get text from IPFields
        bpmIncreaseText = bpmIncreaseIPField.text;
        startingBpmText = startingBpmIPField.text;

        //Check for NaN errors in bpmIncrease
        if(!int.TryParse(bpmIncreaseText, out bpmIncrease))
        {
            NaN_BpmIncrease.SetActive(true);
            return;
        }
        else
        {
            NaN_BpmIncrease.SetActive(false);
        }

        //Check for NaN errors in startingBPM
        if (!int.TryParse(startingBpmText, out startingBpm))
        {
            NaN_StartingBpm.SetActive(true);
            return;
        }
        else
        {
            NaN_StartingBpm.SetActive(false);
        }

        doCameraSway = doScreenSwayToggle.isOn;
        optionsMenu.goToScreen2GM2();
    }
}
