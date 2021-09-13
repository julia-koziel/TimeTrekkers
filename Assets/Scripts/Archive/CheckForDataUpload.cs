using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

public class CheckForDataUpload : MonoBehaviour
{   

    public GameObject loadData;
    public string participantId;
    Queue<string> files;
    // Start is called before the first frame update
    void Start()
    {
        participantId = PlayerPrefs.GetString("ID");
        files = new Queue<string>();
        var tsvs = Directory.GetFiles(getFolderPath(), "*.tsv", SearchOption.AllDirectories);
        tsvs.Where(s => !Regex.IsMatch(s, @".+UPLOADED\.tsv$")).ForEach(files.Enqueue);
        var csvs = Directory.GetFiles(getFolderPath(), "*.csv", SearchOption.AllDirectories);
        csvs.Where(s => !Regex.IsMatch(s, @".+UPLOADED\.csv$")).ForEach(files.Enqueue);
        // get all files to upload
        // in 0.5f call upload
        if (files.Count > 0)
        {
            loadData.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData()
    {
        SceneManager.LoadScene(14);
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

