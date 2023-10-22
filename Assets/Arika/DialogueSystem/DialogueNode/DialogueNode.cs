using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DialogueSystem
{
    public abstract class DialogueNode : ScriptableObject
    {
        [Tooltip("Rect in Dialogue Editor")] [SerializeField]
        protected Rect rect = new Rect(0, 0, 200, 100);

        public Rect Rect => rect;

        [SerializeField] protected List<string> children = new List<string>();
        public List<string> Children => children;

        [SerializeField] private Condition.Condition condition;
        public Condition.Condition Condition => condition;

        /// <summary>记得在使用EDITOR功能的时候加上判断否则会影响打包</summary>///
        public virtual void AddChild(string childID)
        {
#if UNITY_EDITOR
            Undo.RecordObject(this, "Add Dialogue Link");
            children.Add(childID);
            EditorUtility.SetDirty(this);
#endif
        }

        public virtual void RemoveChild(string childID)
        {
#if UNITY_EDITOR
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
#endif
        }

        public void SetPosition(Vector2 newPosition)
        {
#if UNITY_EDITOR
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = newPosition;
            EditorUtility.SetDirty(this);
#endif
        }
    }
}