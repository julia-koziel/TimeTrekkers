using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class CsvReadWrite : MonoBehaviour, IGameEventListener<(List<string[]>, string)>
{
    public DataHolder stageData;
    [HideInInspector]
    public DataHolder data;
    public IntVariable nTrials;
    public BoolVariable isParticipantsGo;
    [Space(10)]
    public StringVariable participantId;
    public StringVariable session;
    public StringVariable batteryId;
    public TranslatableString version;
    public DataGameEvent dataSubmission;
    string taskCode { get { return SceneManager.GetActiveScene().name; } }
    Regex badChars;

    List<string[]> stageCsv;
    List<string[]> trialsCsv
    {
        get
        {
            if (_trialsCsv == null)
            {
                _trialsCsv = new List<string[]>();
                _trialsCsv.Add(data.headers);
            }
            return _trialsCsv;
        }
        set => _trialsCsv = value;
    }
    List<string[]> _trialsCsv;

    void OnEnable() => RegisterListener();
    void OnDisable() => UnregisterListener();
    public void RegisterListener() => dataSubmission.RegisterListener(this, false);
    public void UnregisterListener() => dataSubmission.UnregisterListener(this);
    public void OnEventRaised((List<string[]>, string) dataTuple)
    {
        (List<string[]> data, string identifier) = dataTuple;
        OutputData(data, identifier);
    }

    void Start()
    {
        stageCsv = new List<string[]>();
        stageCsv.Add(stageData.headers);
        LogStageData(); // To record start time
        badChars = new Regex(@"[’',\n\t]", RegexOptions.Compiled);
    }

    public void LogTrialsData()
    {
        if (isParticipantsGo)
        {
            trialsCsv.Add(data.getVariables());
            data.Repaint(); // For seeing values in inspector
        }
    }

    public void LogStageData()
    {
        stageCsv.Add(stageData.getVariables());
        stageData.Repaint();
    }

    public void OutputStageData()
    {
        OutputData(stageCsv, "Stages");
    }

    public void OutputTrialsData()
    {
        if (isParticipantsGo)
        {
            while (trialsCsv.Count < nTrials + 1) // header row
            {
                trialsCsv.Add(data.missingRow);
            }
            OutputData(trialsCsv, getStageName());
            trialsCsv = null;
        }
    }

   public void OutputData(List<string[]> dataList, string identifier)
    {

        if (participantId.Value != "sfaritest nocsv")
        {
            var fileName = getFileName(identifier);
            var contents = "\n".Join(
                dataList.Select(row => "\t".Join(
                    row.Select(var => badChars.Replace(var, string.Empty))
                )));
            writeTsv(fileName, contents);
        }
    }

    void writeTsv(string fileName, string contents)
    {
        var folder = getFolderPath();
        System.IO.Directory.CreateDirectory(folder);
        var writer = System.IO.File.CreateText(folder + fileName);
        writer.WriteLine(contents);
        writer.Close();
    }

    string getStageName()
    {
        var stageVars = stageData.getVariables();
        var name = stageVars[stageData.headers.IndexOf("stage_name")];
        var views = stageVars[stageData.headers.IndexOf("views")];
        return $"{name}-{views}";
    }

    string getFileName(string stageName)
    {
        var timestamp = System.DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss");
        return $"sub-{participantId}_ses-{session}_bat-{batteryId}_task-{taskCode}-{stageName}-{version}-{timestamp}.tsv";
    }

    string getFolderPath()
    {
        string dataPath;
        
#if   UNITY_EDITOR
        dataPath = Application.dataPath + "/CSV";
#elif UNITY_ANDROID
        dataPath = Application.persistentDataPath;
#elif UNITY_IPHONE
        dataPath = Application.persistentDataPath;
#else
        dataPath = Application.dataPath;
#endif
        return $"{dataPath}/{participantId}/{taskCode}/";
    }
}
