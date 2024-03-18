using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class CheckBuy : MonoBehaviour
{
    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == "JoyFusion.DashDashBall.removeads")
        {
            RemoveAD();
        }
    }

    public void RemoveAD()
    {
        PlayerPrefs.SetInt("BuyedRemoveAD", 1);
    }
}
