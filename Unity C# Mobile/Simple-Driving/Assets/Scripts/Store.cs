using UnityEngine;
using UnityEngine.Purchasing;

public class Store : MonoBehaviour
{
    [SerializeField] GameObject _restoreButton;

    const string NewCarId = "com.xyowin.casualdriver.newcar";
    public const string NewCarUnlockedKey = "NewCarUnlocked";

    private void Awake()
    {
        if (Application.platform != RuntimePlatform.IPhonePlayer)
        {
            _restoreButton.SetActive(false);
        }
    }

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == NewCarId)
        {
            PlayerPrefs.SetInt(NewCarUnlockedKey, 1);
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason purchaseFailureReason)
    {
        Debug.LogWarning($"Purchase Failed {product.definition.id} - {purchaseFailureReason}");
    }
}
