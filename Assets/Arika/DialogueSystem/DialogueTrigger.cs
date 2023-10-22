using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DialogueSystem
{
    public sealed class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private List<DialogueSingleTrigger> triggers = new List<DialogueSingleTrigger>();
        public IList<DialogueSingleTrigger> Triggers => triggers;

        public void TriggerAction(NodeAction action)
        {
            Debug.Log($"Try Triggering {action.actionName} in {gameObject.name}");
            foreach (var trigger in triggers)
            {
                // Debug.Log($"action {action.actionName} trigger {trigger.actionName} ");
                if (trigger.actionName != action.actionName) continue;
                Debug.Log($"Found {trigger.actionName}, Triggering");
                trigger.triggerEvent?.Invoke(action.actionArgs);
                return;
            }
        }
    }

    [Serializable]
    public sealed class DialogueSingleTrigger
    {
        public string actionName;
        public UnityEvent<string[]> triggerEvent;
    }
}