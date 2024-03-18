using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class IntertitialAds : MonoBehaviour,IUnityAdsLoadListener,IUnityAdsShowListener
{
    [SerializeField] private string androidID = "Interstitial_Android";
    [SerializeField] private string IosID = "Interstitial_iOS";

    private string adID;

    void Awake()
    {
        adID = (Application.platform == RuntimePlatform.IPhonePlayer) 
            ? IosID 
            :  androidID;

        LoadAD();
    }

    public void LoadAD()
    {
        Debug.Log("Loading Ad: " + adID);
        Advertisement.Load(adID, this);
    }
    public void ShowAd()
    {
        Debug.Log("Showing Ad: " + adID);
        Advertisement.Show(adID, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        LoadAD();
    }
}
