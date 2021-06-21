using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

[Serializable]
public class Receipt
{
    public string index;
    public string uid;
    public string transaction_id;
    public string product_id;
    public string platform;
    public double price;
    public string date;
}

public class IAPManager : MonoBehaviour, IStoreListener
{
    // 영수증

    //계속 구매되는 (소모품)
    [NonSerialized]
    public string[] _GoogleProductID =
        {
         "jason",
         "shield",
         "dolphin",
         "dragon",
         "ghost",
         "longnose",
         "magneto"
          };

    //개발자센터의 해당상품에 설정한 식별자ID
    private const string _IOS_GoldId = "1000coin";

    //한번 구매하면 (소장품)
    [NonSerialized]
    public string[] _GoogleProductCharacterSkin = { "item02" };

    //개발자센터의 해당상품에 설정한 식별자ID
    private const string _IOS_SkinId = "com.studio.app.skin";

    //구독서비스 (매달 무엇을 내는?)
    private const string ProductSubscription = "premium_subscription";

    //개발자센터의 해당상품에 설정한 식별자ID
    private const string _IOS_PremiumSub = "com.studio.app.sub";

    private const string _ANDROID_PremiumSub = "com.studio.app.sub";

    public static IAPManager instance;

    //구매 과정을 제어하는 함수 제공
    private IStoreController storeController;

    //여러 플랫폼을 위한 확장처리를 제공
    private IExtensionProvider storeExtensionProvider;

    //get만 존재하는 프로퍼티는 람다로 표현
    public bool IsInitialized => storeController != null && storeExtensionProvider != null;

    //  public Text DebugText = null;

    public bool isPurchaseUnderProcess = true;




    private void Awake()
    {
        //if (m_instance != null && m_instance != this)
        //{
        //    Destroy(this);
        //}
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
    }

    private void OnEnable()
    {
        InitUnityIAP();
    }

    private void InitUnityIAP()
    {
        //ConfigurationBuilder
        //인앱결제와 관련된 설정을 빌드할수있는 빌더를 생성하는 클래스
        // StandardPurchasingModule
        //유니티가 제공하는 스토어의 설정
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        //상품 등록

        //상품의 ID , 상품의 타입 , 앱스토어에 사용되는ID

        #region 계속 구매되는 소모품

        for (int i = 0; i < _GoogleProductID.Length; i++)
        {
            builder.AddProduct
            (
                _GoogleProductID[i], ProductType.NonConsumable,
                new IDs()
                {
                     //앱스토의 이름
                     {_IOS_GoldId,AppleAppStore.Name },
                     {_GoogleProductID[i],GooglePlay.Name },
                }
            );
        }

        #region 세팅레거시

        {
            // builder.AddProduct
            // (
            //     ProductID[0], ProductType.Consumable,
            //     new IDs()
            //     {
            //          //앱스토의 이름
            //          {_IOS_GoldId,AppleAppStore.Name },
            //          {ProductID[0],GooglePlay.Name },
            //     }
            // );

            // builder.AddProduct
            //(
            //    ProductID[1], ProductType.Consumable,
            //    new IDs()
            //    {
            //          //앱스토의 이름
            //          {_IOS_GoldId,AppleAppStore.Name },
            //          {ProductID[1],GooglePlay.Name },
            //    }
            //);
            // builder.AddProduct
            //(
            //    ProductID[2], ProductType.Consumable,
            //    new IDs()
            //    {
            //          //앱스토의 이름
            //          {_IOS_GoldId,AppleAppStore.Name },
            //          {ProductID[2],GooglePlay.Name },
            //    }
            //);
            // builder.AddProduct
            //(
            //    ProductID[3], ProductType.Consumable,
            //    new IDs()
            //    {
            //          //앱스토의 이름
            //          {_IOS_GoldId,AppleAppStore.Name },
            //          {ProductID[3],GooglePlay.Name },
            //    }
            //);
        }

        #endregion 세팅레거시

        #endregion 계속 구매되는 소모품

        #region 한번만 구매되는 소장서비스

        for (int i = 0; i < _GoogleProductCharacterSkin.Length; i++)
        {
            builder.AddProduct
            (
                _GoogleProductCharacterSkin[i], ProductType.NonConsumable,
                new IDs()
                {
                     //앱스토의 이름
                     {_IOS_SkinId,AppleAppStore.Name },
                     {_GoogleProductCharacterSkin[i],GooglePlay.Name },
                }
            );
        }

        #endregion 한번만 구매되는 소장서비스

        #region 매달 구매해야되는 구독서비스

        builder.AddProduct
        (
           ProductSubscription, ProductType.Subscription,
           new IDs()
           {
                 //앱스토의 이름
                 {_IOS_PremiumSub,AppleAppStore.Name },
                 {_ANDROID_PremiumSub,GooglePlay.Name },
           }
        );

        #endregion 매달 구매해야되는 구독서비스

        //초기화
        //활성화가 끝났을때 실행,빌더설정
        UnityPurchasing.Initialize(this, builder);
    }

