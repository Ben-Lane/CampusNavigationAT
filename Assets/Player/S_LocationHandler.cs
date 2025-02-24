using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml.Serialization;

public class S_LocationHandler : MonoBehaviour
{
    public TextMeshProUGUI LocationUI;
    private Vector2 world_location; //longitude, latitude
    private Vector2 game_location; //units x, units y
    private bool isOnMobile;

    // GPS Data
    private S_GPS gps;

    // Location Scale System
    private S_ScaleCreator locationScaler;

    // Start is called before the first frame update
    void Start()
    {
        //Setup GPS
        gps = GetComponent<S_GPS>();
        isOnMobile = CheckGPSAccessabilityPlatform();

        //acquire world scale
        locationScaler = GetComponent<S_ScaleCreator>();

        //Setup Locations
        game_location = new Vector2(transform.position.x, transform.position.z);           

        if (isOnMobile)
        {
            WithinMap(); // Check the player is within the map if the GPS is on, if not, dont continue

            // Find game location for their world location
            TeleportPlayer(game_location); // Teleport player to the corresponding game location
            LocationUI.text = world_location.x.ToString() + ", " + world_location.y.ToString() + " " + "(Using GPS)";
        }
        else
        {
            // find the world coordinate from game position
            LocationUI.text = world_location.x.ToString() + ", " + world_location.y.ToString() + " " + "(Not Using GPS)";
        }

        //print("Player position in game: " + transform.position);
        //print(locationScaler.GametoIRL(new Vector2(transform.position.x, transform.position.z)));
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CheckGPSAccessabilityPlatform()
    {
        if(gps.longitude == 0 && gps.latitude == 0)
        {
            world_location = new Vector2(gps.longitude, gps.latitude);
            return false; // If on PC or GPS unavailable
        }

        world_location = new Vector2(0.0f, 0.0f);
        return true; // If on MOBILE and GPS available
    }

    void TeleportPlayer(Vector2 location)
    {
        // teleport player to a specific location
    }

    bool WithinMap()
    {
        return true;
    }
}
