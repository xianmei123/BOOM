using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_FirePorts_Mode
{
    Horizontal,
    Vertical,
}

public class BulletTrap : MonoBehaviour
{
    [Header("子弹发射口横向火纵向排列")]
    public E_FirePorts_Mode eMode = E_FirePorts_Mode.Horizontal;
    [Header("子弹发射口数量")]
    public int firePortsNum = 5;
    [Header("子弹发射间隔")]
    public float fireCd = 1f;
    [Header("子弹速度向量（x,y)")]
    public Vector2 bulletSpeed = new Vector2(0,-5);
    [Header("子弹重力加速度")]
    public float a = 1;
    
    private Vector3 down;
    private Vector3 left;
    private Vector3 v3;
    private float width;
    private float height;
    private float depth;

    private void Awake()
    {
        width = this.transform.localScale.x;
        height = this.transform.localScale.y;

        left = this.transform.position;
        left.x = this.transform.position.x - (width / 2f);
        down = this.transform.position;
        down.y = this.transform.position.y - (height / 2f);
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreatBullet",fireCd);
    }

    public void CreatBullet()
    {
        switch (firePortsNum % 2)
        {
            //偶数开火点
            case 0:
                EvenPortsNum();
                break;
            //奇数开火点
            case 1:
                OddPortsNum();
                break;
        }
        Invoke("CreatBullet",fireCd);
    }

    /// <summary>
    /// 偶数子弹出生点分布
    /// </summary>
    public void EvenPortsNum()
    {
        for(int i = 1; i < firePortsNum + 1; i++)
        {
            if (eMode == E_FirePorts_Mode.Horizontal)
            {
                depth = width / (firePortsNum + 1);
                v3 = left;
                v3.x = left.x + depth * i;
            }
            else
            {
                depth = height / (firePortsNum + 1);
                v3 = down;
                v3.y = down.y + depth * i;
            }

            GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Traps/Bullet"));
            obj.transform.position = v3;
            Bullet b = obj.GetComponent<Bullet>();
            b.bulletSpeed = bulletSpeed;
            b.a = a;
        }
    }

    /// <summary>
    /// 奇数子弹出生点分布
    /// </summary>
    public void OddPortsNum()
    {
        for(int i = 1; i < firePortsNum + 1; i++)
        {
            if (eMode == E_FirePorts_Mode.Horizontal)
            {
                depth = width / (firePortsNum + 1);
                v3 = left;
                v3.x = left.x + depth * i;
            }
            else
            {
                depth = height / (firePortsNum + 1);
                v3 = down;
                v3.y = down.y + depth * i;
            }

            GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Traps/Bullet"));
            obj.transform.position = v3;
            Bullet b = obj.GetComponent<Bullet>();
            b.bulletSpeed = bulletSpeed;
            b.a = a;
        }
    }
}
