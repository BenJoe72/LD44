using UnityEngine;

public class PlayerDataScript : MonoBehaviour
{
    public Inventory InventoryScript;
    public LifeBars LifeBarsScript;
    public SavedPpl SavedPplScript;

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
            _blood = value;
            LifeBarsScript.SetBloodPercent(_blood / MaxBlood);
        }
    }

    public float Plasma
    {
        get { return _plasma; }
        set
        {
            _plasma = value;
            LifeBarsScript.SetPlasmaPercent(_plasma / MaxPlasma);
        }
    }

    public int SavedPpl
    {
        get { return _savedPpl; }
        set
        {
            _savedPpl = value;
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

    private float _money;
    private float _blood;
    private float _plasma;
    private int _savedPpl;
    private string _bloodType;
    private bool[] _food;

    public float MaxPlasma;
    public float MaxBlood;
}