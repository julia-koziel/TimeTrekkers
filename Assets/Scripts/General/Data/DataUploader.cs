using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

public class DataUploader : MonoBehaviour 
{
    const string url = "http://psyc.bbk.ac.uk/teachbrite/scripts/getDataAms.php";
    public StringVariable participantId;
    public float maxNoSuccessTime;
    public float maxPartialSuccessTime;
    public float minTime;
    [Space(10)]
    public GameObject pleaseWait;
    public GameObject successText;
    public GameObject errorText;
    public GameObject continueButton;
    public GameObject dataButtonLocation;
    public GameObject loadingCircle;
    public GameObject loadingCloud;
    public BoolVariable dataNeedsUploading;
    float time = 0;
    bool isError = false;
    bool partialSuccess = false;
    bool isSuccess = false;
    Queue<string> files;

    void Start()
    {
        files = new Queue<string>();
        var tsvs = Directory.GetFiles(getFolderPath(), "*.tsv", SearchOption.AllDirectories);
        tsvs.Where(s => !Regex.IsMatch(s, @".+UPLOADED\.tsv$")).ForEach(files.Enqueue);
        var csvs = Directory.GetFiles(getFolderPath(), "*.csv", SearchOption.AllDirectories);
        csvs.Where(s => !Regex.IsMatch(s, @".+UPLOADED\.csv$")).ForEach(files.Enqueue);
        // get all files to upload
        // in 0.5f call upload
        if (files.Count > 0)
        {
            this.In(1).Call(() => StartCoroutine(Upload(files.Dequeue())));
        }
        else
        {
            isSuccess = true;
        }

    }

    void Update()
    {
        time += Time.deltaTime;
        if (isSuccess && time > minTime)
        {
            dataNeedsUploading.boolValue = false;
            pleaseWait.SetActive(false);
            loadingCircle.SetActive(false);
            loadingCloud.SetActive(false);
            successText.SetActive(true);
            continueButton.SetActive(true);
        }
        else if ((isError && time > minTime) || (!partialSuccess && time > maxNoSuccessTime) || time > maxPartialSuccessTime)
        {
            dataNeedsUploading.boolValue = true;
            pleaseWait.SetActive(false);
            loadingCircle.SetActive(false);
            loadingCloud.SetActive(false);
            errorText.SetActive(true);
            dataButtonLocation.SetActive(true);
            continueButton.SetActive(true);
        }
    }

    IEnumerator Upload(string filePath)
    {
        (var headers, var contents) = getFileContents(filePath);
        var fileName = Path.GetFileName(filePath);

        WWWForm form = new WWWForm();

        form.AddField("file", fileName);
        form.AddField("headers", headers);
        form.AddField("contents", contents);
        
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            // TODO handle errors in more detail
            if (www.isNetworkError || www.isHttpError)
            {
                isError = true;
                yield break;
            }
        }

        // if successful, change filename
        string newName;
        if (filePath.EndsWith("tsv")) newName = filePath.Replace(".tsv", "-UPLOADED.tsv");
        else newName = filePath.Replace(".csv", "-UPLOADED.csv");

        File.Move(filePath, newName);
# if UNITY_EDITOR
        if (File.Exists(filePath + ".meta")) File.Move(filePath + ".meta", newName + ".meta");
# endif

        partialSuccess = true;
        if (files.Count > 0)
        {
            StartCoroutine(Upload(files.Dequeue()));
        }
        else
        {
            print("success!");
            isSuccess = true;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(2);
    }

    (string headers, string contents) getFileContents(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        var headers = lines[0].Split('\t');
        var contents = lines.Skip(1).Select(l => l.Split('\t').ToArray()).ToArray();
        
        return (JsonConvert.SerializeObject(headers), JsonConvert.SerializeObject(contents));
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
        return $"{dataPath}/{participantId}/";
    }
}