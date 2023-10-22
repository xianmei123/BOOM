using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public class DialogueEditor : EditorWindow
    {
        private static Dialogue _selectedDialogue;
        [SerializeField] private GUIStyleSO styleCustomNodeBasic;

        [SerializeField] private GUIStyleSO styleCustomNodeStart;
        [SerializeField] private GUIStyleSO styleCustomNodePlayer;
        [SerializeField] private GUIStyleSO styleCustomNodeNonPlayer;
        [SerializeField] private GUIStyleSO styleCustomNodeChild;
        [SerializeField] private GUIStyleSO styleCustomNodeNonChild;


        private GUIStyle _nodeStyleStart;
        private GUIStyle _nodeStylePlayer;
        private GUIStyle _nodeStyleNonPlayer;
        private GUIStyle _nodeStyleChild;
        private GUIStyle _nodeStyleNonChild;

        private DialogueNode _draggingNode;
        private Vector2 _draggingOffset;
        private DialogueNode _creatingNode;
        private DialogueNode _deletingNode;
        private DialogueNode _linkingParentNode;
        private Vector2 _scrollPosition;
        private bool _draggingCanvas;
        private Vector2 _draggingCanvasOffset;

        private const float CanvasSize = 4000;
        private const float BackgroundSize = 50;

        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }

        /// <summary>
        /// When you double click any type of asset in Project Window, this method will be called.
        /// We first check if the asset is a Dialogue, if it is, we set it as the selected dialogue.
        /// And then we open the Dialogue Editor Window.
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            if (EditorUtility.InstanceIDToObject(instanceID) is not Dialogue dialogue) return false;
            _selectedDialogue = dialogue;
            ShowEditorWindow();
            return true;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;

            ApplyCustomNodeStylesFromSO();
            ValidCustomNodeStyle();

            void ValidCustomNodeStyle()
            {
                if (styleCustomNodeBasic)
                {
                    ApplyCustomNodeStylesFromBasicStyle(styleCustomNodeBasic.style);
                    return;
                }

                Debug.LogWarning("styleCustomNodeBasic is null, try to load from Resources");
                styleCustomNodeBasic = Resources.Load<GUIStyleSO>($"GUIStyle Node Basic");
                
                if (styleCustomNodeBasic)
                {
                    ApplyCustomNodeStylesFromBasicStyle(styleCustomNodeBasic.style);
                    return;
                }

                Debug.LogError("styleCustomNodeBasic is still null after loading from resources");

                var defaultStyle = new GUIStyle
                {
                    padding = new RectOffset(20, 20, 20, 20),
                    border = new RectOffset(12, 12, 12, 12)
                };
                ApplyCustomNodeStylesFromBasicStyle(defaultStyle);
            }

            void ApplyCustomNodeStylesFromBasicStyle(GUIStyle style)
            {
                _nodeStyleNonPlayer = new GUIStyle(style)
                {
                    normal =
                    {
                        background = EditorGUIUtility.Load("node0") as Texture2D
                    }
                };
                _nodeStylePlayer = new GUIStyle(style)
                {
                    normal =
                    {
                        background = EditorGUIUtility.Load("node1") as Texture2D
                    }
                };
                _nodeStyleStart = new GUIStyle(style)
                {
                    normal =
                    {
                        background = EditorGUIUtility.Load("node2") as Texture2D
                    }
                };

                _nodeStyleChild = new GUIStyle(style)
                {
                    normal =
                    {
                        background = EditorGUIUtility.Load("node6") as Texture2D
                    }
                };

                _nodeStyleNonChild = new GUIStyle(style)
                {
                    normal =
                    {
                        background = EditorGUIUtility.Load("node3") as Texture2D
                    }
                };
            }

            void ApplyCustomNodeStylesFromSO()
            {
                if (!styleCustomNodeStart || !styleCustomNodePlayer || !styleCustomNodeNonPlayer)
                {
                    Debug.LogWarning(
                        "styleCustomNodeStart or styleCustomNodePlayer or styleCustomNodeNonPlayer is null, applying default style");
                    ValidCustomNodeStyle();
                    return;
                }

                _nodeStyleStart = styleCustomNodeStart.style;
                _nodeStyleStart.normal.background = EditorGUIUtility.Load("node2") as Texture2D;
                _nodeStylePlayer = styleCustomNodePlayer.style;
                _nodeStylePlayer.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
                _nodeStyleNonPlayer = styleCustomNodeNonPlayer.style;
                _nodeStyleNonPlayer.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            }
        }

        /// <summary>
        /// This method is called when you select asset in the Project Window.
        /// </summary>
        private void OnSelectionChanged()
        {
            if (Selection.activeObject is not Dialogue dialogue) return;
            _selectedDialogue = dialogue;
            Repaint();
        }

        /// <summary>
        /// This method is called when Unity decides to repaint the window.
        /// </summary>
        private void OnGUI()
        {
            if (!_selectedDialogue)
            {
                EditorGUILayout.LabelField("No Dialogue Selected.");
                return;
            }

            ProcessMouseEvents();

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            Rect canvas = GUILayoutUtility.GetRect(CanvasSize, CanvasSize);
            Texture2D backgroundTex = Resources.Load<Texture2D>("DialogueSystemEditorBackground");
            Rect texCoords = new Rect(0, 0, CanvasSize / BackgroundSize, CanvasSize / BackgroundSize);
            GUI.DrawTextureWithTexCoords(canvas, backgroundTex, texCoords);

            foreach (var node in _selectedDialogue.Nodes)
            {
                DrawConnections(node);
            }

            foreach (var node in _selectedDialogue.Nodes)
            {
                DrawNode(node);
            }

            EditorGUILayout.EndScrollView();

            if (_creatingNode)
            {
                _selectedDialogue.CreateNode(_creatingNode);
                _creatingNode = null;
            }

            if (_deletingNode)
            {
                _selectedDialogue.DeleteNode(_deletingNode);
                _deletingNode = null;
            }
        }

        private void ProcessMouseEvents()
        {
            switch (Event.current.type)
            {
                case EventType.MouseDown when _draggingNode is null:
                {
                    if (TryGetNodeAtPoint(Event.current.mousePosition + _scrollPosition, out var foundNode))
                    {
                        _draggingNode = foundNode;
                        _draggingOffset = _draggingNode.Rect.position - Event.current.mousePosition;
                        Selection.activeObject = _draggingNode;
                        break;
                    }

                    _draggingCanvas = true;
                    _draggingCanvasOffset = Event.current.mousePosition + _scrollPosition;
                    Selection.activeObject = _selectedDialogue;

                    break;
                }
                case EventType.MouseDrag when _draggingNode is not null:
                    _draggingNode.SetPosition(Event.current.mousePosition + _draggingOffset);
                    GUI.changed = true;
                    break;
                case EventType.MouseDrag when _draggingCanvas:
                    _scrollPosition = _draggingCanvasOffset - Event.current.mousePosition;
                    GUI.changed = true;
                    break;
                case EventType.MouseUp when _draggingNode is not null:
                    _draggingNode = null;
                    break;
                case EventType.MouseUp when _draggingCanvas:
                    _draggingCanvas = false;
                    break;
            }

            static bool TryGetNodeAtPoint(Vector2 point, out DialogueNode foundNode)
            {
                foundNode = null;
                foreach (var node in _selectedDialogue.Nodes.Reverse())
                {
                    if (!node.Rect.Contains(point)) continue;
                    foundNode = node;
                    return true;
                }

                return false;
            }
        }

        private void DrawNode(DialogueNode node)
        {
            GUIStyle style;

            switch (node)
            {
                case DialogueNodeStart startNode:
                    style = _nodeStyleStart;
                    DrawPreNodeGUI();
                    DrawStartNode(startNode);
                    DrawPostNodeGUI();
                    break;

                case DialogueNodeBasic basicNode:
                    style =  _nodeStyleNonPlayer;
                    if (_linkingParentNode is not null && _linkingParentNode != basicNode)
                        style = _linkingParentNode.Children.Contains(node.name) ? _nodeStyleChild : _nodeStyleNonChild;

                    DrawPreNodeGUI();
                    DrawBasicNode(basicNode);
                    DrawPostNodeGUI();
                    break;
            }

            void DrawPreNodeGUI()
            {
                GUILayout.BeginArea(node.Rect, style);
            }

            void DrawPostNodeGUI()
            {
                GUILayout.EndArea();
            }

            void DrawStartNode(DialogueNodeStart startNode)
            {
                EditorGUILayout.LabelField("Start");

                GUILayout.BeginHorizontal();

                DrawLinkButtons(startNode);

                if (GUILayout.Button("+")) _creatingNode = startNode;

                GUILayout.EndHorizontal();
            }

            void DrawBasicNode(DialogueNodeBasic basicNode)
            {
                EditorGUILayout.LabelField(basicNode.ActorName);
                
                // EditorGUILayout.LabelField(basicNode.Text);
                basicNode.SetTextEditor(EditorGUILayout.TextField(basicNode.Text));
                
                GUILayout.BeginHorizontal();

                if (GUILayout.Button("x")) _deletingNode = basicNode;

                DrawLinkButtons(basicNode);

                if (GUILayout.Button("+")) _creatingNode = basicNode;

                GUILayout.EndHorizontal();
            }
        }

        private void DrawLinkButtons(DialogueNode node)
        {
            if (_linkingParentNode is null)
            {
                if (GUILayout.Button("link")) _linkingParentNode = node;
            }
            else if (_linkingParentNode == node)
            {
                if (GUILayout.Button("cancel"))
                {
                    _linkingParentNode = null;
                }
            }
            else if (_linkingParentNode.Children.Contains(node.name))
            {
                if (GUILayout.Button("unlink"))
                {
                    _linkingParentNode.RemoveChild(node.name);
                    _linkingParentNode = null;
                }
            }
            else
            {
                if (GUILayout.Button("child"))
                {
                    Undo.RecordObject(_selectedDialogue, "Add Dialogue Link");
                    _linkingParentNode.AddChild(node.name);
                    _linkingParentNode = null;
                }
            }
        }

        private void DrawConnections(DialogueNode node)
        {
            Vector3 startPosition = new Vector2(node.Rect.xMax, node.Rect.center.y);
            foreach (DialogueNode childNode in _selectedDialogue.GetAllChildren(node))
            {
                Vector3 endPosition = new Vector2(childNode.Rect.xMin, childNode.Rect.center.y);
                Vector3 controlPointOffset = endPosition - startPosition;
                controlPointOffset.y = 0;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier(
                    startPosition, endPosition,
                    startPosition + controlPointOffset,
                    endPosition - controlPointOffset,
                    Color.white, null, 4f);
            }
        }
    }
}