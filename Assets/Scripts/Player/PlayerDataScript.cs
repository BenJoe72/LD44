using System;
using UnityEngine;

public class PlayerDataScript : MonoBehaviour
{
    public Inventory InventoryScript;
    public LifeBars LifeBarsScript;
    public SavedPpl SavedPplScript;
    public Timer TimerScript;

    public float BloodPerFood;
    public float PlasmaPerFood;
    public float BloodRegenRate;
    public float PlasmaRegenRate;
    public float TimePerDonation;
    public float MaxPlasma;
    public float MaxBlood;
    public float StartTimer;

    private float nextRegen;
    private float Timer;
    private float _money;
    private float _blood;
    private float _plasma;
    private int _savedPpl;
    private string _bloodType;
    private bool[] _food;

    private void Start()
    {
        Timer = StartTimer;
        UpdateTimer();
    }

    private void Update()
    {
        if (nextRegen < Time.time)
        {
            nextRegen = Time.time + 1;
            if (Blood < MaxBlood)
            {
                Blood += BloodRegenRate;
            }
            if (Plasma < MaxPlasma)
            {
                Plasma += PlasmaRegenRate;
            }
        }

        Timer -= Time.deltaTime;
        UpdateTimer();
        CheckIsAlive();
    }

    private void UpdateTimer()
    {
        int seconds = (int)(Timer % 60);
        int minutes = (int)(Timer / 60f);
        TimerScript.UpdateTimer(minutes.GetDigitAtPosition(2), minutes.GetDigitAtPosition(1), seconds.GetDigitAtPosition(2), seconds.GetDigitAtPosition(1));
    }

    private bool CheckIsAlive()
    {
        bool alive = Timer > 0f;
        alive &= Blood > 0f;
        return alive;
    }

    public float Money
    {
        get { return _money; }
        set
        {
            _money = value;
            InventoryScript.SetMoney(_money);
        }
    }

    public float Blood
    {
        get { return _blood; }
        set
        {
            _blood = Math.Min(value, MaxBlood);
            CheckIsAlive();
            LifeBarsScript.SetBloodPercent(_blood / MaxBlood);
        }
    }

    public float Plasma
    {
        get { return _plasma; }
        set
        {
            _plasma = Math.Min(value, MaxPlasma);
            LifeBarsScript.SetPlasmaPercent(_plasma / MaxPlasma);
        }
    }

    public int SavedPpl
    {
        get { return _savedPpl; }
        set
        {
            int diff = value - _savedPpl;
            _savedPpl = value;
            Timer += TimePerDonation;
            UpdateTimer();
            SavedPplScript.AddSavedPpl(diff);
            SavedPplScript.SetSavedPpl(_savedPpl);
        }
    }

    public string BloodType
    {
        get { return _bloodType; }
        set
        {
            _bloodType = value;
            string first = _bloodType.Length > 0 ? _bloodType[0].ToString() : null;
            string second = _bloodType.Length > 1 ? _bloodType[1].ToString() : null;
            string third = _bloodType.Length > 2 ? _bloodType[2].ToString() : null;
            LifeBarsScript.SetBloodTypeLetters(first, second, third);
        }
    }

    public void SetFood(int foodNumber)
    {
        Food[foodNumber] = true;
        InventoryScript.SetInventoryIcon(foodNumber);
    }

    public void UseFood(int foodNumber)
    {
        Blood += BloodPerFood;
        Plasma += PlasmaPerFood;
        Food[foodNumber] = false;
        InventoryScript.ResetInventoryIcon(foodNumber);
    }

    public bool[] Food
    {
        get
        {
            if (_food == null)
                _food = new bool[5];
            return _food;
        }
        set
        {
            _food = value;
        }
    }
}