using DialogueSystem;
using UnityEngine;

public class PlayerDialogueTrigger : MonoBehaviour
{
    [Header("Dialogue")] [SerializeField] private Dialogue dialogue;


    private bool IsPlayerInRange { get; set; }
    private bool IsTriggered { get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ProcessColliderEnter(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ProcessColliderEnter(other.gameObject);
    }

    private void ProcessColliderEnter(GameObject otherGameObject)
    {
        if (otherGameObject.CompareTag("Player"))
        {
            IsPlayerInRange = true;
        }
    }

    // On Exit
    private void OnTriggerExit2D(Collider2D other)
    {
        ProcessColliderExit(other.gameObject);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        ProcessColliderExit(other.gameObject);
    }

    private void ProcessColliderExit(GameObject otherGameObject)
    {
        if (otherGameObject.CompareTag("Player"))
        {
            IsPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (IsTriggered)
            return;

        if (!IsPlayerInRange)
            return;

        if (Input.GetKey(KeyCode.W))
        {
            IsTriggered = true;
            DialogueManager.Instance.StartDialogue(dialogue);
        }
    }
}