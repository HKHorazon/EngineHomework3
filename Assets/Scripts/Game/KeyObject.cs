using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    // 供顯示用的物件（可選）
    public GameObject displayObject = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(this.GetComponent<AudioSource>() != null)
            {
                this.GetComponent<AudioSource>().Play();
            }

            this.GetComponent<Collider2D>().enabled = false; // 禁用碰撞器以避免重複觸發
            displayObject.SetActive(false); // 隱藏物件

            // 玩家獲得鑰匙後，銷毀鑰匙物件
            GameManager.Instance.SetKeyStatus(true);

            //延遲銷毀物件以確保音效能播放完畢
            Destroy(gameObject,3f);
        }
    }

}
