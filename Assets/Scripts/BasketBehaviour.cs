using UnityEngine;

public class BasketBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<AppleBehaviour>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}