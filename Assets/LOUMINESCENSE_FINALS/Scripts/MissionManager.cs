using UnityEngine;

public class MissionManager : MonoBehaviour
{
    int objectiveID = 0;
    [SerializeField] string missionTitle;
    [SerializeField] string[] objectives;
    
    public string GetMissionTitle() { return missionTitle; }
    public string GetCurrentObjective() { return objectives[objectiveID]; }
    public void NextObjective() { objectiveID++; }
    public void ResetObjective() { objectiveID = 0; }

    void Awake() { ResetObjective(); }
}
