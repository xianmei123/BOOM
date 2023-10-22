using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool canDialogue = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (canDialogue)
        {
            canDialogue = false;
            Debug.Log(canDialogue);
        }
       
        
    }
}
