using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuScript : MonoBehaviour
{
    [SerializeField] GameObject screen1HolderGM1;
    [SerializeField] GameObject screen2HolderGM1;
    [SerializeField] GameObject screen2HolderGM2;
    [SerializeField] GameObject screen1HolderGM2;

    [SerializeField] GameObject Gamemode1Holder;
    [SerializeField] GameObject Gamemode2Holder;

    [SerializeField] GameObject optionsSectionPermItems;
    [SerializeField] TMP_Dropdown GMDropDown;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "OptionsMenu") goToScreen1GM2();

        DontDestroyOnLoad(gameObject);
    }
        /*
     .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.   .----------------. 
    | .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. | | .--------------. |
    | |    ______    | || |      __      | || | ____    ____ | || |  _________   | || | ____    ____ | || |     ____     | || |  ________    | || |  _________   | | | |     __       | |
    | |  .' ___  |   | || |     /  \     | || ||_   \  /   _|| || | |_   ___  |  | || ||_   \  /   _|| || |   .'    `.   | || | |_   ___ `.  | || | |_   ___  |  | | | |    /  |      | |
    | | / .'   \_|   | || |    / /\ \    | || |  |   \/   |  | || |   | |_  \_|  | || |  |   \/   |  | || |  /  .--.  \  | || |   | |   `. \ | || |   | |_  \_|  | | | |    `| |      | |
    | | | |    ____  | || |   / ____ \   | || |  | |\  /| |  | || |   |  _|  _   | || |  | |\  /| |  | || |  | |    | |  | || |   | |    | | | || |   |  _|  _   | | | |     | |      | |
    | | \ `.___]  _| | || | _/ /    \ \_ | || | _| |_\/_| |_ | || |  _| |___/ |  | || | _| |_\/_| |_ | || |  \  `--'  /  | || |  _| |___.' / | || |  _| |___/ |  | | | |    _| |_     | |
    | |  `._____.'   | || ||____|  |____|| || ||_____||_____|| || | |_________|  | || ||_____||_____|| || |   `.____.'   | || | |________.'  | || | |_________|  | | | |   |_____|    | |
    | |              | || |              | || |              | || |              | || |              | || |              | || |              | || |              | | | |              | |
    | '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' | | '--------------' |
     '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'   '----------------' */
    public void goToScreen1GM1()
    {
        GMDropDown.gameObject.SetActive(true);
        Gamemode1Holder.SetActive(true);
        Gamemode2Holder.SetActive(false);
        screen1HolderGM1.SetActive(true);
        screen2HolderGM1.SetActive(false);
        optionsSectionPermItems.SetActive(true);
    }
    public void goToScreen2GM1()
    {
        GMDropDown.gameObject.SetActive(false);
        screen1HolderGM1.SetActive(false);
        screen2HolderGM1.SetActive(true);
        optionsSectionPermItems.SetActive(false);
    }
    /*
    .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.   .----------------. 
    | .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. | | .--------------. |
    | |    ______    | || |      __      | || | ____    ____ | || |  _________   | || | ____    ____ | || |     ____     | || |  ________    | || |  _________   | | | |    _____     | |
    | |  .' ___  |   | || |     /  \     | || ||_   \  /   _|| || | |_   ___  |  | || ||_   \  /   _|| || |   .'    `.   | || | |_   ___ `.  | || | |_   ___  |  | | | |   / ___ `.   | |
    | | / .'   \_|   | || |    / /\ \    | || |  |   \/   |  | || |   | |_  \_|  | || |  |   \/   |  | || |  /  .--.  \  | || |   | |   `. \ | || |   | |_  \_|  | | | |  |_/___) |   | |
    | | | |    ____  | || |   / ____ \   | || |  | |\  /| |  | || |   |  _|  _   | || |  | |\  /| |  | || |  | |    | |  | || |   | |    | | | || |   |  _|  _   | | | |   .'____.'   | |
    | | \ `.___]  _| | || | _/ /    \ \_ | || | _| |_\/_| |_ | || |  _| |___/ |  | || | _| |_\/_| |_ | || |  \  `--'  /  | || |  _| |___.' / | || |  _| |___/ |  | | | |  / /____     | |
    | |  `._____.'   | || ||____|  |____|| || ||_____||_____|| || | |_________|  | || ||_____||_____|| || |   `.____.'   | || | |________.'  | || | |_________|  | | | |  |_______|   | |
    | |              | || |              | || |              | || |              | || |              | || |              | || |              | || |              | | | |              | |
    | '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' | | '--------------' |
     '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'   '----------------' 
         */
    public void goToScreen1GM2()
    {
        GMDropDown.gameObject.SetActive(true);
        Gamemode1Holder.SetActive(false);
        Gamemode2Holder.SetActive(true);
        screen1HolderGM2.SetActive(true);
        screen2HolderGM2.SetActive(false);
        optionsSectionPermItems.SetActive(true);
    }
    public void goToScreen2GM2()
    {
        GMDropDown.gameObject.SetActive(false);
        screen1HolderGM2.SetActive(false);
        screen2HolderGM2.SetActive(true);
        optionsSectionPermItems.SetActive(false);
    }

    /*
     .----------------.  .----------------.  .----------------.  .----------------.  .----------------. 
    | .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |
    | |     ____     | || |  _________   | || |  ____  ____  | || |  _________   | || |  _______     | |
    | |   .'    `.   | || | |  _   _  |  | || | |_   ||   _| | || | |_   ___  |  | || | |_   __ \    | |
    | |  /  .--.  \  | || | |_/ | | \_|  | || |   | |__| |   | || |   | |_  \_|  | || |   | |__) |   | |
    | |  | |    | |  | || |     | |      | || |   |  __  |   | || |   |  _|  _   | || |   |  __ /    | |
    | |  \  `--'  /  | || |    _| |_     | || |  _| |  | |_  | || |  _| |___/ |  | || |  _| |  \ \_  | |
    | |   `.____.'   | || |   |_____|    | || | |____||____| | || | |_________|  | || | |____| |___| | |
    | |              | || |              | || |              | || |              | || |              | |
    | '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |
     '----------------'  '----------------'  '----------------'  '----------------'  '----------------' 
        */

    public void handleGMDropdown()
    {
        int gamemodeNumber = 1;
        if (GMDropDown.options[GMDropDown.value].text.Contains("2")) gamemodeNumber = 2;
        if (GMDropDown.options[GMDropDown.value].text.Contains("1")) gamemodeNumber = 1;

        if(gamemodeNumber == 1) { goToScreen1GM1(); }
        if(gamemodeNumber == 2) { goToScreen1GM2(); }
    }
}
