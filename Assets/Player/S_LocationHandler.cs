using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class S_LocationHandler : MonoBehaviour
{
    public TextMeshProUGUI LocationUI;
    private Vector2 world_location; //longitude, latitude
    private Vector2 game_location; //units x, units y
    private bool isOnMobile;

    // GPS Data
    private S_GPS gps;

    // Start is called before the first frame update
    void Start()
    {
        //Setup GPS
        gps = GetComponent<S_GPS>();

        isOnMobile = CheckGPSAccessabilityPlatform();
        world_location = new Vector2(0.0f, 0.0f);
        game_location = new Vector2(transform.position.x, transform.position.z);

        if (isOnMobile)
        {
            LocationUI.text = world_location.x.ToString() + ", " + world_location.y.ToString() + " " + "(Using GPS)";
        }
        else
        {
            LocationUI.text = game_location.x.ToString() + ", " + game_location.y.ToString() + " " + "(Not Using GPS)";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CheckGPSAccessabilityPlatform()
    {
        if(gps.longitude == 0 && gps.latitude == 0)
        {
            return false; // If on PC or GPS unavailable
        }

        return true; // If on MOBILE and GPS available
    }
}
