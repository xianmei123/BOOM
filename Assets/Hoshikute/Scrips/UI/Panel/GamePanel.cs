using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Image skill1;
    public Image skill2;
    public Image skill3;
    public Image skill4;
    
    protected override void Init()
    {
        skill1.gameObject.SetActive(false);
        skill2.gameObject.SetActive(false);
        skill3.gameObject.SetActive(false);
        skill4.gameObject.SetActive(false);
    }
/// <summary>
/// 显示获得的技能
/// </summary>
/// <param name="skillName"></param>
    public void ShowSkill(string skillName)
    {
        switch (skillName)
        {
            case "SprintSkill":
                skill1.gameObject.SetActive(true);
                break;
            case "FlySprintSkill":
                skill2.gameObject.SetActive(true);
                break;
            case "ClimbSkill":
                skill3.gameObject.SetActive(true);
                break;
            case "AttackSkill":
                skill4.gameObject.SetActive(true);
                break;
        }
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UIMgr.Instance.HidePanel<GamePanel>();
            UIMgr.Instance.ShowPanel<EscPanel>();
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
