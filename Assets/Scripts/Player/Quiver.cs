using UnityEngine;
using UnityEngine.UI;
using Cainos.CustomizablePixelCharacter;
using TMPro;

public class Quiver : MonoBehaviour
{
    public int arrowCount = 0;
    public Image arrowIconImage;
    public TMP_Text arrowCountText;
    public GameObject arrowPrefab;

    public static Quiver Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        UpdateArrowUI();
    }

    public void AddArrows(int amount)
    {
        arrowCount += amount;
        UpdateArrowUI();

        var controller = FindObjectOfType<PixelCharacterController>();
        if (controller != null && controller.projectilePrefab == null && arrowPrefab != null)
        {
            controller.projectilePrefab = arrowPrefab;
        }
    }

    public bool TryUseArrow()
    {
        if (arrowCount > 0)
        {
            arrowCount--;
            UpdateArrowUI();
            return true;
        }
        else
        {
            var controller = FindObjectOfType<PixelCharacterController>();
            if (controller != null)
                controller.projectilePrefab = null;
        }
        return false;
    }

    public void UpdateArrowUI()
    {
        arrowIconImage.enabled = arrowCount > 0;
        arrowCountText.enabled = arrowCount > 0;
        arrowCountText.text = arrowCount.ToString();
    }
} 