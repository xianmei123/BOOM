using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    protected abstract void Init();

    public virtual void ShowMe()
    {

    }

    public virtual void HideMe()
    {

    }
}
