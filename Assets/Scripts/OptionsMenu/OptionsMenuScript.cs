using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuScript : MonoBehaviour
{
    [SerializeField] GameObject screen1HolderGM1;
    [SerializeField] GameObject screen2HolderGM1;
    [SerializeField] GameObject screen2HolderGM2;
    [SerializeField] GameObject screen1HolderGM2;

    [SerializeField] GameObject optionsSectionPermItems;
    public void goToScreen2GM1()
    {
        screen1HolderGM1.SetActive(false);
        screen2HolderGM1.SetActive(true);
        optionsSectionPermItems.SetActive(false);
    }
    public void goToScreen1GM1()
    {
        screen1HolderGM1.SetActive(true);
        screen2HolderGM1.SetActive(false);
        optionsSectionPermItems.SetActive(true);
    }
    public void goToScreen2GM2()
    {
        screen1HolderGM2.SetActive(false);
        screen2HolderGM2.SetActive(true);
        optionsSectionPermItems.SetActive(false);
    }
    public void goToScreen1GM2()
    {
        screen1HolderGM2.SetActive(true);
        screen2HolderGM2.SetActive(false);
        optionsSectionPermItems.SetActive(true);
    }
}
