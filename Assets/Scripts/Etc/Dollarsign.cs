using UnityEngine;

public class Dollarsign : MonoBehaviour
{
    public float RotationSpeed;
    public float Money;
    public float MinMoney;
    public float MaxMoney;

    private void Start()
    {
        Money = Random.Range(MinMoney, MaxMoney);
    }

    private void Update()
    {
        transform.Rotate(0, RotationSpeed, 0);
    }
}