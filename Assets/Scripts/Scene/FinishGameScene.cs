using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameScene : GameSceneBaseScript
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        UIManager.Instance.finishPanel.Show();
        GameObject.Destroy(this.gameObject);
    }

}
