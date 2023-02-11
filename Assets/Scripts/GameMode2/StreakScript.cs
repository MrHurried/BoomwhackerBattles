using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StreakScript : MonoBehaviour
{
    public TextMeshProUGUI streakOnderIsaText;

    private Color isaTopColor, isaBottomColor;
    private Color matTopColor, matBottomColor;

    //Measured in degrees (max = 360; min = 0)
    public int HueChangeMagnitude = 2;
    // Start is called before the first frame update
    void Start()
    {
        resetGradient(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void resetGradient()
    {
        matTopColor = Color.HSVToRGB(274, 82, 58);
        matBottomColor = Color.HSVToRGB(256, 62, 91);

        isaTopColor = Color.HSVToRGB(274, 82, 58);
        isaBottomColor = Color.HSVToRGB(256, 62, 91);

        streakOnderIsaText.colorGradient = new VertexGradient(isaTopColor, isaTopColor, isaBottomColor, isaBottomColor);
    }

    void shiftHue(bool isMatisse)
    {
        if (isMatisse)
        {

        }
        else
        {
            //if ((Color.RGBToHSV(isaBottomColor)).hue - HueChangeMagnitude < 0) isaBottomColor = Color.HSVToRGB(0, isa , );
            //streakOnderIsaText.color = new VertexGradient()
        }
    }
}
