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
    private int _currentFoodSpace;
    private string[] _upgrades = new string[]{
        "AB+",
        "A+",
        "0+",
        "0-"
    };

    private void Start()
    {
        _currentUpgrade = 0;
        _currentFoodSpace = 0;

        _playerdataScript.Blood = _playerdataScript.MaxBlood;
        _playerdataScript.Plasma = _playerdataScript.MaxPlasma;
        _playerdataScript.SavedPpl = 0;
        _playerdataScript.Money = 0f;
        _playerdataScript.BloodType = _upgrades[_currentUpgrade];
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
                if (_playerdataScript.Blood >= _playerdataScript.MaxBlood / 10f)
                {
                    _playerdataScript.Blood -= _playerdataScript.MaxBlood / 10f;
                    _playerdataScript.Plasma = Mathf.Max(_playerdataScript.Plasma - _playerdataScript.MaxPlasma / 10f, 0f);
                    _playerdataScript.SavedPpl += Random.Range((int)0, (int)3);
                }
            }
            if (_onPlasmaPlace)
            {
                _playerdataScript.Money += PricePerPlasma * _playerdataScript.Plasma;
                _playerdataScript.Plasma = 0f;
            }
            if (_onShopPlace)
            {
                if (_playerdataScript.Money >= FoodPrice && _currentFoodSpace < 4)
                {
                    _playerdataScript.Money -= FoodPrice;
                    _playerdataScript.Food[_currentFoodSpace] = true;
                    _currentFoodSpace++;
                }
            }
            if (_onUpgradePlace)
            {
                // AB+ A+ 0+ 0-
                if (_currentUpgrade < 3 && _playerdataScript.Money >= UpgradePrices[_currentUpgrade])
                {
                    _playerdataScript.Money -= UpgradePrices[_currentUpgrade];
                    _currentUpgrade++;

                }
            }
        }
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