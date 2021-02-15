using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class StageList : MonoBehaviour {
    public Stage[] stages;
    public StringVariable currentStageName;
    public IntVariable currentStageViews;
    public Stage currentStage;
    public int[] stageIds;
    public int[] deletedIds;
    public static Stage emptyStage 
    {
        get
        {
            var empty = new Stage();
            empty.stageType = Stage.StageType.UI;
            empty.stageName = "Empty";
            return empty;
        }
    }
    public Stage GetStage(int id) => stages.First(s => s.stageId == id);
}