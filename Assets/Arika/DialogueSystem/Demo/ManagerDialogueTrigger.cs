using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DialogueSystem.Demo
{
    public sealed class ManagerDialogueTrigger : MonoBehaviour
    {
        private DialogueManager DialogueManager => DialogueManager.Instance;

        private DialogueTrigger DialogueTrigger { get; set; }

        private void Awake()
        {
            DialogueTrigger = GetComponent<DialogueTrigger>();

            // ChangeActorSprite
            var changeSpriteEvent = new UnityEvent<string[]>();
            changeSpriteEvent.AddListener(ChangeActorSprite);
            DialogueTrigger.Triggers.Add(
                new DialogueSingleTrigger
                {
                    actionName = nameof(ChangeActorSprite),
                    triggerEvent = changeSpriteEvent
                });
            Debug.Log($"Add {nameof(ChangeActorSprite)} to {gameObject.name}");

            // ChangeBackgroundSprite
            var changeBackgroundSpriteEvent = new UnityEvent<string[]>();
            changeBackgroundSpriteEvent.AddListener(ChangeBackgroundSprite);
            DialogueTrigger.Triggers.Add(
                new DialogueSingleTrigger
                {
                    actionName = nameof(ChangeBackgroundSprite),
                    triggerEvent = changeBackgroundSpriteEvent
                });
        }

        public void ChangeActorSprite(string[] args)
        {
            if (args.Length < 1) return;
            var spriteIndexString = args[0];
            Debug.Log($"ChangeActorSprite {spriteIndexString}");
            if (int.TryParse(spriteIndexString, out var spriteIndex))
            {
                DialogueManager.CurrentActor.CurrentSpriteIndex = spriteIndex;
            }
        }

        [SerializeField] private Image imgTest;

        [SerializeField] private Sprite[] testSprites;


        public void ChangeBackgroundSprite(string[] args)
        {
            if (args.Length < 1) return;
            var spriteIndexString = args[0];
            Debug.Log($"ChangeBackgroundSprite {spriteIndexString}");
            if (int.TryParse(spriteIndexString, out var spriteIndex))
            {
                imgTest.sprite = testSprites[spriteIndex];
            }
        }
    }
}