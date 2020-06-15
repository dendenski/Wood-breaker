using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class GoogleMobileAdsDemoScript : MonoBehaviour
{
    private BannerView bannerView;
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
    }
    private void RequestBanner()
    {
        //actual ads
/*         #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3037190026293447/2189039987";
        #else
            string adUnitId = "unexpected_platform";
        #endif */
        //test ads
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        #else
            string adUnitId = "unexpected_platform";
        #endif
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
    }
    public void destroyAds(){
        bannerView.Destroy();
    }
}
