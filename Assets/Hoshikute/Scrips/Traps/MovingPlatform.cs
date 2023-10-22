using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_MoveType
{
    Horizontal,
    Vertical,
}

public class MovingPlatform : MonoBehaviour
{
    public E_MoveType e_MoveType = E_MoveType.Horizontal;
    public float moveSpeed = 5;

    private Transform left;
    private Transform right;
    private Transform top;
    private Transform bottom;
    private Vector3 vleft;
    private Vector3 vright;
    private Vector3 vtop;
    private Vector3 vbottom;
    private int dir = 1;
    private Vector2 distance;

    private void Awake()
    {
        left = this.transform.Find("LeftPoint").transform;
        right = this.transform.Find("RightPoint").transform;
        top = this.transform.Find("TopPoint").transform;
        bottom = this.transform.Find("BottomPoint").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        vleft = left.position;
        vright = right.position;
        vtop = top.position;
        vbottom = bottom.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(e_MoveType == E_MoveType.Horizontal)
        {
            this.transform.Translate(Vector2.right * dir * moveSpeed * 0.01f);
            
            if (this.transform.position.x > vright.x || this.transform.position.x < vleft.x)
                dir *= -1;
            distance = Vector2.right * dir * moveSpeed * 0.01f;
        }
        else
        {
            this.transform.Translate(Vector2.up * dir * moveSpeed * 0.01f);
           
            if (this.transform.position.y > vtop.y || this.transform.position.y < vbottom.y)
                dir *= -1;
            distance = Vector2.up * dir * moveSpeed * 0.01f;
        }
    }
    


    public Vector2 GetOffset()
    {
        return distance;
    }
}
