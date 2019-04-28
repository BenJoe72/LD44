using System;
using System.Linq;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    private bool _onBloodPlace;
    private bool _onPlasmaPlace;
    private bool _onShopPlace;
    private bool _onUpgradePlace;

    public float FoodPrice;
    public float[] UpgradePrices;
    public float PricePerPlasma;
    private int _currentUpgrade;
    private string[] _upgrades = new string[]{
        "AB+",
        "A+",
        "0+",
        "0-"
    };

    private void Start()
    {
        _currentUpgrade = 0;

        PlayerDataScript.Blood = PlayerDataScript.MaxBlood;
        PlayerDataScript.Plasma = PlayerDataScript.MaxPlasma;
        PlayerDataScript.SavedPpl = 0;
        PlayerDataScript.Money = 0f;
        PlayerDataScript.BloodType = _upgrades[_currentUpgrade];
    }

    private PlayerDataScript _playerdataScript;
    private PlayerDataScript PlayerDataScript
    {
        get
        {
            if (_playerdataScript == null)
                _playerdataScript = GetComponent<PlayerDataScript>();
            return _playerdataScript;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (_onBloodPlace)
            {
                if (PlayerDataScript.Blood >= PlayerDataScript.MaxBlood / 10f)
                {
                    PlayerDataScript.Blood -= PlayerDataScript.MaxBlood / 10f;
                    PlayerDataScript.Plasma = Mathf.Max(PlayerDataScript.Plasma - PlayerDataScript.MaxPlasma / 10f, 0f);
                    PlayerDataScript.SavedPpl += UnityEngine.Random.Range((int)1, (int)4);
                }
            }
            if (_onPlasmaPlace)
            {
                PlayerDataScript.Money += PricePerPlasma * PlayerDataScript.Plasma;
                PlayerDataScript.Plasma = 0f;
            }
            if (_onShopPlace)
            {
                if (PlayerDataScript.Money >= FoodPrice && PlayerDataScript.Food.Any(x => !x))
                {
                    int currentFoodSpace = Array.FindIndex(PlayerDataScript.Food, x => !x);
                    PlayerDataScript.Money -= FoodPrice;
                    PlayerDataScript.SetFood(currentFoodSpace);
                    currentFoodSpace++;
                }
            }
            if (_onUpgradePlace)
            {
                // AB+ A+ 0+ 0-
                if (_currentUpgrade < 3 && PlayerDataScript.Money >= UpgradePrices[_currentUpgrade])
                {
                    PlayerDataScript.Money -= UpgradePrices[_currentUpgrade];
                    _currentUpgrade++;
                    PlayerDataScript.BloodType = _upgrades[_currentUpgrade];
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha1) && PlayerDataScript.Food[0])
            PlayerDataScript.UseFood(0);
        if(Input.GetKeyDown(KeyCode.Alpha2) && PlayerDataScript.Food[1])
            PlayerDataScript.UseFood(1);
        if(Input.GetKeyDown(KeyCode.Alpha3) && PlayerDataScript.Food[2])
            PlayerDataScript.UseFood(2);
        if(Input.GetKeyDown(KeyCode.Alpha4) && PlayerDataScript.Food[3])
            PlayerDataScript.UseFood(3);
        if(Input.GetKeyDown(KeyCode.Alpha5) && PlayerDataScript.Food[4])
            PlayerDataScript.UseFood(4);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("MoneyPickup"))
        {
            other.GetComponentInChildren<Renderer>().enabled = false;
            other.GetComponentInChildren<Collider>().enabled = false;
            other.GetComponentInChildren<SpriteRenderer>().enabled = false;
            other.GetComponentInChildren<ParticleSystem>().Play();
            GetComponent<PlayerDataScript>().Money += other.GetComponent<Dollarsign>().Money;

            Destroy(other.gameObject, 2f);
        }
        else if (other.tag.Equals("BloodPlace"))
        {
            other.transform.Find("Prompt").GetComponent<SpriteRenderer>().enabled = true;
            _onBloodPlace = true;
        }
        else if (other.tag.Equals("PlasmaPlace"))
        {
            other.transform.Find("Prompt").GetComponent<SpriteRenderer>().enabled = true;
            _onPlasmaPlace = true;
        }
        else if (other.tag.Equals("ShopPlace"))
        {
            other.transform.Find("Prompt").GetComponent<SpriteRenderer>().enabled = true;
            _onShopPlace = true;
        }
        else if (other.tag.Equals("UpgradePlace"))
        {
            other.transform.Find("Prompt").GetComponent<SpriteRenderer>().enabled = true;
            _onUpgradePlace = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("BloodPlace"))
        {
            other.transform.Find("Prompt").GetComponent<SpriteRenderer>().enabled = false;
            _onBloodPlace = false;
        }
        else if (other.tag.Equals("PlasmaPlace"))
        {
            other.transform.Find("Prompt").GetComponent<SpriteRenderer>().enabled = false;
            _onPlasmaPlace = false;
        }
        else if (other.tag.Equals("ShopPlace"))
        {
            other.transform.Find("Prompt").GetComponent<SpriteRenderer>().enabled = false;
            _onShopPlace = false;
        }
        else if (other.tag.Equals("UpgradePlace"))
        {
            other.transform.Find("Prompt").GetComponent<SpriteRenderer>().enabled = false;
            _onUpgradePlace = false;
        }
    }
}