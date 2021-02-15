using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CsvReadWrite : MonoBehaviour
{
    public DataHolder stageData;
    [HideInInspector]
    public DataHolder data;
    public IntVariable nTrials;
    public BoolVariable isParticipantsGo;
    [Space(10)]
    public StringVariable participantId;
    public StringVariable session;
    // public TranslatableString version;
    string taskCode { get { return SceneManager.GetActiveScene().name; } }

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

    void Start()
    {
        stageCsv = new List<string[]>();
        stageCsv.Add(stageData.headers);
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
        var fileName = getFileName("Stages");
        var contents = "\n".Join(stageCsv.Select(row => "\t".Join(row)));
        writeTsv(fileName, contents);
    }

    public void OutputTrialsData()
    {
        if (isParticipantsGo)
        {
            while (trialsCsv.Count < nTrials + 1) // header row
            {
                trialsCsv.Add(data.missingRow);
            }
            var fileName = getFileName(getStageName());
            var contents = "\n".Join(trialsCsv.Select(row => "\t".Join(row)));
            writeTsv(fileName, contents);
            trialsCsv = null;
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
        return $"sub-{participantId}_ses-{session}_task-{taskCode}-{stageName}-{1.2}-{timestamp}.tsv";
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
