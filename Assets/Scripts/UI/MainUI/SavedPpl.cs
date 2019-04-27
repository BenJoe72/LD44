using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SavedPpl : MonoBehaviour
{
    public RectTransform AddedSavedPpl;
    public Image PlusIcon;
    public Image[] AddedSavedPplIcons;
    public Image[] SavedPplNumberImages;

    [Header("Sprites")]
    public Sprite[] SavedPplNumbers;

    private Vector3 _savedPplStartPos;
    private float _saveDuration = 1f;

    private void Start()
    {
        AddedSavedPpl.gameObject.SetActive(false);
        _savedPplStartPos = AddedSavedPpl.localPosition;
    }

    public void AddSavedPpl(int addedAmount)
    {
        AddedSavedPpl.gameObject.SetActive(true);
        PlusIcon.color = Color.white;
        AddedSavedPpl.localPosition = _savedPplStartPos;

        Sequence something = DOTween.Sequence();

        something.Append(AddedSavedPpl.DOPunchScale(Vector3.one * .2f, .3f));
        something.Append(AddedSavedPpl.DOLocalMoveY(AddedSavedPpl.localPosition.y + 30f, _saveDuration));
        something.Join(PlusIcon.DOColor(new Color(1, 1, 1, 0), _saveDuration));
        for (int i = 0; i < AddedSavedPplIcons.Length; i++)
        {
            var item = AddedSavedPplIcons[i];
            item.color = Color.white;
            item.gameObject.SetActive(i < addedAmount);
            if (i < addedAmount)
                something.Join(item.DOColor(new Color(1, 1, 1, 0), _saveDuration));
        }

        something.AppendCallback(() => { AddedSavedPpl.gameObject.SetActive(false); });
    }

    public void SetSavedPpl(int newAmount)
    {
        int digits = newAmount.ToString().Length;
        for (int i = 0; i < SavedPplNumberImages.Length; i++)
        {
            if (i < digits)
            {
                SavedPplNumberImages[i].enabled = true;
                SavedPplNumberImages[i].sprite = SavedPplNumbers[newAmount.GetDigitAtPosition(i + 1)];
            }
            else
            {
                SavedPplNumberImages[i].enabled = false;
            }
        }
    }
}
