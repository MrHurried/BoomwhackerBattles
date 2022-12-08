using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuScript : MonoBehaviour
{
    [SerializeField] GameObject screen1Holder;
    [SerializeField] GameObject screen2Holder;
    public void goToScreen2()
    {
        screen1Holder.SetActive(false);
        screen2Holder.SetActive(true);
    }
    public void goToScreen1()
    {
        screen1Holder.SetActive(true);
        screen2Holder.SetActive(false);
    }
}
