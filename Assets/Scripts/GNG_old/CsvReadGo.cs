
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class CsvReadGo : MonoBehaviour
{

    private List<string[]> rowData = new List<string[]>();
    private string[] rowDataTemp = new string[8];
    private string id;

    public string version;

    public void csvstart()
    {

        // Creating First row of titles
        rowDataTemp[0] = "ID";
        rowDataTemp[0] = "Trial";
        rowDataTemp[1] = "Velocity";
        rowDataTemp[2] = "Go";
        rowDataTemp[3] = "NoGo";
        rowDataTemp[4] = "Clicked";
        rowDataTemp[5] = "correct";
        rowDataTemp[6] = "Inhibition I.";
        rowDataTemp[7] = "Reaction time";
        rowData.Add(rowDataTemp);
    }

    public void csvupdate(string id, int trial, float velocity, int gdog, int bdog,
                          int clicked, double ii, float RT)
    {
        int correct = clicked == gdog ? 1 : 0;
        if (trial == -1) { correct = -1; } // break trial
        // You can add up the values in as many cells as you want.
        rowDataTemp = new string[9];
        rowDataTemp[0] = "" + id;
        rowDataTemp[1] = "" + trial; // trial number
        rowDataTemp[2] = "" + velocity;
        rowDataTemp[3] = "" + gdog;
        rowDataTemp[4] = "" + bdog; 
        rowDataTemp[5] = "" + clicked;
        rowDataTemp[6] = "" + correct;
        rowDataTemp[7] = "" + ii; // inhibition index
        rowDataTemp[8] = "" + RT; // approx reaction time
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
        return PlayerPrefs.GetString("ID") + "-GNG-" + versionNumber + "-" + id + ".csv";
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
