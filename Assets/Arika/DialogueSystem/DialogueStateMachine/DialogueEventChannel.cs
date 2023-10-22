using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueEventChannel",
    menuName = "ScriptableObject/DialogueSystem/DialogueEventChannel", order = 1)]
public class DialogueEventChannel : ScriptableObject
{
    public Action<DialogueState> OnDialogueStateRequested;
    public Action<DialogueState> OnDialogueStateChanged;
    public void RaiseDialogueStateRequest(DialogueState state) => OnDialogueStateRequested?.Invoke(state);
    public void RaiseDialogueStateChanged(DialogueState state) => OnDialogueStateChanged?.Invoke(state);
}