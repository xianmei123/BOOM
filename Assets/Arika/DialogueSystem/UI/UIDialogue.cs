using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace DialogueSystem
{
    public sealed class UIDialogue : MonoBehaviour
    {
        [Header("Actor Name")] [SerializeField]
        private TextMeshProUGUI tmpActorNameDisplay;

        [Header("Actor Image")] [SerializeField]
        private Image imgActorDisplay;


        [Header("Text")] [SerializeField] private TextMeshProUGUI tmpDialogueText;
        [Header("Button")] [SerializeField] private Button btnNext;
        [SerializeField] private Button btnQuit;

        public bool IsActive => DialogueManager.IsInDialogue;

        private DialogueManager DialogueManager => DialogueManager.Instance;

        private void Awake()
        {
            if (btnNext)
                btnNext.onClick.AddListener(() => { DialogueManager.Next(); });
            if (btnQuit)
                btnQuit.onClick.AddListener(() => DialogueManager.QuitDialogue());
        }

        private void Start()
        {
            DialogueManager.OnConversationStarted += ConversationStarted;
            DialogueManager.OnConversationUpdated += ConversationUpdated;
            DialogueManager.OnConversationEnded += ConversationEnded;

            RefreshPanel();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DialogueManager.Next();
            }
        }


        private void ConversationStarted()
        {
            gameObject.SetActive(true);
        }

        private void ConversationUpdated()
        {
            RefreshPanel();
        }

        private void ConversationEnded()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            RefreshPanel();
        }

        private void OnDestroy()
        {
            if (DialogueManager)
                DialogueManager.OnConversationUpdated -= RefreshPanel;
            if (btnNext) btnNext.onClick.RemoveAllListeners();
            if (btnQuit) btnQuit.onClick.RemoveAllListeners();
        }

        private void RefreshPanel()
        {
            Debug.Log("RefreshPanel");
            if (!DialogueManager)
                return;
            transform.gameObject.SetActive(DialogueManager.IsInDialogue);
            if (!DialogueManager.IsInDialogue) return;
            RefreshDialogue();
            RefreshCharactersDisplay();
        }


        private void RefreshDialogue()
        {
            tmpDialogueText.text = DialogueManager.CurrentNodeText;
        }

        [Header("CharactersDisplay")] [SerializeField]
        private Color actorSpeakingColor = Color.white;

        [SerializeField] private Color actorNotSpeakingColor = Color.gray;


        private void RefreshCharactersDisplay()
        {
            var currentActor = DialogueManager.CurrentActor;
            if (!currentActor)
                return;
            tmpActorNameDisplay.text = currentActor.ActorName;
            imgActorDisplay.sprite = currentActor.CurrentSprite;
            imgActorDisplay.gameObject.SetActive(currentActor.CurrentSprite);
        }
    }
}