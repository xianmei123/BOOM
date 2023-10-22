using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

class UIMgr
{
    private static UIMgr instance = new UIMgr();
    public static UIMgr Instance => instance;


    private Dictionary<string,BasePanel> panelDic = new Dictionary<string, BasePanel>();
    private Transform canvasTrans;
    public string lastSelPanelName;

    private UIMgr()
    {
        GameObject canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        canvasTrans = canvas.transform;
        GameObject.DontDestroyOnLoad(canvas);

    }

    public T ShowPanel<T>() where T:BasePanel
    {
        if(!panelDic.ContainsKey(typeof(T).Name))
        {
            GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>("UI/"+typeof(T).Name));
            obj.transform.SetParent(canvasTrans,false);
            T panel = obj.GetComponent<T>();
            panelDic.Add(typeof(T).Name,panel);
        }
        return panelDic[typeof(T).Name] as T;
    }

    /// <summary>
    /// 显示上一个hide的面板
    /// </summary>
    public void ShowLastPanel()
    {
        GameObject obj = GameObject.Instantiate((Resources.Load<GameObject>("UI/" + lastSelPanelName)));
        obj.transform.SetParent(canvasTrans,false);
        panelDic.Add(lastSelPanelName,obj.GetComponent<BasePanel>());
    }

    public void HidePanel<T>() where T:BasePanel
    {
        lastSelPanelName = typeof(T).Name;
        if(panelDic.ContainsKey(typeof(T).Name))
        {
            GameObject.Destroy(panelDic[typeof(T).Name].gameObject);
            panelDic.Remove(typeof(T).Name);
        }
    }

    public T GetPanel<T>() where T:BasePanel
    {
        if(panelDic.ContainsKey(typeof(T).Name))
            return panelDic[typeof(T).Name] as T;
        return null;
    }

    

}