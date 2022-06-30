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
        float width = 400;
        float heigth = 180;
        float space = heigth + 50;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "initialize"))
        {
            InitCallback initCallback = new InitCallback();
            AndroidJavaClass openClass = new AndroidJavaClass("com.qooapp.opensdk.QooAppOpenSDK");
            qooAppUnity = openClass.CallStatic<AndroidJavaObject>("initialize", initCallback, androidJo);
        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "CheckLicense"))
        {

            CheckLicenseCallback verifyCallback = new CheckLicenseCallback();
            qooAppUnity.Call("checkLicense", verifyCallback);

        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "QueryProducts"))
        {

            Debug.Log("opensdk in QueryProduct ");
            QueryProductsCallback requestCallback = new QueryProductsCallback();
            Debug.Log("opensdk QueryProduct start call");
            // 
            qooAppUnity.Call("queryProducts", requestCallback);

            Debug.Log("opensdk QueryProduct after call = ");

        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "Purchase"))
        {

            PurchaseCallback paymentCallback = new PurchaseCallback();
            // please replace product_id with real value
            string product_id = "zhl_002";// 
            qooAppUnity.Call("purchase", paymentCallback, androidJo, product_id);

        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "Consume"))
        {

            ConsumeCallback requestCallback = new ConsumeCallback();
            // please replace token and purchase_id with real value
            string token = "";
            string purchase_id = "";
            qooAppUnity.Call("consume", requestCallback, purchase_id, token);

        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "RestorePurchases"))
        {

            RestorePurchasesCallback requestCallback = new RestorePurchasesCallback();
            // 
            qooAppUnity.Call("restorePurchases", requestCallback);

        }
        ypos += space;
        if (GUI.Button(new Rect(xpos, ypos, width, heigth), "Logout"))
        {

            LogoutCallback requestCallback = new LogoutCallback();
            // 
            qooAppUnity.Call("logout", requestCallback, androidJo);

        }
    }

    private static void showToast(object str)
    {
        AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", str.ToString());
        Debug.Log(javaString);

        AndroidJavaClass toast = new AndroidJavaClass("android.widget.Toast");
        toast.CallStatic<AndroidJavaObject>("makeText", androidJo, javaString, toast.GetStatic<int>("LENGTH_SHORT")).Call("show");
    }

    public class InitCallback : AndroidJavaProxy
    {
        public InitCallback() : base("com.qooapp.opensdk.common.QooAppCallback")
        {
        }

        void onSuccess(string response)
        {
            showToast("opensdk InitCallback response = " + response);
        }

        void onError(string error)
        {
            showToast("InitCallback error = " + error);
        }
    }

    public class CheckLicenseCallback : AndroidJavaProxy
    {
        public CheckLicenseCallback() : base("com.qooapp.opensdk.common.QooAppCallback")
        {
        }

        void onSuccess(string response)
        {
            showToast("opensdk CheckLicenseCallback response = " + response);
        }

        void onError(string error)
        {
            showToast("opensdk CheckLicenseCallback error = " + error);
        }
    }

    public class LoginCallback : AndroidJavaProxy
    {
        public LoginCallback() : base("com.qooapp.opensdk.common.QooAppCallback")
        {
        }

        void onSuccess(string response)
        {
            showToast("opensdk LoginCallback response = " + response);
        }

        void onError(string error)
        {
            showToast("opensdk LoginCallback error = " + error);
        }
    }

    public class QueryProductsCallback : AndroidJavaProxy
    {
        public QueryProductsCallback() : base("com.qooapp.opensdk.common.QooAppCallback")
        {
        }

        void onSuccess(string response)
        {
            Debug.Log("queryProducts: "+ response);
            showToast("opensdk QueryProductsCallback response = " + response);
        }

        void onError(string error)
        {
            showToast("opensdk QueryProductsCallback error = " + error);
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

    public class ConsumeCallback : AndroidJavaProxy
    {
        public ConsumeCallback() : base("com.qooapp.opensdk.common.QooAppCallback")
        {
        }

        void onSuccess(string response)
        {
            showToast("opensdk ConsumeCallback response = " + response);
        }

        void onError(string error)
        {
            showToast("opensdk ConsumeCallback error = " + error);
        }
    }

    public class RestorePurchasesCallback : AndroidJavaProxy
    {
        public RestorePurchasesCallback() : base("com.qooapp.opensdk.common.QooAppCallback")
        {
        }

        void onSuccess(string response)
        {
            showToast("opensdk RestorePurchasesCallback response = " + response);
        }

        void onError(string error)
        {
            showToast("opensdk RestorePurchasesCallback error = " + error);
        }
    }

    public class LogoutCallback : AndroidJavaProxy
    {
        public LogoutCallback() : base("com.qooapp.opensdk.common.QooAppCallback")
        {
        }

        void onSuccess(string response)
        {
            showToast("opensdk LogoutCallback response = " + response);
        }

        void onError(string error)
        {
            showToast("opensdk LogoutCallback error = " + error);
        }
    }




}