using System;
using UnityEngine;

/// <summary>
/// There should be only 1 instance of this Scriptable Object in the project.
/// If a script needs to access the Quest Manager, it should serialize a reference to this Scriptable Object.
/// </summary>
public class DialogueStateMachine : Singleton<DialogueStateMachine>
{
    [SerializeField] private DialogueEventChannel channel;

    [Tooltip("The state the game should start with (Set it to InGame by default)")] [SerializeField]
    private DialogueState startupState;

    public DialogueState CurrentState { get; private set; }

    private void OnEnable()
    {
        if (!channel)
        {
            Debug.LogError("DialogueStateMachine: No DialogueEventChannel assigned!");
            return;
        }
        channel.OnDialogueStateRequested -= SetDialogueState;
        channel.OnDialogueStateRequested += SetDialogueState;
        SetDialogueState(startupState);
    }

    private void OnDestroy()
    {
        channel.OnDialogueStateRequested -= SetDialogueState;
    }

    public void SetDialogueState(DialogueState state)
    {
        if (CurrentState == state) return;
        CurrentState = state;
        channel.RaiseDialogueStateChanged(CurrentState);
    }
}