using UnityEngine;

namespace DialogueSystem.Editor
{
    [CreateAssetMenu(fileName = "GUIStyleSO", menuName = "ScriptableObject/GUIStyleSO", order = 0)]
    public class GUIStyleSO : ScriptableObject
    {
        public GUIStyle style;
    }
}