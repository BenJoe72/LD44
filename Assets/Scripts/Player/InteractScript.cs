using UnityEngine;

public class InteractScript : MonoBehaviour
{
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
            Debug.Log("You are standing on the bloodplace!");
        }
        else if (other.tag.Equals("PlasmaPlace"))
        {
            Debug.Log("You are standing on the plasmapalce!");
        }
        else if (other.tag.Equals("ShopPlace"))
        {
            Debug.Log("You are standing on the shopplace!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("BloodPlace"))
        {
            Debug.Log("Exited the bloodplace!");
        }
        else if (other.tag.Equals("PlasmaPlace"))
        {
            Debug.Log("Exited the plasmapalce!");
        }
        else if (other.tag.Equals("ShopPlace"))
        {
            Debug.Log("Exited the shopplace!");
        }k
    }
}