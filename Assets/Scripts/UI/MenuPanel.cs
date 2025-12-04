using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : UIPanelBase
{
    //開始遊戲按鈕事件
    public void OnClick_StartGame()
    {
        this.Hide();
        UIManager.Instance.loadingPanel.LoadScene(UIManager.BATTLE_SCENE);
    }

    //遊戲設定按鈕事件
    public void OnClick_Options()
    {
        //TODO: 打開遊戲設定面板
        Debug.Log("按下遊戲設定按鈕");
    }

    //結束遊戲按鈕事件
    public void OnClick_QuitGame()
    {
        Application.Quit();
    }
}
