using UnityEngine;

public class PlayerDataScript : MonoBehaviour
{
    public Inventory InventoryScript;

    public float Money
    {
        get { return _money; }
        set
        {
            _money = value;
            InventoryScript.SetMoney(_money);
        }
    }

    private float _money;
}