using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MobileVN : MonoBehaviour
{
    PlayerInput touchSystem;
    Vector2 currentTouchPos;
    [Header("Essential Assets")]
    [SerializeField] Sprite[] chara;
    [SerializeField] Sprite[] background;
    [Header("Character Name Definitions")]
    [SerializeField] string[] characterName;
    [Header("UI References - Backgrounds and Characters")]
    [SerializeField] Image backgroundImage;
    [SerializeField] CanvasGroup characterGroup;
    [SerializeField] Image leftCharacterImage;
    [SerializeField] Image rightCharacterImage;
    [Header("UI References - Dialogue and Mission Boxes")]
    [SerializeField] CanvasGroup dialogueGroup;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] CanvasGroup nameGroup;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image missionBox;
    [SerializeField] TextMeshProUGUI missionTitleText;
    [SerializeField] TextMeshProUGUI missionText;
    [Header("Dialogues and Missions")]
    int currentMission = 0;
    int currentDialogue = 0;
    bool isDialogueOngoing;
    [SerializeField] DialogueManager[] dialogues;
    [SerializeField] MissionManager[] missions;

    public void ToggleCharacterGroup(bool active)
    {
        if (active)
        {
            characterGroup.alpha = 1.0f;
            characterGroup.blocksRaycasts = true;
            characterGroup.interactable = true;
        }
        else
        {
            characterGroup.alpha = 0.0f;
            characterGroup.blocksRaycasts = false;
            characterGroup.interactable = false;
        }
    }

    public void SetDisplayCharacter(bool isLeft, int characterID)
    {
        if (characterID >= chara.Length || characterID < 0)
        {
            Debug.LogError("Invalid character ID.");
        }
        else
        {
            if (isLeft) { leftCharacterImage.sprite = chara[characterID]; }
            else { rightCharacterImage.sprite = chara[characterID]; }
        }
    }

    public void ToggleCharacter(bool isLeft, bool active)
    {
        if (active)
        {
            if (isLeft) { leftCharacterImage.enabled = true; }
            else { rightCharacterImage.enabled = true; }
        }
        else
        {
            if (isLeft) { leftCharacterImage.enabled = false; }
            else { rightCharacterImage.enabled = false; }
        }
    }

    public void ToggleDialogue(bool active)
    {
        if (active)
        {
            dialogueGroup.alpha = 1.0f;
            dialogueGroup.blocksRaycasts = true;
            dialogueGroup.interactable = true;
        }
        else
        {
            dialogueGroup.alpha = 0.0f;
            dialogueGroup.blocksRaycasts = false;
            dialogueGroup.interactable = false;
        }
    }

    public void SetDialogueText(string dialogue){ dialogueText.text = dialogue; }

    public void ToggleNameLabel(bool active)
    {
        if (active)
        {
            nameGroup.alpha = 1.0f;
            nameGroup.blocksRaycasts = true;
            nameGroup.interactable = true;
        }
        else
        {
            nameGroup.alpha = 0.0f;
            nameGroup.blocksRaycasts = false;
            nameGroup.interactable = false;
        }
    }

    public void SetCustomNameLabel(string customName) { nameText.text = customName; }

    public void SetCharacterNameLabel(int characterID)
    {
        if (characterID >= chara.Length || characterID < 0)
        {
            Debug.LogError("Invalid character ID.");
        }
        else
        {
            nameText.text = characterName[characterID];
        }
    }

    public void LoadDialogue(int missionID, int dialogueIndex)
    {
        if(missionID == 0)
        {
            missionTitleText.text = missions[currentMission].GetMissionTitle();
            isDialogueOngoing = true;
            Debug.LogWarning("Dialogue Started.");
            dialogues[dialogueIndex].ResetDialogue();
            SetDialogueText(dialogues[dialogueIndex].GetCurrentDialogue());
            ToggleDialogue(true); ToggleCharacterGroup(true);
            if(dialogueIndex == 0)
            {
                SetDisplayCharacter(true, 0); ToggleCharacter(true, true);
                SetCharacterNameLabel(0); ToggleNameLabel(true);
            }
        }
    }

    public void UpdateDialogue()
    {
        if (isDialogueOngoing)
        {
            dialogues[currentDialogue].NextDialogue();
            if (dialogues[currentDialogue].GetDialogueID() >= dialogues[currentDialogue].GetDialogueLength())
            {
                ToggleCharacter(true, false); ToggleCharacterGroup(false);
                ToggleNameLabel(false); ToggleDialogue(false);
                isDialogueOngoing = false;
                Debug.LogWarning("Dialogue Finished.");
                if (currentDialogue == 0)
                {
                    missionText.text = missions[currentMission].GetCurrentObjective();
                }
            }
            else
            {
                SetDialogueText(dialogues[currentDialogue].GetCurrentDialogue());
            }
        }
        else
        {
            Debug.Log("Dialogue not ongoing");
        }
    }

    void Awake()
    {
        touchSystem = GetComponent<PlayerInput>();
    }

    void Start()
    {
        LoadDialogue(currentMission, currentDialogue);
    }

    public void OnPlayerTap(InputValue value)
    {
        Debug.Log($"Touched in {currentTouchPos}");
        UpdateDialogue();
    }

    public void OnGetTappedPosition(InputValue value)
    {
        currentTouchPos = value.Get<Vector2>();
    }
}
