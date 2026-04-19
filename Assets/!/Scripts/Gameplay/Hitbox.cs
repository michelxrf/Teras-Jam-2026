using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Handle hit logic here, e.g., apply damage to the player
            Debug.Log("Player hit!");
        }
    }
}
