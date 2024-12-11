/*using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class InAppPurchases : MonoBehaviour
{
    public static InAppPurchases instance;

    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.


    public void InitializePurchasing()
    {
        if (IsInitialized()) { return; }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Template for consumables:
        builder.AddProduct(sampleItem, ProductType.Consumable);

        //Template for non-consumables
        builder.AddProduct(sampleUpgrade, ProductType.NonConsumable);

        //UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, sampleItem, StringComparison.Ordinal))
        {
            Debug.Log("Sample Item purchased!");
            //Code where we give item to player goes here
        }
        else if (String.Equals(args.purchasedProduct.definition.id, sampleUpgrade, StringComparison.Ordinal))
        {
            //Each separate purchase option will need its own elseif statement
            Debug.Log("Sample Upgrade purchased!");
            //Code where we give upgrade to player goes here
        }
        else
        {
            Debug.Log("Purchase Failed");
        }
        return PurchaseProcessingResult.Complete;
    }


    // ******* PRODUCTS GO HERE *********** //

    //Example of product
    private string sampleItem = "sample_Item";
    private string sampleUpgrade = "sample_upgrade";

    //Each product will need its own method

    *//*public void BuySampleItem()
    {
        BuyProductID(sampleItem);
    }

    // ---------- Use the following code when calling these methods: -------------

    public enum PurchaseType { sampleItem, sampleUpgrade }; //All available purchases will need to be enumerated in this enum declaration
    public PurchaseType purchasetype;

    public void ClickPurchaseButton()
    {
        switch (purchaseType)
        {
            case PurchaseType.sampleItem:
                instance.BuySampleItem;
                break;
            case PurchaseType.sampleUpgrade():
                InAppPurchases.instance.BuySampleUpgrade;
                break;
            default:
                Debug.Log("Unknown purchase requested");
        }
    }*//*


}
*/