    /// <summary>
    /// 실행후 OnIntialized 자동으로 실행됨.
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="extensions"></param>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        //DebugText.text = "Unity IAP 초기화 성공";
        Debug.Log("Unity IAP 초기화 성공");

        storeController = controller;
        storeExtensionProvider = extensions;
    }

    public void PurchaseComplete(Product product)
    {
        Debug.Log(product.availableToPurchase + " , " + product.receipt);

        //테스트 되는건지 모르겟다...
        if (product.definition.id.Equals(_GoogleProductID[0]))
        {
            Debug.Log("No.0 구입여부TRUE");
        }
        if (!product.definition.id.Equals(_GoogleProductID[0]))
        {
            Debug.Log("No.0 구입여부FALSE");
        }
        //////////////////////////////////////////////////
        if (product.definition.id.Equals(_GoogleProductID[1]))
        {
            Debug.Log("No.1 구입여부TRUE");
        }
        if (!product.definition.id.Equals(_GoogleProductID[1]))
        {
            Debug.Log("No.1 구입여부FALSE");
        }
        //////////////////////////////////////////////////
        if (product.definition.id.Equals(_GoogleProductID[2]))
        {
            Debug.Log("No.2 구입여부TRUE");
        }
        if (!product.definition.id.Equals(_GoogleProductID[2]))
        {
            Debug.Log("No.2 구입여부FALSE");
        }
        //////////////////////////////////////////////////
        if (product.definition.id.Equals(_GoogleProductID[3]))
        {
            Debug.Log("No.3 구입여부TRUE");
        }
        if (!product.definition.id.Equals(_GoogleProductID[3]))
        {
            Debug.Log("No.3 구입여부FALSE");
        }
        //////////////////////////////////////////////////
        if (product.definition.id.Equals(_GoogleProductID[4]))
        {
            Debug.Log("No.4 구입여부TRUE");
        }
        if (!product.definition.id.Equals(_GoogleProductID[4]))
        {
            Debug.Log("No.4 구입여부FALSE");
        }
        //////////////////////////////////////////////////
        if (product.definition.id.Equals(_GoogleProductID[5]))
        {
            Debug.Log("No.5 구입여부TRUE");
        }
        if (!product.definition.id.Equals(_GoogleProductID[5]))
        {
            Debug.Log("No.5 구입여부FALSE");
        }
    }

    /// <summary>
    /// 초기화 실패
    /// </summary>
    /// <param name="error"></param>
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"유니티 IAP 초기화 실패{error}");
    }

    private Receipt receipt = new Receipt();

    private bool purchase = false;

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        // DebugText.text = $"구매 성공 - ID : {args.purchasedProduct.definition.id}";
        Debug.Log($"구매 성공 - ID : {e.purchasedProduct.definition.storeSpecificId}");

        var validPurchase = true;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX

        var validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);

        try
        {
            // On Google Play, result has a single product ID.
            // On Apple stores, receipts contain multiple products.
            var receipt = validator.Validate(e.purchasedProduct.receipt);

            Debug.Log(receipt);
            //구입정보 시간 영수증 구매ID
            foreach (var productReceipt in receipt)
            {
                var google = productReceipt as GooglePlayReceipt;
                var product = storeController.products.WithID(google.productID).hasReceipt;
                Debug.Log("영수증보유 = " + product);

                if (google.purchaseState == GooglePurchaseState.Purchased && product == true)
                {
                    Debug.Log("구매함.");
                }
                if (google.purchaseState == GooglePurchaseState.Refunded && product == false)
                {
                    Debug.Log("환불함.");
                }

                if (google == null) continue;

                Debug.Log(google.productID);
                Debug.Log(google.purchaseState);
                Debug.Log(google.purchaseToken);

                product_Receipt(google, google.productID);
            }
        }
        catch (IAPSecurityException)
        {
            Debug.Log("Invalid receipt not unlocking content");
            validPurchase = false;
        }
