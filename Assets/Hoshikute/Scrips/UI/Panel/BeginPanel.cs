using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnBegin;
    public Button btnQuit;
    protected override void Init()
    {
        btnBegin.onClick.AddListener(()=>{
            Debug.Log("Begin");
            UIMgr.Instance.HidePanel<BeginPanel>();
            StartCoroutine(LoadingScene());
        });
        btnQuit.onClick.AddListener(()=>{
            Application.Quit();
        });
    }
    /// <summary>
    /// �첽���س�����ʾ���
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadingScene()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("LevelTest1");
        ao.completed += (a) =>
        {
            UIMgr.Instance.ShowPanel<GamePanel>();
        };
        yield return ao;
    }
}
