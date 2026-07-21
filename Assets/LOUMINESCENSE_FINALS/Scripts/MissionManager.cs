using UnityEngine;

public class MissionManager : MonoBehaviour
{
    int objectiveID = 0;
    [SerializeField] string missionTitle;
    [SerializeField] string[] objectives;
    public DialogueManager[] dialogues;
    bool[] isObjectiveComplete;

    public string GetMissionTitle() { return missionTitle; }
    public string GetCurrentObjective() { return objectives[objectiveID]; }
    public int GetObjectiveID() { return objectiveID; }
    public int GetObjectiveLength() { return objectives.Length; }
    public void NextObjective() { objectiveID++; }
    public void ResetObjective()
    {
        objectiveID = 0;
        for (int i = 0; i < objectives.Length; i++)
        {
            isObjectiveComplete[i] = false;
        }
    }
    public void FinishObjective() { isObjectiveComplete[objectiveID] = true; }
    public bool CheckObjectiveStatus() { return isObjectiveComplete[objectiveID]; }

    void Awake()
    {
        objectiveID = 0;
        isObjectiveComplete = new bool[objectives.Length];
        for (int i = 0; i < objectives.Length; i++) { isObjectiveComplete[i] = false; }
    }
}
