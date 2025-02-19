using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;


public class S_GPS : MonoBehaviour
{
    // CAN MAKE THIS SINGLETON IF NEEDED (RECOMMENDED)

    public float longitude;
    public float latitude;

    // Start is called before the first frame update
    void Start()
    {
        longitude = 0.0f;
        latitude = 0.0f;

        // Begin accessing location
        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        // 1. Check for permissions
        if(!Input.location.isEnabledByUser)
        {
            Debug.Log("User has not enabled permissions");
            yield break;
        }

        // 2. Start location services
        Input.location.Start();
        int maxWaitTime = 20;
        while(Input.location.status == LocationServiceStatus.Initializing && maxWaitTime > 0) //waits for x amount of seconds 
        {
            yield return new WaitForSeconds(1);
            maxWaitTime--;
        }
            
        // 3. Check for successful connection
        if(maxWaitTime <= 0) // Timed out due to taking too long
        {
            Debug.Log("Timed out");
            yield break;

        }

        if(Input.location.status == LocationServiceStatus.Failed) // Timed out due to not being succesful
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }

        // 4. Access longitude and latitude
        longitude = Input.location.lastData.longitude;
        latitude = Input.location.lastData.latitude;

        print("longitude: " + longitude.ToString());
        print("latitude: " + latitude.ToString());
    }

    
}
