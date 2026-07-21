using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    int dialogueID = 0;
    [SerializeField] string[] dialogueEntry;
    
    public string GetCurrentDialogue() { return dialogueEntry[dialogueID]; }
    public int GetDialogueID() { return dialogueID; }
    public int GetDialogueLength() { return dialogueEntry.Length; }
    public void NextDialogue() { dialogueID++; }
    public void ResetDialogue() { dialogueID = 0; }

    void Awake() { ResetDialogue(); }
}
