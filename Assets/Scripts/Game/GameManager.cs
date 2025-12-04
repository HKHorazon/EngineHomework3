using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager mInstance = null;
    public static GameManager Instance
    {
        get
        {
            return mInstance;
        }
    }

    [field:SerializeField] public bool hasKey { get; private set; } = false;

    private void Awake()
    {
        GameManager.mInstance = this;
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
