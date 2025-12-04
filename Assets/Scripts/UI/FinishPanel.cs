using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishPanel : UIPanelBase
{
    public Button finishButton = null;


    public override void Show()
    {
        base.Show();
        //按鈕延遲出現
        StartCoroutine(ShowFinishButtonWithDelay());
    }

    //按鈕延遲出現的協程
    private IEnumerator ShowFinishButtonWithDelay()
    {
        //使用IEnumerator來實現延遲效果
        //先隱藏按鈕
        finishButton.gameObject.SetActive(false);
        //等待一段時間
        yield return new WaitForSeconds(3.0f);
        //顯示按鈕
        finishButton.gameObject.SetActive(true);
    }

    //重新開始遊戲按鈕事件
    public void OnClick_RestartGame()
    {
        this.Hide();
        UIManager.Instance.loadingPanel.LoadScene(UIManager.START_SCENE);
    }
}
