using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BL_MapScript : MonoBehaviour {

    string url = "";
    public float lat = 29.577785f;
    public float lon = -95.104192f;

    public int zoom = 14;
    public int scale;
    public int mapWidth = 640;
    public int mapHeight = 640;

    public string GoogleApiKey;
    public enum mapTypes {roadmap, satellite, hybrid, terrain }
    public mapTypes mapSelected;

    private bool loadingMap = false;
    private IEnumerator mapCoroutine;
    

    LocationInfo li;

    IEnumerator GetGoogleMap(float lat, float lon)
    {
        //url = "https://maps.googleapis.com/maps/api/staticmap?center=Berkeley,CA&zoom=14&size=400x400&key=AIzaSyClbn42pXuzc1o3r6IS2gBUQtEQ7Vh-yh0";
        url = "https://maps.googleapis.com/maps/api/staticmap?center="+lat+","+lon+
            "&zoom="+zoom+"&size="+ mapWidth +"x"+ mapHeight+"&scale="+scale+"&maptype="+mapSelected+"&key=AIzaSyClbn42pXuzc1o3r6IS2gBUQtEQ7Vh-yh0";
        loadingMap = true;
        WWW www = new WWW(url);
        yield return www;
        loadingMap = false;
        gameObject.GetComponent<RawImage>().texture = www.texture;
        StopCoroutine(mapCoroutine);
    }

    
    // Use this for initialization
	void Start () {
        Debug.Log("New Map");
        mapCoroutine = GetGoogleMap(lat, lon);
        StartCoroutine(mapCoroutine);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("New Map");
            lat = 29.582542f;
            lon = -95.098220f;
            mapCoroutine = GetGoogleMap(lat, lon);
            StartCoroutine(mapCoroutine);

        }
	}
}
