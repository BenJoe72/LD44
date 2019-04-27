using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LifeBars : MonoBehaviour
{
    public Image BloodFill;
    public Image PlasmaFill;

    public Image[] BloodTypeImages;

    [Header("Sprites")]
    public Sprite[] BloodTypeLetters;

    public void SetBloodPercent(float percent)
    {
        float duration = Mathf.Abs(BloodFill.fillAmount - percent) * 2f;
        BloodFill.DOFillAmount(percent, duration);
    }

    public void SetPlasmaPercent(float percent)
    {
        float duration = Mathf.Abs(PlasmaFill.fillAmount - percent) * 2f;
        PlasmaFill.DOFillAmount(percent, duration);
    }

    public void SetBloodTypeLetters(string first = null, string second = null, string third = null)
    {
        if (BloodTypeImages[0].enabled = !string.IsNullOrWhiteSpace(first))
        {
            BloodTypeImages[0].sprite = BloodTypeLetters[_bloodTypeLookup[first]];
        }
        if (BloodTypeImages[1].enabled = !string.IsNullOrWhiteSpace(second))
        {
            BloodTypeImages[1].sprite = BloodTypeLetters[_bloodTypeLookup[second]];
        }
        if (BloodTypeImages[2].enabled = !string.IsNullOrWhiteSpace(third))
        {
            BloodTypeImages[2].sprite = BloodTypeLetters[_bloodTypeLookup[third]];
        }
    }

    private Dictionary<string, int> _bloodTypeLookup = new Dictionary<string, int>{
        {"0", 0},
        {"A", 1},
        {"B", 2},
        {"-", 3},
        {"+", 4}
    };
}
