using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingPanel : UIPanelBase
{
    const float MIN_LOAD_TIME = 1f; // 最少載入時間
    const float FADE_IN_TIME = 0.3f;   // 淡入時間  
    const float FADE_OUT_TIME = 0.3f;  // 淡出時間



    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI progressText;

    public void LoadScene(string sceneName)
    {
        this.gameObject.SetActive(true);
        var canvasGrp = this.GetComponent<CanvasGroup>();
        if (canvasGrp != null) canvasGrp.alpha = 0f; // 初始化透明度

        StartCoroutine(LoadSceneAsync(sceneName));

    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        this.Show();

        // 淡入效果
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            float fadeInDuration = FADE_IN_TIME;
            for (float t = 0; t < fadeInDuration; t += Time.deltaTime)
            {
                canvasGroup.alpha = t / fadeInDuration;
                yield return null;
            }
            canvasGroup.alpha = 1f;
        }

        // 開始載入場景(非同步)
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float minimumLoadTime = MIN_LOAD_TIME; // 至少讀取 1 秒
        float elapsedTime = 0f;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            progressText.text = $"{Mathf.RoundToInt(progress * 100)}%";

            elapsedTime += Time.deltaTime;

            if (operation.progress >= 0.9f && elapsedTime >= minimumLoadTime)
            {
                progressText.text = "100%";
                yield return new WaitForSeconds(0.2f); // 可自訂等待時間
                operation.allowSceneActivation = true;
            }
            yield return null;
        }

        // 淡出效果
        if (canvasGroup != null)
        {
            float fadeOutDuration = FADE_OUT_TIME;
            for (float t = 0; t < fadeOutDuration; t += Time.deltaTime)
            {
                canvasGroup.alpha = 1f - (t / fadeOutDuration);
                yield return null;
            }
            canvasGroup.alpha = 0f;
        }

        this.Hide();
    }
}
