using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;

public class ShowAdsManager : MonoBehaviour
{
    #if UNITY_IPHONE
	string idAdMobInterstitial = "";
	string idAdMobBanner = "";
	string testDeviceId = "";
	#endif

	#if UNITY_ANDROID
	public string idAdMobInterstitial;
	public string testDeviceId;
	#endif

	public static ShowAdsManager instance;
	InterstitialAd interstitial;

    private void Awake()
	{
		// Check if instance already exists
		if (instance == null)
        {
			// If not, set instance to this
			instance = this;

		}

		// If instance already exists and it's not this:
		else if (instance != this)
        {
			// Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);
        }

		// Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
	}

    // Start is called before the first frame update
    void Start()
    {
        LoadFullAdmob();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadFullAdmob()
	{
		// Create an empty ad request.
		interstitial = new InterstitialAd(idAdMobInterstitial);
		AdRequest request = new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator) // Simulator
			.AddTestDevice(testDeviceId) // My test device
			.Build();

		// Load the interstitial with the request.
		interstitial.LoadAd(request);
	}
	
	public void ShowFullAdmod()
	{
		interstitial.Show();
		LoadFullAdmob();
	}
}
