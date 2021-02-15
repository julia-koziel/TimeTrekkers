using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;


public class KittyReadWriteCsv : MonoBehaviour
{
    private List<string[]> rowData = new List<string[]>();
    private string[] rowDataTemp = new string[9];
    private string id;
    // Start is called before the first frame update

    public void csvstart()
    
    {

        // Creating First row of titles manually..
        rowDataTemp[0] = "Trial";
        rowDataTemp[1] = "correct";
        rowDataTemp[2] = "firstvibrationlevel";
        rowDataTemp[3] = "secondvibrationlevel";

        rowData.Add(rowDataTemp);
    } 


    // Update is called once per frame
   public void csvupdate(int trial, int correct, int firstvibrationlevel, int secondvibrationlevel)
    {
        // You can add up the values in as many cells as you want.
        rowDataTemp = new string[9];
        rowDataTemp[0] = "" + trial; // trial number
        rowDataTemp[1] = "" + correct; // whether the response is correct
        rowDataTemp[2] = "" + firstvibrationlevel; // first vibration amplitude
        rowDataTemp[3] = "" + secondvibrationlevel; // second vibration amplitude


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
        return PlayerPrefs.GetString("ID") + "-Kitty" + "-" + id + ".csv";
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

