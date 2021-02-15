using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
public class CSVReadWriteRLP : MonoBehaviour
{
   private List<string[]> rowData = new List<string[]>();
    private string[] rowDataTemp = new string[7];
    private string id;

    public string level =  "L3";

    public void csvstart()
    {

        // Creating First row of titles manually.

        rowDataTemp[0] = "Trial";
        rowDataTemp[1] = "rewarded_side";
        rowDataTemp[2] = "correct";
        rowDataTemp[3] = "cm_correct";
        rowDataTemp[4] = "reactionTime";
    
        rowData.Add(rowDataTemp);
    }


    public void csvupdate (int trial, int clicked_pos, float correct_flag, float hitrate, double reactionTime)
    {

       // You can add up the values in as many cells as you want.
        rowDataTemp = new string[7];
        rowDataTemp[0] = "" + trial; // trial number
        rowDataTemp[1] = "" + clicked_pos; // where magic box displayed
        rowDataTemp[2] = "" + correct_flag; // where was clicked
        rowDataTemp[3] = "" + hitrate; // cumulative n of corrects
        rowDataTemp[4] = "" + reactionTime; // dprime

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
        return PlayerPrefs.GetString("ID") + "-RL-" + "-" + id + ".csv";
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
