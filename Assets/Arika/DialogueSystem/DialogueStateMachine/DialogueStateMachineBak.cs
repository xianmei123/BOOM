// using UnityEngine;
//
// public class DialogueStateMachine : StaticInstance<DialogueStateMachine>
// {
//     [SerializeField] private DialogueEventChannel channel;
//     [SerializeField] private DialogueState startupState;
//
//     private DialogueState _currentState;
//     public DialogueState CurrentState => _currentState;
//
//     protected override void Awake()
//     {
//         base.Awake();
//         channel.OnDialogueStateRequested += SetFlowState;
//     }
//
//     private void Start()
//     {
//         SetFlowState(startupState);
//     }
//
//     private void OnDestroy()
//     {
//         channel.OnDialogueStateRequested -= SetFlowState;
//     }
//
//     private void SetFlowState(DialogueState state)
//     {
//         if (_currentState == state) return;
//         _currentState = state;
//         channel.RaiseDialogueStateChanged(_currentState);
//     }
// }