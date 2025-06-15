using UnityEngine;

public class InputTester : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown) Debug.Log("✅ anyKeyDown");
        if (Input.GetKeyDown(KeyCode.Space)) Debug.Log("✅ SPACE");
        if (Input.GetKeyDown(KeyCode.E)) Debug.Log("✅ E");
    }
}