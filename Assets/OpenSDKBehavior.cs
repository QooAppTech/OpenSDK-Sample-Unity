using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSDKBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    AndroidJavaClass jc;
    static AndroidJavaObject androidJo;
    static AndroidJavaObject qooAppUnity;

    // Start is called before the first frame update
    void Start()
    {
        jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        Debug.Log("opensdk jc2 = " + jc);
        androidJo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        Debug.Log("opensdk androidJo = " + androidJo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }private void OnGUI()
    {
        GUI.color = Color.white;
        GUI.backgroundColor = Color.green;
        GUI.skin.textField.fontSize = 40;

        GUI.color = Color.white;
        GUI.backgroundColor = Color.green;
        GUI.skin.button.fontSize = 40;

        float xpos = 300;
        float ypos = 50;
        float width = 640;
        float heigth = 120;
        float space = heigth + 10;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "initialize"))
        {
            OpenSDKCallback initCallback = new OpenSDKCallback();
            AndroidJavaClass openClass = new AndroidJavaClass("com.qooapp.opensdk.QooAppOpenSDK");
            qooAppUnity = openClass.CallStatic<AndroidJavaObject>("initialize", initCallback, androidJo);
        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "login"))
        {

            OpenSDKCallback loginCallback = new OpenSDKCallback();
            qooAppUnity.Call("login", loginCallback, androidJo);

        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "logout"))
        {

            OpenSDKCallback requestCallback = new OpenSDKCallback();
            // 
            qooAppUnity.Call("logout", requestCallback, androidJo);

        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "latestVersionCode"))
        {
            OpenSDKCallback versionCallback = new OpenSDKCallback();
            qooAppUnity.Call("latestVersionCode", versionCallback);

        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "openGameDetail page in QooApp"))
        {
            qooAppUnity.Call("openGameDetail", androidJo);

        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "checkLicense"))
        {

            OpenSDKCallback verifyCallback = new OpenSDKCallback();
            qooAppUnity.Call("checkLicense", verifyCallback);

        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "queryPageProducts"))
        {

            Debug.Log("opensdk in queryPageProducts ");
            OpenSDKCallback requestCallback = new OpenSDKCallback();
            Debug.Log("opensdk queryPageProducts start call");
            //
            int pageIndex = 1;
            qooAppUnity.Call("queryProducts", requestCallback, pageIndex);

            Debug.Log("opensdk queryPageProducts after call = ");

        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "queryProducts"))
        {

            Debug.Log("opensdk in QueryProduct ");
            OpenSDKCallback requestCallback = new OpenSDKCallback();
            Debug.Log("opensdk QueryProduct start call");
            // 
            qooAppUnity.Call("queryProducts", requestCallback);

            Debug.Log("opensdk QueryProduct after call = ");

        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "queryProductsInfo"))
        {

            OpenSDKCallback paymentCallback = new OpenSDKCallback();
            // please replace product_id with real value
            string product_id = "zhl_01";// 
            qooAppUnity.Call("queryProductsInfo", paymentCallback, product_id);

        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "purchase"))
        {

            PurchaseCallback paymentCallback = new PurchaseCallback();
            // please replace product_id with real value
            string product_id = "111";// 
            qooAppUnity.Call("purchase", paymentCallback, androidJo, product_id);

        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "consume"))
        {

            OpenSDKCallback requestCallback = new OpenSDKCallback();
            // please replace token and purchase_id with real value
            string token = "";
            string purchase_id = "";
            qooAppUnity.Call("consume", requestCallback, purchase_id, token);

        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "restorePurchases"))
        {

            OpenSDKCallback requestCallback = new OpenSDKCallback();
            // 
            qooAppUnity.Call("restorePurchases", requestCallback);

        }
    }

    private static void showToast(object str)
    {
        AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", str.ToString());
        Debug.Log(javaString);

        AndroidJavaClass toast = new AndroidJavaClass("android.widget.Toast");
        toast.CallStatic<AndroidJavaObject>("makeText", androidJo, javaString, toast.GetStatic<int>("LENGTH_SHORT")).Call("show");
    }


    public class OpenSDKCallback : AndroidJavaProxy
    {
        public OpenSDKCallback() : base("com.qooapp.opensdk.common.QooAppCallback")
        {
        }

        void onSuccess(string response)
        {
            showToast("opensdk onSuccess response = " + response);
        }

        void onError(string error)
        {
            showToast("opensdk onError error = " + error);
        }
    }

    public class PurchaseCallback : AndroidJavaProxy
    {
        public PurchaseCallback() : base("com.qooapp.opensdk.common.PaymentCallback")
        {

        }
        void onComplete(string response)
        {
            showToast("opensdk PurchaseCallback response = " + response);
        }

        void onError(string error)
        {
            showToast("opensdk PurchaseCallback error = " + error);
        }

        void onCancel()
        {
            showToast("opensdk PurchaseCallback onCancel()");
        }
    }
}
