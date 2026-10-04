using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerHW3 : MonoBehaviour
{
    private static GameManagerHW3 mInstance = null;
    public static GameManagerHW3 Instance
    {
        get
        {
            return mInstance;
        }
    }

    [field:SerializeField] public bool hasKey { get; private set; } = false;

    private void Awake()
    {
        GameManagerHW3.mInstance = this;
    }

    public void SetKeyStatus(bool hasKey)
    {
        this.hasKey = hasKey;
    }

    public void LoadEndGameLevel()
    {
        UIManager.Instance.loadingPanel.LoadScene(UIManager.FINISH_SCENE);
    }
}
