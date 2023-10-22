using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueActor", menuName = "ScriptableObject/DialogueSystem/DialogueActor", order = 1)]
public sealed class DialogueActor : ScriptableObject
{
    [SerializeField] private string actorName;
    public string ActorName => actorName;

    [SerializeField] private Sprite[] actorSprites = new Sprite[1];
    public Sprite[] ActorSprites => actorSprites;

    public int CurrentSpriteIndex { get; set; }

    public Sprite CurrentSprite
    {
        get
        {
            if (CurrentSpriteIndex < 0 || CurrentSpriteIndex >= actorSprites.Length)
                return null;
            return actorSprites[CurrentSpriteIndex];
        }
    }

    private void OnEnable()
    {
        CurrentSpriteIndex = 0;
    }
}