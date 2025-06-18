using UnityEngine;

public class TextController : MonoBehaviour
{
    public GameObject textObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textObject.SetActive(false);
        }
    }
}