using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemGrid : MonoBehaviour
{
    public T_ItemInfo itemInfo;
    // Start is called before the first frame update
    void Start()
    {
        EventTrigger trigger = this.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry enter = new EventTrigger.Entry();
        enter.eventID = EventTriggerType.PointerEnter;
        enter.callback.AddListener(EnterItemGrid);
        trigger.triggers.Add(enter);
    }

    private void EnterItemGrid(BaseEventData data)
    {
        Debug.Log(UIMgr.Instance.GetPanel<EscPanel>());
        Debug.Log(itemInfo.f_item_name);
        UIMgr.Instance.GetPanel<EscPanel>().nameText.text = itemInfo.f_item_name;
        UIMgr.Instance.GetPanel<EscPanel>().textInfo.text = itemInfo.f_item_info;
    }

    public void InitItemGrid(T_ItemInfo Info)
    {
        itemInfo = Info;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
