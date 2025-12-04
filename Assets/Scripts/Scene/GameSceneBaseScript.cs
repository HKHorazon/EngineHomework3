using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneBaseScript : MonoBehaviour
{
    protected virtual void Start()
    {
        //任何通用的場景初始化程式碼
        UIManager.Instance.Init();
    }
}
