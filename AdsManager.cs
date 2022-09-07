using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [Header("Android ID")]
    [SerializeField] string _androidGameId;
    [SerializeField] string interstitialAndroidAdId = "Interstitial_Android";
    [SerializeField] string rewardedAndroidAdId = "Rewarded_Android";

    [Header("IOS ID")]
    [SerializeField] string _iOSGameId;
    [SerializeField] string interstitialIOSAdId = "Interstitial_iOS";
    [SerializeField] string rewardedIOSAdId = "Rewarded_iOS";


    string _gameId,_interstitialAdId, _rewardedAdId;
    [SerializeField] bool _testMode = true;

    private void Awake()
    {

        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _interstitialAdId = interstitialIOSAdId;
        _rewardedAdId = rewardedIOSAdId;
#elif UNITY_ANDROID
        _interstitialAdId = interstitialAndroidAdId;
        _rewardedAdId = rewardedAndroidAdId;
#endif

        if (Advertisement.isInitialized)
        {
            Debug.Log("Advertisement is Initialized");
            LoadRewardedAd();
            LoadInerstitialAd();
        }
        else
        {
            InitializeAds();
        }
    }
    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSGameId : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadInerstitialAd(); 
        LoadRewardedAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void LoadInerstitialAd()
    {
        Advertisement.Load(_interstitialAdId, this);
    }

    public void LoadRewardedAd()
    {
        Advertisement.Load(_rewardedAdId, this);
    }

    // Show the loaded content in the Ad Unit:
    public void ShowInerstitialAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Inerstitial Ad: " + _interstitialAdId);
        Advertisement.Show(_interstitialAdId, this);
    }

    // Show the loaded content in the Ad Unit:
    public void ShowRewardedAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Inerstitial Ad: " + _rewardedAdId);
        Advertisement.Show(_rewardedAdId, this);       
    }

    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
        if (adUnitId.Equals(_rewardedAdId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            // Enable the button for users to click:
        }
    }
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("OnUnityAdsShowComplete " + showCompletionState);
        if (_gameId.Equals(_rewardedAdId) && UnityAdsShowCompletionState.COMPLETED.Equals(showCompletionState))
        {
            Debug.Log("rewared Player");
        }
        LoadRewardedAd();
    }

}