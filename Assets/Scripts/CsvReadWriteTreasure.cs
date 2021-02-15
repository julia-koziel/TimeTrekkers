using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class CsvReadWriteTreasure : MonoBehaviour
{
    // // Start is called before the first frame update
    private List<string[]> rowData = new List<string[]>();
    private string[] rowDataTemp = new string[10];
    private string id;

    // public string level =  "L1";

    public void csvstart()
    {

        // Creating First row of titles manually.

        rowDataTemp[0] = "id";
        rowDataTemp[1] = "Trial";
        rowDataTemp[2] = "rewarded_side";
        rowDataTemp[3] = "clicked_side";
        rowDataTemp[4] = "left_chest";
        rowDataTemp[5] = "right_chest";
        rowDataTemp[6] = "dprime";
        rowDataTemp[7] = "hitrate";
        rowDataTemp[8] = "comissionerror";
        rowDataTemp[9] = "RT";
    
        rowData.Add(rowDataTemp);
    }


    public void csvupdate (string id, int trial, int clicked_pos, float correct_flag, float box_shown, float box2_shown, double dprime, float hitrate, float comissionerror, double reactionTime)
    {

       // You can add up the values in as many cells as you want.
        rowDataTemp = new string[10];
        rowDataTemp[0] = "" + id;
        rowDataTemp[1] = "" + trial; // trial number
        rowDataTemp[2] = "" + clicked_pos; // where magic box displayed
        rowDataTemp[3] = "" + correct_flag; // where was click
        rowDataTemp[4] = "" + box_shown; // box on left
        rowDataTemp[5] = "" + box2_shown; // box on right
        rowDataTemp[6] = "" + dprime; // dprime
        rowDataTemp[7] = "" + hitrate; // cumulative n of incorrects
        rowDataTemp[8] = "" + comissionerror; // reaction time
        rowDataTemp[9] = "" + reactionTime; // 

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
