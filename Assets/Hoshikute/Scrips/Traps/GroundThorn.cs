using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundThorn : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("GroundThorn");
    }
}
