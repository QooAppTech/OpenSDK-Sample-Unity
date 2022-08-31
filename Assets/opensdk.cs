using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class opensdk : MonoBehaviour
{
    // Start is called before the first frame update
    AndroidJavaClass jc;
    static AndroidJavaObject androidJo;
    static AndroidJavaObject qooAppUnity;

    void Start()
    {
        jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        Debug.Log("opensdk jc2 = " + jc);
        androidJo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        Debug.Log("opensdk androidJo = " + androidJo);

        

        AndroidJavaObject logUtils = new AndroidJavaObject("com.qooapp.opensdk.m.f");
        logUtils.SetStatic("a", true);

        AndroidJavaObject config = new AndroidJavaObject("com.qooapp.opensdk.m.b");
        // Config.URL_SDK a
        config.SetStatic("a", "https://testing-open-sdk.qoo-app.com");
        // Config.URL_SDK_SANDBOX b
        config.SetStatic("b", "https://testing-open-sdk-sandbox.qoo-app.com");
        // Config.URL_LOGIN c
        config.SetStatic("c", "https://testing-sso.qoo-app.com");

        // Config.PAYMENT_URL_SDK d
        config.SetStatic("d", "https://testing-payment-platform.qoo-app.com");
        // Config.PAYMENT_URL_SDK_SANDBOX e
        config.SetStatic("e", "https://testing-sandbox-payment-platform.qoo-app.com");

        AndroidJavaClass encryptionClass = new AndroidJavaClass("com.qooapp.opensdk.m.d");
        // Encryption.decode com.qooapp.opensdk.m.d.a("") testing
        string packageName = encryptionClass.CallStatic<string>("a", "Y29tLnFvb2FwcC5xb29oZWxwZXIudGVzdGZhaXJ5");

        // Config.QOOAPP_PACKAGE_NAME f
        config.SetStatic("f", packageName);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        GUI.color = Color.white;
        GUI.backgroundColor = Color.green;
        GUI.skin.textField.fontSize = 40;

        GUI.color = Color.white;
        GUI.backgroundColor = Color.green;
        GUI.skin.button.fontSize = 40;

        float xpos = 300;
        float ypos = 100;
        float width = 640;
        float heigth = 150;
        float space = heigth + 50;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "initialize"))
        {
            OpenSDKCallback initCallback = new OpenSDKCallback();
            AndroidJavaClass openClass = new AndroidJavaClass("com.qooapp.opensdk.QooAppOpenSDK");
            qooAppUnity = openClass.CallStatic<AndroidJavaObject>("initialize", initCallback, androidJo);
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
            string product_id = "zhl_01";// 
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
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "logout"))
        {

            OpenSDKCallback requestCallback = new OpenSDKCallback();
            // 
            qooAppUnity.Call("logout", requestCallback, androidJo);

        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "openGameDetail page in QooApp"))
        {
            qooAppUnity.Call("openGameDetail", androidJo);

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