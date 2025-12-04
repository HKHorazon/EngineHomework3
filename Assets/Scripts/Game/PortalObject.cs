using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // 玩家帶著鑰匙進入傳送門後，進入下一關
            if(GameManager.Instance != null && GameManager.Instance.hasKey)
            {
                if(this.GetComponent<AudioSource>() != null)
                {
                    this.GetComponent<AudioSource>().Play();
                }
                // 假設 GameManager 有方法可以檢查是否有鑰匙
                // 這裡簡單示範進入下一關的邏輯
                Debug.Log("Player has the key! Proceeding to the next level...");
                // 在這裡可以加入進入下一關的程式碼，例如載入新場景
                GameManager.Instance.SetKeyStatus(false); // 重置鑰匙狀態
                GameManager.Instance.LoadEndGameLevel(); 
            }
        }
    }
}
