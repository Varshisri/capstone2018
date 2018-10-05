using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PluginWrapper : MonoBehaviour {

    private AndroidJavaObject androidInstance = null;
    private AndroidJavaObject activityContext = null;
    // Use this for initialization
    void Start()
    {
        if (androidInstance == null)
        {
            using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            }

            using (AndroidJavaObject pluginClass = new AndroidJavaObject("com.example.pluginlibrary.PluginTest"))
            {
                if (pluginClass != null)
                {
                    androidInstance = pluginClass.CallStatic<AndroidJavaObject>("instance");
                    androidInstance.Call("setContext", activityContext);
                    androidInstance.Call("GetMessageFromUnity");
                    androidInstance.Call<string>("GetWifiInformation");
                    activityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                    {
                        androidInstance.Call("ShowMessage", "Plugin Active!");
                    }));
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //public static AndroidJavaObject GetAndroidActivity()
    //{
    //    AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    //    AndroidJavaObject activity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
    //    return activity;
    //}

    // private AndroidJavaObject androidJavaClass = null;

}