#endif

        //비소비품
        string text = "";
        if (validPurchase)
        {
            // GooglePlayGPGS.instance.playnanooStart();
            var usercharacter = UserDataManager.instance.Player_Eqip.user_Characters;
            var characterdatas = UserDataManager.instance.character_Ref.userDatas;
            if (e.purchasedProduct.definition.id.Equals(_GoogleProductID[0]))
            {
                //Shield
                text = _GoogleProductID[0];
                Data_ispurchase(UserDataManager.instance.Player_Eqip.user_Characters[9],
                                                UserDataManager.instance.character_Ref.userDatas[9], true);
            }
            if (e.purchasedProduct.definition.id.Equals(_GoogleProductID[1]))
            {
                //dolphin
                text = _GoogleProductID[1];
                Data_ispurchase(UserDataManager.instance.Player_Eqip.user_Characters[10],
                                                UserDataManager.instance.character_Ref.userDatas[10], true);
            }
            if (e.purchasedProduct.definition.id.Equals(_GoogleProductID[2]))
            {
                //ghost
                text = _GoogleProductID[2];
                Data_ispurchase(UserDataManager.instance.Player_Eqip.user_Characters[11],
                                                UserDataManager.instance.character_Ref.userDatas[11], true);
            }
            if (e.purchasedProduct.definition.id.Equals(_GoogleProductID[3]))
            {
                //longnose
                text = _GoogleProductID[3];
                Data_ispurchase(UserDataManager.instance.Player_Eqip.user_Characters[12],
                                                UserDataManager.instance.character_Ref.userDatas[12], true);
            }
            if (e.purchasedProduct.definition.id.Equals(_GoogleProductID[4]))
            {
                //magneto
                text = _GoogleProductID[4];
                Data_ispurchase(UserDataManager.instance.Player_Eqip.user_Characters[13],
                                                 UserDataManager.instance.character_Ref.userDatas[13], true);
            }
            if (e.purchasedProduct.definition.id.Equals(_GoogleProductID[5]))
            {
                //jason
                text = _GoogleProductID[5];
                Data_ispurchase(UserDataManager.instance.Player_Eqip.user_Characters[14],
                                                UserDataManager.instance.character_Ref.userDatas[14], true);
            }
            if (e.purchasedProduct.definition.id.Equals(_GoogleProductID[6]))
            {
                //dragon
                text = _GoogleProductID[6];
                Data_ispurchase(UserDataManager.instance.Player_Eqip.user_Characters[15],
                                                UserDataManager.instance.character_Ref.userDatas[15], true);
            }

            UserDataManager.instance.character_Ref.Click("RARE");

            MessagePopManager.instance.ShowPop(text);
            UserDataManager.instance.Achiement_Secces(8);
        }

        UserDataManager.instance.Achiement_Secces(4);

        MessagePopManager.instance.ShowPop("구매성공!!!");
        //구입완료 이펙트 펑~
        StartCoroutine(UserDataManager.instance.character_Ref.purchase_Effect.purchase_effect_play());

        return PurchaseProcessingResult.Complete;
    }

    private void product_Receipt(GooglePlayReceipt google, string id)
    {
        {
            switch (google.purchaseState)
            {
                case GooglePurchaseState.Purchased:
                    // 광고 제거 코드
                    Debug.Log(google.productID + " 구입중 사용가능");
                    break;

                case GooglePurchaseState.Cancelled:
                case GooglePurchaseState.Refunded:
                    // 광고 보여주는 코드
                    Debug.Log(google.productID + " 환불 사용불가");
                    // Data_ispurchase(usercharacter[9], characterdatas[9], false);
                    break;
            }
        }
    }

    private void Data_ispurchase(charcter_infomation info, UserData user, bool b)
    {
        info.is_purchase = b;
        user.isPurchase = b;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        //상품의 아이디 , 실패정보
        Debug.LogWarning($"구매 실패 - {product.definition.id} , {reason}");
        MessagePopManager.instance.ShowPop("케릭터 구매에 실패했습니다.");
    }

    //구매 시도

    public void Purchase(string productId)
    {
        if (!IsInitialized) return;
        //해당 ID에 대응되는 상품오브젝트를 반환
        var product = storeController.products.WithID(productId);

        if (product != null && product.availableToPurchase) //구매가능
        {
            //var Debug = $"구매시도 - {product.definition.id}";
            //DebugText.text = Debug;

            Debug.Log($"구매시도 - {product.definition.id}");
            storeController.InitiatePurchase(product);
        }
        else
        {
            //var Debug = $"구매시도 불가 - {product.definition.id}";
            //DebugText.text = Debug;
            Debug.Log($"구매시도 불가 - {product.definition.id}");
        }
    }

    //구매복구 IOS만가능
    public void RestorePurchase()
    {
        if (!IsInitialized) return;
        //플랫폼
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("구매 복구 시도");

            var appleExt = storeExtensionProvider.GetExtension<IAppleExtensions>();
            //기존의 구매내역을 복구
            appleExt.RestoreTransactions(
                result => Debug.Log($"구매 복구 시도 결과{result}"));
        }
    }

    //소모품 외에 아이템들만 구매내역을 확인해야한다.. IOS만가능
    public bool HadPurchase(string productId)
    {
        if (!IsInitialized) return false;
        //해당 ID에 대응되는 상품오브젝트를 반환
        var product = storeController.products.WithID(productId);

        if (product != null)
        {
            return product.hasReceipt;  //구매한 정보 (영수증)
        }
        return false;
    }
}