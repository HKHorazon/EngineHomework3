// 作業操作紀錄：每 10 次操作記一筆「時間 電腦ID 本次開啟累計次數」，
// 存檔時寫進該場景的隱藏物件，學生匯出場景時跟著一起交回來。老師用 _tools/courselog_read.py 讀。
// 專案裡有 ProjectSettings/CourseLog.teacher 就不記（老師的學生版母體）。
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
public class CourseLog : MonoBehaviour
{
    public List<string> entries = new List<string>();
}

#if UNITY_EDITOR
namespace CourseLogEditor
{
    using System;
    using System.IO;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine.SceneManagement;

    [InitializeOnLoad]
    static class Recorder
    {
        const int K = 10;
        const string Pending = "Library/CourseLog.pending";  // 還沒存進場景的紀錄，關掉 Unity 也不會丟

        static Recorder()
        {
            if (File.Exists("ProjectSettings/CourseLog.teacher")) return;
            Selection.selectionChanged += Bump;
            EditorApplication.playModeStateChanged += _ => Bump();
            ObjectChangeEvents.changesPublished += (ref ObjectChangeEventStream s) => { for (int i = 0; i < s.length; i++) Bump(); };
            EditorSceneManager.sceneSaving += Flush;
        }

        static void Bump()
        {
            int n = SessionState.GetInt("courselog.n", 0) + 1;  // SessionState 撐過重編譯／進 Play，Unity 重開才歸零
            SessionState.SetInt("courselog.n", n);
            if (n % K != 0) return;
            try { File.AppendAllText(Pending, $"{DateTime.Now:yyyyMMdd'T'HHmmss} {SystemInfo.deviceUniqueIdentifier} {n}\n"); }
            catch { /* 絕不干擾學生操作 */ }
        }

        static void Flush(Scene scene, string path)
        {
            try
            {
                if (!File.Exists(Pending)) return;
                CourseLog log = null;
                foreach (var go in scene.GetRootGameObjects())
                    if ((log = go.GetComponent<CourseLog>()) != null) break;
                if (log == null)
                {
                    var go = new GameObject("CourseLog") { hideFlags = HideFlags.HideInHierarchy };
                    SceneManager.MoveGameObjectToScene(go, scene);
                    log = go.AddComponent<CourseLog>();
                    log.hideFlags = HideFlags.HideInInspector;
                }
                log.entries.AddRange(File.ReadAllLines(Pending));
                File.Delete(Pending);
            }
            catch { }
        }
    }
}
#endif
