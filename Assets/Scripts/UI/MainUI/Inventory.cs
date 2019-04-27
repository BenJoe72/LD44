using System;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image[] InventoryIcons;
    public Image[] MoneyImagesWhole;
    public Image[] MoneyImagesDecimal;

    [Header("Numbers")]
    public Sprite[] MoneyNumbers;
    public Sprite DollarSign;
    public Sprite[] FoodItems;

    public void SetInventoryIcon(int inventoryNumber)
    {
        Sprite newSprite = FoodItems[UnityEngine.Random.Range(0, FoodItems.Length)];
        InventoryIcons[inventoryNumber].enabled = true;
        InventoryIcons[inventoryNumber].sprite = newSprite;
    }

    public void ResetInventoryIcon(int inventoryNumber)
    {
        InventoryIcons[inventoryNumber].enabled = false;
    }

    public void SetMoney(float amount)
    {
        int wholeAmount = (int)amount;
        int decAmount = (int)((amount - wholeAmount) * 100) + 1; // some correction for rounding

        SetMoneyWhole(wholeAmount);
        SetMoneyDecimal(decAmount);
    }

    private void SetMoneyDecimal(int decAmount)
    {
        for (int i = 0; i < MoneyImagesDecimal.Length; i++)
        {
            MoneyImagesDecimal[i].sprite = MoneyNumbers[decAmount.GetDigitAtPosition(i+1)];
        }
    }

    private void SetMoneyWhole(int wholeAmount)
    {
        int digitnumber = wholeAmount.ToString().Length;
        for (int i = 0; i < MoneyImagesWhole.Length; i++)
        {
            if (i < digitnumber)
            {
                MoneyImagesWhole[i].enabled = true;
                MoneyImagesWhole[i].sprite = MoneyNumbers[wholeAmount.GetDigitAtPosition(i+1)];
            }
            else if (i== digitnumber)
            {
                MoneyImagesWhole[i].enabled = true;
                MoneyImagesWhole[i].sprite = DollarSign;
            }
            else
            {
                MoneyImagesWhole[i].enabled = false;
            }
        }
    }
}