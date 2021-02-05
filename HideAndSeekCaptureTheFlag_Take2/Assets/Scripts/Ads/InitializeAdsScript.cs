using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAdsScript : MonoBehaviour
{

    string gameId = "4002553";
    bool testMode = true;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }
}
