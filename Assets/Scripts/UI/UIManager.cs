using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //單例模式 (Singleton Pattern)
    private static UIManager mInstance = null;
    public static UIManager Instance
    {
        get
        {
            //如果mInstance是null，先嘗試尋找場景中的UIManager物件
            if (mInstance == null)
            {
                mInstance = GameObject.FindObjectOfType<UIManager>();
            }
            //如果場景中找不到，再從Resources資料夾載入UIManager Prefab
            if (mInstance == null)
            {
                GameObject prefab = Resources.Load<GameObject>("UIManager");
                if (prefab != null)
                {
                    GameObject obj = GameObject.Instantiate(prefab);
                    mInstance = obj.GetComponent<UIManager>();
                }
                else
                {
                    Debug.LogError("UIManager Prefab is NULL!");
                }
            }
            return mInstance;
        }
    }

    //場景名稱常數
    public const string START_SCENE = "開始遊戲";
    public const string BATTLE_SCENE = "戰鬥";
    public const string FINISH_SCENE = "結束遊戲";

    //所有UI的物件，其實較好的做法是用Dictionary來管理
    //但這種寫法較直觀，適合新手閱讀
    public MenuPanel menuPanel;
    public LoadingPanel loadingPanel;
    public FinishPanel finishPanel;

    public void Init()
    {
        //確保UIManager物件在場景切換時不被銷毀
        DontDestroyOnLoad(this.gameObject);

        //尋找EventSystem物件
        GameObject eventSystem = GameObject.Find("EventSystem");
        if(eventSystem == null)
        {
            //直接建立空物件，並掛載EventSystem和StandaloneInputModule元件
            eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
            eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        }

        //隱藏所有面板
        menuPanel.Hide();
        loadingPanel.Hide();
        finishPanel.Hide();
    }

}
