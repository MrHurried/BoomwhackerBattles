using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StreakScript : MonoBehaviour
{
    public TextMeshProUGUI streakOnderIsaText;

    private Color isaTopColor, isaBottomColor;
    private Color matTopColor, matBottomColor;

    private const float S_TopColor = 82/100;
    private const float S_BottomColor = 62/100;
    private const float V_TopColor = 58/100;
    private const float V_BottomColor = 91/100;
    //the Hue values at every reset
    private const float H_TopDefault = 274/360;
    private const float H_BottomDefault = 256/360;

    //Measured in degrees (max = 360; min = 0)
    public float HueChangeMagnitude = 2/360;
    // Start is called before the first frame update
    void Start()
    {
        resetGradient(); 
    }

    // Update is called once per frame
    void Update()
    {
        updateTextColor();
    }

    void updateTextColor()
    {
        //streakOnderIsaText.colorGradient = new VertexGradient(isaTopColor, isaTopColor, isaBottomColor, isaBottomColor);
    }

    void resetGradient()
    {
        matTopColor = Color.HSVToRGB(H_TopDefault, S_TopColor, V_TopColor);
        matBottomColor = Color.HSVToRGB(H_BottomDefault, S_BottomColor, V_BottomColor);

        isaTopColor = Color.HSVToRGB(H_TopDefault, S_TopColor, V_TopColor);
        isaBottomColor = Color.HSVToRGB(H_BottomDefault, S_BottomColor, V_BottomColor);

        streakOnderIsaText.colorGradient = new VertexGradient(isaTopColor, isaTopColor, isaBottomColor, isaBottomColor);
    }

    void shiftHue(bool isMatisse)
    {
        float H, S, V;

        if (isMatisse)
        {

        }
        else
        {
            Color.RGBToHSV(isaBottomColor, out H, out S, out V);
            if ((H - HueChangeMagnitude < 0)) isaBottomColor = Color.HSVToRGB(0, S_BottomColor, V_BottomColor);
            else
            {
                isaBottomColor = Color.HSVToRGB(H - HueChangeMagnitude, S_BottomColor, V_BottomColor);
                isaTopColor = Color.HSVToRGB(H - HueChangeMagnitude, S_TopColor, V_TopColor);
            }
            //streakOnderIsaText.color = new VertexGradient()
        }
    }
}
