using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//開始場景 起始程式碼
public class StartGameScene : GameSceneBaseScript
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        UIManager.Instance.menuPanel.Show();
        GameObject.Destroy(this.gameObject);
    }
}
