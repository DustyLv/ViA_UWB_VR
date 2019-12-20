using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

public class JSONRecorder : MonoBehaviour
{
    [SerializeField]
    private bool record = false;
    public bool Record
    {
        get { return record; }
        set
        {
            record = value;
            if(value == false)
                EndSession();
        }
    }

    public TextAsset destinationFile;
    public string fileName = "Recording";
    private bool isSameSession = false;

    StreamWriter writer = null;

    /// <summary>
    /// Receives and writes JSON data to a file. Adds DateTime to file name.
    /// </summary>
    /// <param name="data"></param>
    public void ReceiveAndProcess(string data)
    {
        #if UNITY_EDITOR
        string path = "";
        
        print(data);

        if (!isSameSession)
        {
            DateTime dateTimeNow = DateTime.Now;
            string dateTimeString = dateTimeNow.ToString("_dd-MM-yyyy_hh-mm-ss");

            TextAsset text = new TextAsset();
            AssetDatabase.CreateAsset(text, "Assets/Resources/" + fileName + dateTimeString + ".txt");
            AssetDatabase.SaveAssets();
            destinationFile = text;
            isSameSession = true;
        }
        path = AssetDatabase.GetAssetPath(destinationFile);

        //Write some text to the test.txt file
        writer = new StreamWriter(path, true);
        writer.WriteLine(data);
        writer.Close();
        #endif
    }

    /// <summary>
    /// Ends recording session.
    /// </summary>
    void EndSession()
    {

        isSameSession = false;
        //writer.Close();
    }
}
