
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class CsvReadSustainedAttention : MonoBehaviour
{

    private List<string[]> rowData = new List<string[]>();
    private string[] rowDataTemp = new string[7];
    private string id;

    public string level =  "L3";

    public void csvstart()
    {

        // Creating First row of titles manually.
        rowDataTemp[0] = "id";
        rowDataTemp[1] = "time";       // From beginning of trial, for click ordering
        rowDataTemp[2] = "trial";      // 1 trial = 1 car
        rowDataTemp[3] = "trial_type"; // 1 is Pip's car, 0 is other car
        rowDataTemp[4] = "clicked";    // 1 is clicked, 0 is not clicked
        rowDataTemp[5] = "correct";    // whether click was correct or not
        rowDataTemp[6] = "RT";
        rowData.Add(rowDataTemp);
    }

    public void csvupdate(string id, float time, int trial, int shipType, int clicked,
                         float reactionTime)
    {
        int correct = shipType == clicked ? 1 : 0;

        rowDataTemp = new string[7];
        rowDataTemp[0] = "" + id;
        rowDataTemp[1] = "" + time;    // From beginning of trial, for click ordering
        rowDataTemp[2] = "" + trial;   // 1 trial = 1 ship
        rowDataTemp[3] = "" + shipType; // 1 is target ship, 0 is other ships
        rowDataTemp[4] = "" + clicked; // 1 is clicked, 0 is not clicked
        rowDataTemp[5] = "" + correct; // whether click was correct or not
        rowDataTemp[6] = "" + reactionTime;
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
        return PlayerPrefs.GetString("ID") + "-SA-" + "-" + id + ".csv";
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

}
