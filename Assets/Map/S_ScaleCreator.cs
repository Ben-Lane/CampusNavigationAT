using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Notifications.Android;
using Unity.VisualScripting;
using UnityEngine;


public class S_ScaleCreator : MonoBehaviour
{
    public GameObject TranslationPoint1;
    public GameObject TranslationPoint2;

    public float scale;

    //Translation Point Locations
    private Vector2 TP1_WC;
    private Vector2 TP1_GC;

    //Translation Point Locations
    private Vector2 TP2_WC;
    private Vector2 TP2_GC;

    // Translation Point Flattens
    private Vector2 TP1_flat_world;
    private Vector2 TP2_flat_world;

    // Start is called before the first frame update
    void Start()
    {
        // Get Both World Coordinates (Longitude + Latitude)
        TP1_WC = new Vector2(TranslationPoint1.GetComponent<S_WorldCoordinates>().longitude, TranslationPoint1.GetComponent<S_WorldCoordinates>().latitude);
        TP2_WC = new Vector2(TranslationPoint2.GetComponent<S_WorldCoordinates>().longitude, TranslationPoint2.GetComponent<S_WorldCoordinates>().latitude);

        // Get Both Game Coordinates (x + y)
        TP1_GC = new Vector2(TranslationPoint1.transform.position.x, TranslationPoint1.transform.position.z);
        TP2_GC = new Vector2(TranslationPoint2.transform.position.x, TranslationPoint2.transform.position.z);

        // Get Both World Coordinates Flattened
        TP1_flat_world = FlattenGPS(TP1_WC);
        TP2_flat_world = FlattenGPS(TP2_WC);

        print(TP2_WC);
        print(TP2_GC);
        print(GametoIRL(TP2_GC));
        print(IRLtoGame(TP2_WC));
        
    }

    public Vector2 FlattenGPS(Vector2 coordinates)
    {
        //float Scalar = TP1_GC.x / (Mathf.Cos(TP1_WC.y) * Mathf.Cos(TP1_WC.x)); // finds scale from longitude latitude and 1 game coordinate point

        float flat_long = 6378137 * coordinates.x * (Mathf.PI / 180); // 6378137 = earth radius in metres
        float flat_lat = 6378137 * Mathf.Log10(Mathf.Tan((Mathf.PI / 4) + (coordinates.y * (Mathf.PI / 360))));

        return new Vector2(flat_long, flat_lat);
    }

    public Vector2 ReverseFlattenGPS(Vector2 coordinates)
    {
        float non_flat_long = (coordinates.x / 6378137) * (180 / Mathf.PI);
        float non_flat_lat = (Mathf.Atan(Mathf.Exp(coordinates.y / 6378137)) * 2 - Mathf.PI / 2) * (180 / Mathf.PI);

        return new Vector2(non_flat_long, non_flat_lat);
    }

    public Vector2 GametoIRL(Vector2 position)
    {

        float merc_x = TP1_flat_world.x + (position.x - TP1_GC.x) * (TP2_flat_world.x - TP1_flat_world.x) / (TP2_GC.x - TP1_GC.x);
        float merc_y = TP1_flat_world.y + (position.y - TP1_GC.y) * (TP2_flat_world.y - TP1_flat_world.y) / (TP2_GC.y - TP1_GC.y);

        Vector2 ReverseFlattenPos = ReverseFlattenGPS(new Vector2(merc_x, merc_y));

        return ReverseFlattenPos;
    }

    public Vector2 IRLtoGame(Vector2 position)
    {
        Vector2 NewFlatten = FlattenGPS(position);

        float game_x = TP1_GC.x + (NewFlatten.x - TP1_flat_world.x) * (TP2_GC.x - TP1_GC.x) / (TP2_flat_world.x - TP1_flat_world.x);
        float game_y = TP1_GC.y + (NewFlatten.y - TP1_flat_world.y) * (TP2_GC.y - TP1_GC.y) / (TP2_flat_world.y - TP1_flat_world.y);

        Vector2 Coordinates = new Vector2(game_x, game_y);

        return Coordinates;
    }
}

