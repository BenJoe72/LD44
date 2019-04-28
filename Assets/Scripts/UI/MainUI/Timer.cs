using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Image First;
    public Image Second;
    public Image Colon;
    public Image Third;
    public Image Fourth;

    [Header("Sprites")]
    public Sprite[] Numbers;

    public void UpdateTimer(int first, int second, int third, int fourth)
    {
        if (first <= 0 && second <= 0 && third < 10)
        {
            First.color = Color.red;
            Second.color = Color.red;
            Colon.color = Color.red;
            Third.color = Color.red;
            Fourth.color = Color.red;
        }
        else
        {            
            First.color = Color.white;
            Second.color = Color.white;
            Colon.color = Color.white;
            Third.color = Color.white;
            Fourth.color = Color.white;
        }
        
        First.sprite = Numbers[first];
        Second.sprite = Numbers[second];
        Third.sprite = Numbers[third];
        Fourth.sprite = Numbers[fourth];
    }
}
