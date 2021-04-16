
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class CsvReadWriteBees : MonoBehaviour
{

    private List<string[]> rowData = new List<string[]>();
    private string[] rowDataTemp = new string[6];
    private string id;

    public void csvstart()
    {

        // Creating First row of titles manually..

        rowDataTemp[0] = "Trial";
        rowDataTemp[1] = "Block";
        rowDataTemp[2] = "Direction";
        rowDataTemp[3] = "Correct";
        rowDataTemp[4] = "Time";
        rowDataTemp[5] = "coherence";
        rowData.Add(rowDataTemp);
    }

    public void csvupdate(int trial, int block,
                          int direction, bool correct, float time, float coherence)
    {
        trial++;
        Debug.Log(trial + ": " + coherence);
        rowDataTemp = new string[6];
        rowDataTemp[0] = "" + trial; // trial number
        rowDataTemp[1] = "" + block; // Block number
        rowDataTemp[2] = "" + direction; // which direction is right
        rowDataTemp[3] = "" + correct; // was click correct
        rowDataTemp[4] = "" + time; // reaction time
        rowDataTemp[5] = "" + coherence; // are all bees coherent
        rowData.Add(rowDataTemp);

    }



    public void output(){

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string folderPath = getFolderPath();
        System.IO.Directory.CreateDirectory(folderPath);
        
        string filePath = folderPath + getFilePath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();

    }

    // Following method is used to retrive the relative path as device platform
    private string getFolderPath()
    {
        string dataPath;
        
#if UNITY_EDITOR
        dataPath = Application.dataPath + "/CSV/";
#elif UNITY_ANDROID
        dataPath = Application.persistentDataPath+"/";
#elif UNITY_IPHONE
        dataPath = Application.persistentDataPath+"/";
#else
        dataPath = Application.dataPath +"/";
#endif
        return dataPath + PlayerPrefs.GetString("ID") + "/";
    }

    private string getFilePath()
    {
        id = GetUniqueID();
        string versionNumber = getVersionNumber();
        return PlayerPrefs.GetString("ID") + "-MC-" + versionNumber + "-" + id + ".csv";
    }

    private string GetUniqueID()
    {
        string[] split = System.DateTime.Now.TimeOfDay.ToString().Split(new Char[] { ':', '.' });
        string id = "";
        for (int i = 0; i < split.Length; i++)
        {
            id += split[i];
        }
        return id;
    }

    private string getVersionNumber()
    {
        string key = PrefsKeys.Keys.VersionNumber.ToString();
        return PlayerPrefs.GetString(key, "NA");
    }
}
