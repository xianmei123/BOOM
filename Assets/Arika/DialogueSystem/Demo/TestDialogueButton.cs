#define ENABLE_LOGS

using DialogueSystem;
using UnityEngine;
using UnityEngine.UI;

public class TestDialogueButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Dialogue dialogue;


    private void Awake()
    {
        if (!button)
        {
            button = GetComponent<Button>();
        }
        
        if(!button)
            return;
        
        button.onClick.AddListener(StartDialogue);
    }

    private void StartDialogue()
    {
        if (!dialogue)
        {
            this.LogWarning("No dialogue set!");
            return;
        }
        
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}