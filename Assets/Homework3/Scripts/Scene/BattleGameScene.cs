using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGameScene : GameSceneBaseScript
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GameObject.Destroy(this.gameObject);
    }

}
