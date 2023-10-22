using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DialogueStateListener : MonoBehaviour
{
    [SerializeField] private DialogueEventChannel channel;
    [SerializeField] private DialogueStateListenerEntry[] entries;

    private void Awake()
    {
        channel.OnDialogueStateChanged += OnDialogueStateChanged;
    }

    private void OnDestroy()
    {
        channel.OnDialogueStateChanged -= OnDialogueStateChanged;
    }

    private void OnDialogueStateChanged(DialogueState state)
    {
        entries.FirstOrDefault(e => e.state == state)?.unityEvent.Invoke();
    }
}

[Serializable]
public class DialogueStateListenerEntry
{
    public DialogueState state;
    public UnityEvent unityEvent;
}