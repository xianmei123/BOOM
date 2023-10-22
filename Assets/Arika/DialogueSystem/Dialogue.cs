using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "ScriptableObject/DialogueSystem/Dialogue", order = 0)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<DialogueNode> nodes = new List<DialogueNode>();
        public IEnumerable<DialogueNode> Nodes => nodes;
        [SerializeField] private Vector2 newNodeOffset = new Vector2(250, 0);

        private readonly Dictionary<string, DialogueNode> _nodeDict = new Dictionary<string, DialogueNode>();

        private void OnEnable()
        {
            ValidateNodeDict();
        }

        private void OnValidate()
        {
            ValidateNodeDict();
        }
        
        private void ValidateNodeDict()
        {
            _nodeDict.Clear();
            foreach (var node in Nodes)
            {
                if (!node)
                {
                    Debug.LogError($"node is null in {name}, node count: {nodes.Count}");
                    continue;
                }

                if (!_nodeDict.TryAdd(node.name, node))
                    Debug.LogWarning($"Duplicate node name {node.name} in {name}");
            }
        }

        public bool TryGetNode(string nodeName, out DialogueNode node)
        {
            return _nodeDict.TryGetValue(nodeName, out node);
        }

        public DialogueNode GetRootNode() => nodes[0];

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            foreach (var childID in parentNode.Children)
            {
                if (_nodeDict.TryGetValue(childID, out var dialogueNode))
                    yield return dialogueNode;
            }
        }

#if UNITY_EDITOR
        public void CreateNode(DialogueNode parent)
        {
            DialogueNodeBasic newNodeBasic = MakeNode<DialogueNodeBasic>(parent);
            Undo.RegisterCreatedObjectUndo(newNodeBasic, "Created Dialogue Node");
            Undo.RecordObject(this, "Added Dialogue Node");
            AddNode(newNodeBasic);
        }

        public void DeleteNode(DialogueNode nodeToDelete)
        {
            Undo.RecordObject(this, "Deleted Dialogue Node");
            nodes.Remove(nodeToDelete);
            OnValidate();
            CleanDanglingChildren(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);
        }

        private T MakeNode<T>(DialogueNode parent) where T : DialogueNode
        {
            var newNode = CreateInstance<T>();
            newNode.name = newNode is DialogueNodeStart ? "Start" : Guid.NewGuid().ToString();
            if (parent is null) return newNode;

            parent.AddChild(newNode.name);
            newNode.SetPosition(parent.Rect.position + newNodeOffset);

            if (newNode is not DialogueNodeBasic basicNode)
                return newNode;
            if (parent is not DialogueNodeBasic parentBasicNode)
                return newNode;

            basicNode.SetActor(parentBasicNode.Actor);
            return newNode;
        }

        private void AddNode(DialogueNode newNode)
        {
            nodes.Add(newNode);
            OnValidate();
        }

        private void CleanDanglingChildren(DialogueNode nodeToDelete)
        {
            foreach (var node in Nodes) node.RemoveChild(nodeToDelete.name);
        }
#endif

        #region Unity Serialization

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (nodes.Count is 0)
            {
                var startNode = MakeNode<DialogueNodeStart>(null);
                AddNode(startNode);
            }

            if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(this))) return;

            foreach (var node in Nodes)
            {
                if (AssetDatabase.GetAssetPath(node) is "") AssetDatabase.AddObjectToAsset(node, this);
            }
#endif
        }

        public void OnAfterDeserialize()
        {
        }

        #endregion
    }
}