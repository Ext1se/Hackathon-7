using PixelCrushers.DialogueSystem;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
public const string TUTORIAL_STATE = "TutorialState";
        
        public DialogueSystemTrigger DialogueSystemTrigger;
        public DialogueSystemController DialogueSystemController;

        private StandardDialogueUI m_StandardDialogueUI;
        private float m_TryCountOpenDialog = 0f;
        
        private void InitComponents()
        {
            if (DialogueSystemController == null)
            {
                DialogueSystemController = FindObjectOfType<DialogueSystemController>();
            }

            if (DialogueSystemTrigger == null)
            {
                DialogueSystemTrigger = FindObjectOfType<DialogueSystemTrigger>();
            }

            m_StandardDialogueUI = DialogueSystemController.displaySettings.dialogueUI.GetComponent<StandardDialogueUI>();
        }

        private void OnValidate()
        {
            InitComponents();
        }

        private void Awake()
        {
            InitComponents();
        }

        private void Start()
        {
        }

        #region Dialog

        public void ShowDialog()
        {
            m_TryCountOpenDialog++;
            if (m_StandardDialogueUI.conversationUIElements.mainPanel.isOpen)
            {
                DialogueSystemController.StopConversation();
                m_StandardDialogueUI.Close();
                if (m_TryCountOpenDialog < 3)
                {
                    Invoke(nameof(ShowDialog), 1f);
                }
            }
            else
            {
                m_TryCountOpenDialog = 0;
                DialogueSystemTrigger.OnUse();
            }
        }

        public void CloseDialog()
        {
            if (m_StandardDialogueUI.conversationUIElements.mainPanel.isOpen)
            {
                DialogueSystemController.StopConversation();
                m_StandardDialogueUI.Close();
            }
        }

        public void SwitchDialog()
        {
            if (m_StandardDialogueUI.conversationUIElements.mainPanel.isOpen)
            {
                DialogueSystemController.StopConversation();
                m_StandardDialogueUI.Close();
            }
            else
            {
                DialogueSystemTrigger.OnUse();
            }
        }
        
        public void SetDialogVariable(string variableName, int variableValue)
        {
            DialogueLua.SetVariable(variableName, variableValue);
        }

        protected void OnEnable()
        {
            RegisterLua();
        }

        protected void OnDisable()
        {
            UnregisterLua();

            DialogueSystemController.StopConversation();
            m_StandardDialogueUI.Close();
        }

        public void RegisterLua()
        {
        }

        public void UnregisterLua()
        {
        }

        #endregion
}
