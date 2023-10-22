using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EscPanel : BasePanel
{
    public Toggle collections;
    public Toggle instructions;
    public Toggle system;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI textInfo;

    public Slider musicSlider;
    public Slider soundSlider;
    public Slider surroundingsSlider;

    public GameObject bagPanel;
    public GameObject instructionPanel;
    public GameObject systemPanel;
    public GameObject content;


    public List<int> lists = new List<int>();
    protected override void Init()
    {
        bagPanel.SetActive(false);
        instructionPanel.SetActive(false);
        systemPanel.SetActive(false);

        ShowInstructionPanel();
        collections.onValueChanged.AddListener((b) =>
        {
            if(b)
            {
                ShowBagPanel();
            }
            else
            {
                ClearChilds(content.transform);
                bagPanel.SetActive(false);
            }
        });
        instructions.onValueChanged.AddListener((b) =>
        {
            if (b)
            {
                ShowInstructionPanel();
            }
            else
                instructionPanel.SetActive(false);
        });
        system.onValueChanged.AddListener((b) =>
        {
            if (b)
            {
                ShowSystemPanel();
            }
            else
                systemPanel.SetActive(false);
        });

        lists.Add(1);
        lists.Add(2);
        Debug.Log(lists.Count);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIMgr.Instance.ShowPanel<GamePanel>();
            UIMgr.Instance.HidePanel<EscPanel>();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ShowBagPanel()
    {
        bagPanel.SetActive(true);
        
        for (int i = 0; i < lists.Count; i++)
        {

            T_ItemInfo info = BinaryDataMgr.Instance.GetTable<T_ItemInfoContainer>().dataDic[lists[i]];
            
            GameObject itemGrid = Instantiate(Resources.Load<GameObject>("UI/ItemGrid"));
            ItemGrid tg = itemGrid.GetComponent<ItemGrid>();
            tg.InitItemGrid(info);
            itemGrid.transform.SetParent(content.transform, false);
            itemGrid.GetComponent<Image>().sprite = Resources.Load<Sprite>(info.f_item_img_res);
            Debug.Log(info.f_item_img_res);
        }
        T_ItemInfo info1 = BinaryDataMgr.Instance.GetTable<T_ItemInfoContainer>().dataDic[lists[0]];
        if (info1 != null)
        {
            nameText.text = info1.f_item_name;
            textInfo.text = info1.f_item_info;
        }
    }
    /// <summary>
    /// 清除父物体下面的所有子物体
    /// </summary>
    /// <param name="parent"></param>
    private void ClearChilds(Transform parent)
    {
        if (parent.childCount > 0)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
    }

    public void ShowInstructionPanel()
    {
        instructionPanel.SetActive(true);
    }

    public void ShowSystemPanel()
    {
        systemPanel.SetActive(true);
    }
    
}
