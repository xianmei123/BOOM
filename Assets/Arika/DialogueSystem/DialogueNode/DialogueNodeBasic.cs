using System;
using UnityEditor;
using UnityEngine;

namespace DialogueSystem
{
    public sealed class DialogueNodeBasic : DialogueNode
    {
        [Tooltip("此角色的DialogueActor")] [SerializeField]
        private DialogueActor actor;

        public DialogueActor Actor => actor;
        public string ActorName => actor ? actor.ActorName : "Actor Not Set";


        [Tooltip("对话内容")] [TextArea] [SerializeField]
        private string text;

        public string Text => text;


        [Tooltip("进入节点时执行的动作")] [SerializeField]
        private NodeAction[] onEnterActions = Array.Empty<NodeAction>();

        public NodeAction[] OnEnterActions => onEnterActions;

        [Tooltip("离开节点时执行的动作")] [SerializeField]
        private NodeAction[] onExitActions = Array.Empty<NodeAction>();

        public NodeAction[] OnExitActions => onExitActions;

        public void SetText(string newText)
        {
            text = newText;
        }

#if UNITY_EDITOR
        public void SetTextEditor(string newText)
        {
            if (newText == text) return;
            Undo.RecordObject(this, "Update Dialogue Text");
            text = newText;
            EditorUtility.SetDirty(this);
        }

        public void SetActor(DialogueActor newActor)
        {
            Undo.RecordObject(this, "Change Dialogue actor");
            actor = newActor;
            EditorUtility.SetDirty(this);
        }
#endif
    }

    [Serializable]
    public class NodeAction
    {
        public string actionName;
        public string[] actionArgs;
    }
}