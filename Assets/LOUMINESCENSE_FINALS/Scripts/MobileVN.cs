using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MobileVN : MonoBehaviour
{
    PlayerInput touchSystem;
    Vector2 currentTouchPos;
    AudioSource soundPlayer;
    [SerializeField] AudioClip[] sfx;
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
    [SerializeField] CanvasGroup missionGroup;
    [SerializeField] Button missionListButton;
    [SerializeField] Image missionBox;
    [SerializeField] TextMeshProUGUI missionTitleText;
    [SerializeField] TextMeshProUGUI missionText;
    [Header("UI References - Others")]
    [SerializeField] CanvasGroup missionClearScreen;
    [SerializeField] TextMeshProUGUI clearMessage;
    [SerializeField] TextMeshProUGUI chapterCleared;
    [SerializeField] TextMeshProUGUI allClear;
    [SerializeField] Button continueButton;
    [SerializeField] Button restartButton;
    [SerializeField] CanvasGroup[] ActionMenu;
    [Header("Dialogues and Missions")]
    int currentMission = 0;
    int currentDialogue = 0;
    int currentObjective = 0;
    bool isDialogueOngoing;
    bool isObjectiveListShown = false;
    [SerializeField] MissionManager[] missions;

    void ToggleCharacterGroup(bool active)
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

    void SetDisplayCharacter(bool isLeft, int characterID)
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

    void ToggleCharacter(bool isLeft, bool active)
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

    void ToggleDialogue(bool active)
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

    void SetDialogueText(string dialogue){ dialogueText.text = dialogue; }

    void ToggleNameLabel(bool active)
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

    void SetCustomNameLabel(string customName) { nameText.text = customName; }

    void SetCharacterNameLabel(int characterID)
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

    //Loads the dialogue from current mission (missionID), and the dialogue index provided from the array of dialogue managers from the mission manager (dialogueIndex).
    void LoadDialogue(int missionID, int dialogueIndex)
    {
        isDialogueOngoing = true;
        Debug.LogWarning("Dialogue Started.");
        missions[missionID].dialogues[dialogueIndex].ResetDialogue();
        SetDialogueText(missions[missionID].dialogues[dialogueIndex].GetCurrentDialogue());
        ToggleDialogue(true); ToggleCharacterGroup(true);
        if (missionID == 0)
        {
            if      (missionID == 0 && missions[missionID].GetObjectiveID() == 0)
            {
                if (dialogueIndex == 0 || dialogueIndex == 1 || dialogueIndex == 3 || dialogueIndex == 4)
                {
                    SetDisplayCharacter(true, 0); ToggleCharacter(true, true);
                    SetCharacterNameLabel(0); ToggleNameLabel(true);
                }
            }
            else if (missionID == 0 && missions[missionID].GetObjectiveID() == 1)
            {
                if(dialogueIndex == 8)
                {
                    SetDisplayCharacter(true, 0); ToggleCharacter(true, true);
                    SetDisplayCharacter(false, 1); ToggleCharacter(false, true);
                    SetCharacterNameLabel(0); ToggleNameLabel(true);
                }
            }
            else if (missionID == 0 && missions[missionID].GetObjectiveID() == 2)
            {
                //Nothing happens.
            }
        }
        else if (missionID == 1)
        {
            if      (missionID == 1 && missions[missionID].GetObjectiveID() == 0)
            {
                if (dialogueIndex == 2 || dialogueIndex == 3)
                {
                    SetDisplayCharacter(true, 0); ToggleCharacter(true, true);
                    SetCharacterNameLabel(0); ToggleNameLabel(true);
                }
            }
            else if (missionID == 1 && missions[missionID].GetObjectiveID() == 1)
            {
                if (dialogueIndex == 6)
                {
                    SetDisplayCharacter(true, 0); ToggleCharacter(true, true);
                    SetDisplayCharacter(false, 4); ToggleCharacter(false, true);
                    SetCharacterNameLabel(0); ToggleNameLabel(true);
                }
                else if(dialogueIndex == 5)
                {
                    SetDisplayCharacter(true, 0); ToggleCharacter(true, true);
                    SetCharacterNameLabel(0); ToggleNameLabel(true);
                }
            }
            else if (missionID == 1 && missions[missionID].GetObjectiveID() == 2)
            {
                if (dialogueIndex == 7 || dialogueIndex == 8 || dialogueIndex == 9 || dialogueIndex == 10)
                {
                    SetDisplayCharacter(false, 2); ToggleCharacter(false, true);
                    if(dialogueIndex == 7)
                    {
                        ToggleNameLabel(false);
                    }
                    else
                    {
                        SetCharacterNameLabel(2); ToggleNameLabel(true);
                    }
                    
                }
            }
        }
        else if (missionID == 2)
        {
            if      (missionID == 2 && missions[missionID].GetObjectiveID() == 0)
            {
                if(dialogueIndex == 1 || dialogueIndex == 2 || dialogueIndex == 3)
                {
                    SetDisplayCharacter(true, 0); ToggleCharacter(true, true);
                    SetCharacterNameLabel(0); ToggleNameLabel(true);
                }
            }
            else if (missionID == 2 && missions[missionID].GetObjectiveID() == 1)
            {
                if (dialogueIndex == 5)
                {
                    SetDisplayCharacter(true, 0); ToggleCharacter(true, true);
                    SetCharacterNameLabel(0); ToggleNameLabel(true);
                }
            }
            else if (missionID == 2 && missions[missionID].GetObjectiveID() == 2)
            {
                if(dialogueIndex == 8 || dialogueIndex == 9)
                {
                    SetDisplayCharacter(true, 0); SetCharacterNameLabel(0); ToggleCharacter(true, true); ToggleNameLabel(true);
                }
            }
        }
    }

    //The dialogue will update if the dialogue is ongoing.
    //The if the dialogueID of the mission manager is higher than the length of the dialogue messages from the dialogue manager, the dialogue ends.
    //Otherwise, the dialogue will continue.
    public void UpdateDialogue()
    {
        if (isDialogueOngoing)
        {
            soundPlayer.PlayOneShot(sfx[1]);
            missions[currentMission].dialogues[currentDialogue].NextDialogue();
            if (missions[currentMission].dialogues[currentDialogue].GetDialogueID() >= missions[currentMission].dialogues[currentDialogue].GetDialogueLength())
            {
                //End of dialogue
                ToggleCharacter(true, false); ToggleCharacter(false, false); ToggleCharacterGroup(false);
                ToggleNameLabel(false); ToggleDialogue(false);
                isDialogueOngoing = false;
                Debug.LogWarning("Dialogue Finished.");
                if (currentMission == 0 && missions[currentMission].GetObjectiveID() == 0)
                {
                    if (currentDialogue == 4) { FinishObjective(5); }
                    else { ToggleActionMenu(true); }
                }
                else if (currentMission == 0 && missions[currentMission].GetObjectiveID() == 1)
                {
                    if (currentDialogue == 5 || currentDialogue == 6 || currentDialogue == 7) { ToggleActionMenu(true); }
                    else if (currentDialogue == 8) { FinishObjective(9); }
                }
                else if (currentMission == 0 && missions[currentMission].GetObjectiveID() == 2)
                {
                    if(currentDialogue == 10 || currentDialogue == 11) { FinishMission(); }
                    else { ToggleActionMenu(true); }
                }
                else if (currentMission == 1 && missions[currentMission].GetObjectiveID() == 0)
                {
                    if(currentDialogue == 2 || currentDialogue == 3) { FinishObjective(4); }
                    else { ToggleActionMenu(true); }
                }
                else if (currentMission == 1 && missions[currentMission].GetObjectiveID() == 1)
                {
                    if(currentDialogue == 6)
                    {
                        FinishObjective(7);
                    }
                    else
                    {
                        ToggleActionMenu(true);
                    }
                }
                else if (currentMission == 1 && missions[currentMission].GetObjectiveID() == 2)
                {
                    if(currentDialogue == 9 || currentDialogue == 10)
                    {
                        FinishMission();
                    }
                    else
                    {
                        ToggleActionMenu(true);
                    }
                }
                else if (currentMission == 2 && missions[currentMission].GetObjectiveID() == 0)
                {
                    if (currentDialogue == 3)
                    {
                        FinishObjective(4);
                    }
                    else
                    {
                        ToggleActionMenu(true);
                    }
                }
                else if (currentMission == 2 && missions[currentMission].GetObjectiveID() == 1)
                {
                    if (currentDialogue == 6)
                    {
                        FinishObjective(7);
                    }
                    else
                    {
                        ToggleActionMenu(true);
                    }
                }
                else if (currentMission == 2 && missions[currentMission].GetObjectiveID() == 2)
                {
                    if (currentDialogue == 9)
                    {
                        FinishMission();
                    }
                    else
                    {
                        ToggleActionMenu(true);
                    }
                }
            }
            else
            {
                //Dialogue advances
                SetDialogueText(missions[currentMission].dialogues[currentDialogue].GetCurrentDialogue());
                if (currentMission == 0 && missions[currentMission].GetObjectiveID() == 0)
                {
                    if(currentDialogue == 2)
                    {
                        if (missions[currentMission].dialogues[currentDialogue].GetDialogueID() == 3)
                        {
                            ToggleNameLabel(true);
                            ToggleCharacter(true, true);
                        }
                        else
                        {
                            ToggleCharacter(true, false);
                        }
                    }
                }
                else if (currentMission == 0 && missions[currentMission].GetObjectiveID() == 1)
                {
                    if(currentDialogue == 6)
                    {
                        switch (missions[currentMission].dialogues[currentDialogue].GetDialogueID())
                        {
                            case 1:
                                SetDisplayCharacter(true, 0);
                                SetDisplayCharacter(false, 1);
                                ToggleNameLabel(true);
                                ToggleCharacter(true, true);
                                ToggleCharacter(false, true);
                                break;
                            case 2: SetCharacterNameLabel(1); break;
                            case 3:
                                SetDisplayCharacter(true, 0);
                                SetDisplayCharacter(false, 1);
                                ToggleNameLabel(true);
                                ToggleCharacter(true, true);
                                ToggleCharacter(false, true);
                                break;
                            case 4: SetCharacterNameLabel(1); break;
                        }
                    }
                    else if(currentDialogue == 8)
                    {
                        if (missions[currentMission].dialogues[currentDialogue].GetDialogueID() == 1)
                        {
                            SetCharacterNameLabel(1);
                        }
                    }
                }
                else if (currentMission == 0 && missions[currentMission].GetObjectiveID() == 2)
                {
                    //Nothing happens here.
                }
                else if (currentMission == 1 && missions[currentMission].GetObjectiveID() == 0)
                {
                    if(currentDialogue == 2 || currentDialogue == 3)
                    {
                        if(missions[currentMission].dialogues[currentDialogue].GetDialogueID() == 1)
                        {
                            ToggleCharacter(true, false);
                            ToggleNameLabel(false);
                        }
                    }
                }
                else if (currentMission == 1 && missions[currentMission].GetObjectiveID() == 1)
                {
                    if (currentDialogue == 5)
                    {
                        if (missions[currentMission].dialogues[currentDialogue].GetDialogueID() == 1)
                        {
                            ToggleCharacter(true, false);
                            ToggleNameLabel(false);
                        }
                    }
                    else if (currentDialogue == 6)
                    {
                        switch (missions[currentMission].dialogues[currentDialogue].GetDialogueID())
                        {
                            case 1: SetCharacterNameLabel(4); break;
                            case 2: SetCharacterNameLabel(1); break;
                            case 3: SetCharacterNameLabel(4); break;
                            case 4: ToggleNameLabel(false); break;
                        }
                    }
                }
                else if (currentMission == 1 && missions[currentMission].GetObjectiveID() == 2)
                {
                    if(currentDialogue == 9)
                    {
                        switch (missions[currentMission].dialogues[currentDialogue].GetDialogueID())
                        {
                            case 1: ToggleNameLabel(false); break;
                            case 2: ToggleNameLabel(true); break;
                            case 3: ToggleNameLabel(false); break;
                            case 4: ToggleNameLabel(true); break;
                            case 5: ToggleNameLabel(false); break;
                        }
                    }
                    else if (currentDialogue == 10)
                    {
                        switch (missions[currentMission].dialogues[currentDialogue].GetDialogueID())
                        {
                            case 1: ToggleNameLabel(false); break;
                            case 2: ToggleNameLabel(false); break;
                            case 3: ToggleNameLabel(true); break;
                            case 4: ToggleNameLabel(false); break;
                        }
                    }
                }
                else if (currentMission == 2 && missions[currentMission].GetObjectiveID() == 0)
                {
                    if(currentDialogue == 3)
                    {
                        if (missions[currentMission].dialogues[currentDialogue].GetDialogueID() == 1)
                        {
                            ToggleNameLabel(false);
                        }
                    }
                    
                }
                else if (currentMission == 2 && missions[currentMission].GetObjectiveID() == 1)
                {
                    if(currentDialogue == 6)
                    {
                        if (missions[currentMission].dialogues[currentDialogue].GetDialogueID() == 1)
                        {
                            SetDisplayCharacter(true, 0);
                            SetCharacterNameLabel(0);
                            ToggleCharacter(true, true);
                            ToggleNameLabel(true);
                        }
                    }
                }
                else if (currentMission == 2 && missions[currentMission].GetObjectiveID() == 2)
                {
                    if(currentDialogue == 9)
                    {
                        switch (missions[currentMission].dialogues[currentDialogue].GetDialogueID())
                        {
                            case 1: ToggleCharacter(true, false); ToggleNameLabel(false); break;
                            case 2: ToggleNameLabel(false); break;
                            case 3: SetDisplayCharacter(false, 3); SetCharacterNameLabel(3); ToggleCharacter(false, true); ToggleNameLabel(true); break;
                            case 4: SetDisplayCharacter(true, 0); ToggleCharacter(true, true); SetCharacterNameLabel(0); break;
                            case 5: SetCharacterNameLabel(3); break;
                            case 6: ToggleNameLabel(false); break;
                        }
                    }
                    
                }
            }
        }
        else
        {
            soundPlayer.PlayOneShot(sfx[2]);
            Debug.Log("Dialogue not ongoing");
        }
    }

    private void FinishObjective(int nextDialogue)
    {
        currentObjective++;
        soundPlayer.PlayOneShot(sfx[3]);
        missions[currentMission].NextObjective();
        missionText.text = missions[currentMission].GetCurrentObjective();
        currentDialogue = nextDialogue;
        SetMissionAndViewDialogue(currentMission, nextDialogue);
    }

    void FinishMission()
    {
        isObjectiveListShown = false;
        missionListButton.enabled = false;
        chapterCleared.text = missions[currentMission].GetMissionTitle();
        currentObjective++;
        currentMission++;
        soundPlayer.PlayOneShot(sfx[4]);
        missionClearScreen.alpha = 1.0f;
        missionClearScreen.interactable = true;
        missionClearScreen.blocksRaycasts = true;
        if (currentMission >= missions.Length)
        {
            currentObjective = 0;
            missionTitleText.text = "THE END";
            continueButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(true);
            allClear.gameObject.SetActive(true);
            Debug.Log("Thank you for playing!");
        }
        else
        {
            missionTitleText.text = "TO BE CONTINUED";
            continueButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(false);
            allClear.gameObject.SetActive(false);
        }
    }

    //Press the book button to show or hide the objective window.
    public void ToggleObjectiveList()
    {
        if (isObjectiveListShown) { isObjectiveListShown = false; } else { isObjectiveListShown = true; }
    }

    //Starts a quest.
    public void StartMission()
    {
        missionListButton.enabled = true;
        missionClearScreen.alpha = 0.0f;
        missionClearScreen.interactable = false;
        missionClearScreen.blocksRaycasts = false;
        ToggleActionMenu(false);
        currentDialogue = 0;
        missions[currentMission].ResetObjective();
        missionText.text = missions[currentMission].GetCurrentObjective();
        missionTitleText.text = missions[currentMission].GetMissionTitle();
        LoadDialogue(currentMission, currentDialogue);
    }

    public void RestartGame()
    {
        currentObjective = 0;
        currentMission = 0;
        StartMission();
    }

    //The action menu is shown after a dialogue is finished. It is hidden while a dialogue is ongoing.
    void ToggleActionMenu(bool active)
    {
        if (active)
        {
            ActionMenu[currentObjective].alpha = 1.0f;
            ActionMenu[currentObjective].interactable = true;
            ActionMenu[currentObjective].blocksRaycasts = true;
        }
        else
        {
            ActionMenu[currentObjective].alpha = 0.0f;
            ActionMenu[currentObjective].interactable = false;
            ActionMenu[currentObjective].blocksRaycasts = false;
        }
    }

    void SetMissionAndViewDialogue(int mission, int dialogue)
    {
        currentMission = mission;
        currentDialogue = dialogue;
        LoadDialogue(currentMission, currentDialogue);
    }

    void Awake()
    {
        currentMission = 0;
        touchSystem = GetComponent<PlayerInput>();
        soundPlayer = GetComponent<AudioSource>();
        foreach (CanvasGroup AM in ActionMenu)
        {
            AM.alpha = 0.0f;
            AM.interactable = false;
            AM.blocksRaycasts = false;
        }
    }

    void Start()
    {
        StartMission();
    }

    void Update()
    {
        if (isObjectiveListShown)
        {
            missionGroup.alpha = 1.0f;
            missionGroup.interactable = true;
            missionGroup.blocksRaycasts = true;
        }
        else
        {
            missionGroup.alpha = 0.0f;
            missionGroup.interactable = false;
            missionGroup.blocksRaycasts = false;
        }
        if (currentMission < missions.Length)
        {
            if (missions[currentMission].CheckObjectiveStatus())
            {
                missionText.color = new Color(0.2f, 1.0f, 0.2f);
            }
            else
            {
                missionText.color = new Color(1.0f, 1.0f, 1.0f);
            }
        }
        else
        {
            missionListButton.enabled = false;
            missionGroup.alpha = 0.0f;
            missionGroup.interactable = false;
            missionGroup.blocksRaycasts = false;
        }

        switch (currentObjective)
        {
            case 0: backgroundImage.sprite = background[0]; break;
            case 1: backgroundImage.sprite = background[1]; break;
            case 2: backgroundImage.sprite = background[1]; break;
            case 3: backgroundImage.sprite = background[3]; break;
            case 4: backgroundImage.sprite = background[3]; break;
            case 5: backgroundImage.sprite = background[3]; break;
            case 6: backgroundImage.sprite = background[4]; break;
            case 7: backgroundImage.sprite = background[2]; break;
            case 8: backgroundImage.sprite = background[2]; break;
            default: break;
        }
    }

    public void OnPlayerTap(InputValue value)
    {
        soundPlayer.PlayOneShot(sfx[0]);
        Debug.Log($"Touched in {currentTouchPos}");
    }

    public void OnGetTappedPosition(InputValue value)
    {
        currentTouchPos = value.Get<Vector2>();
    }

    public void M1O1_Action1() { ToggleActionMenu(false); SetMissionAndViewDialogue(0, 1); }
    public void M1O1_Action2() { ToggleActionMenu(false); missions[currentMission].FinishObjective(); SetMissionAndViewDialogue(0, 2); }
    public void M1O1_Action3()
    {
        ToggleActionMenu(false);
        if (missions[currentMission].CheckObjectiveStatus())
        {
            SetMissionAndViewDialogue(0, 4);
        }
        else
        {
            SetMissionAndViewDialogue(0, 3);
        }
    }

    public void M1O2_Action1() { ToggleActionMenu(false); missions[currentMission].FinishObjective(); SetMissionAndViewDialogue(0, 6); }
    public void M1O2_Action2()
    {
        ToggleActionMenu(false);
        if (missions[currentMission].CheckObjectiveStatus())
        {
            SetMissionAndViewDialogue(0, 8);
        }
        else
        {
            SetMissionAndViewDialogue(0, 7);
        }
    }
    public void M1O3_Action1()
    {
        ToggleActionMenu(false);
        missions[currentMission].FinishObjective();
        SetMissionAndViewDialogue(0, 10);
    }

    public void M1O3_Action2()
    {
        ToggleActionMenu(false);
        missions[currentMission].FinishObjective();
        SetMissionAndViewDialogue(0, 11);
    }

    public void M2O1_Action1() { ToggleActionMenu(false); missions[currentMission].FinishObjective(); SetMissionAndViewDialogue(1, 2); }
    public void M2O1_Action2() { ToggleActionMenu(false); missions[currentMission].FinishObjective(); SetMissionAndViewDialogue(1, 3); }
    public void M2O1_Action3() { ToggleActionMenu(false); SetMissionAndViewDialogue(1, 1); }
    public void M2O2_Action1() { ToggleActionMenu(false); SetMissionAndViewDialogue(1, 6); }
    public void M2O2_Action2() { ToggleActionMenu(false); missions[currentMission].FinishObjective(); SetMissionAndViewDialogue(1, 5); }
    public void M2O3_Action1() { ToggleActionMenu(false); missions[currentMission].FinishObjective(); SetMissionAndViewDialogue(1, 9); }
    public void M2O3_Action2() { ToggleActionMenu(false); missions[currentMission].FinishObjective(); SetMissionAndViewDialogue(1, 10); }
    public void M2O3_Action3() { ToggleActionMenu(false); SetMissionAndViewDialogue(1, 8); }
    public void M3O1_Action1() { ToggleActionMenu(false); SetMissionAndViewDialogue(2, 1); }
    public void M3O1_Action2() { ToggleActionMenu(false); SetMissionAndViewDialogue(2, 2); }
    public void M3O1_Action3() { ToggleActionMenu(false); missions[currentMission].FinishObjective(); SetMissionAndViewDialogue(2, 3); }
    public void M3O2_Action1() { ToggleActionMenu(false); SetMissionAndViewDialogue(2, 5); }
    public void M3O2_Action2() { ToggleActionMenu(false); missions[currentMission].FinishObjective(); SetMissionAndViewDialogue(2, 6); }
    public void M3O3_Action1() { ToggleActionMenu(false); SetMissionAndViewDialogue(2, 8); }
    public void M3O3_Action2() { ToggleActionMenu(false); SetMissionAndViewDialogue(2, 9); }
}
