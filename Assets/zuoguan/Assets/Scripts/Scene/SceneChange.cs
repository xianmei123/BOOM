using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] public String LevelName;
    [SerializeField] public int index;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("下一关");
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(LevelName);
            SkillData.Instance.index = index;
        }
        
        
        
        
    }
}
