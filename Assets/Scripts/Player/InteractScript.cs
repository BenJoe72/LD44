using UnityEngine;

public class InteractScript : MonoBehaviour
{
    private bool _onBloodPlace;
    private bool _onPlasmaPlace;
    private bool _onShopPlace;
    private bool _onUpgradePlace;

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {

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
            Debug.Log("You are standing on the bloodspace!");
        }
        else if (other.tag.Equals("PlasmaPlace"))
        {
            other.transform.Find("Prompt").GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("You are standing on the plasmapalce!");
        }
        else if (other.tag.Equals("ShopPlace"))
        {
            other.transform.Find("Prompt").GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("You are standing on the shopplace!");
        }
        else if (other.tag.Equals("UpgradePlace"))
        {
            other.transform.Find("Prompt").GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("You are standing on the upgradeplace!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("BloodPlace"))
        {
            other.transform.Find("Prompt").GetComponent<SpriteRenderer>().enabled = false;
            Debug.Log("Exited the bloodplace!");
        }
        else if (other.tag.Equals("PlasmaPlace"))
        {
            other.transform.Find("Prompt").GetComponent<SpriteRenderer>().enabled = false;
            Debug.Log("Exited the plasmapalce!");
        }
        else if (other.tag.Equals("ShopPlace"))
        {
            other.transform.Find("Prompt").GetComponent<SpriteRenderer>().enabled = false;
            Debug.Log("Exited the shopplace!");
        }
        else if (other.tag.Equals("UpgradePlace"))
        {
            other.transform.Find("Prompt").GetComponent<SpriteRenderer>().enabled = false;
            Debug.Log("Exited the upgradeplace!");
        }
    }
}