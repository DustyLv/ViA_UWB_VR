using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Globalization;

public class JSONPlayback : MonoBehaviour
{
    public bool playRecording = false;

    public TextAsset recordingFileToPlay;
    private List<string> recordingList = new List<string>();

    private string previousDataTimestamp = "";
    private string currentDataTimestamp = "";

    JSONWorker jsonWorker;

    /// <summary>
    /// Converts a file with recording data into a string list.
    /// </summary>
    void Start()
    {
        jsonWorker = GetComponent<JSONWorker>();
        recordingList = recordingFileToPlay.text.Split('\n').ToList();

        recordingList.RemoveRange(0, 10); // remove first 10 lines, because they are Unity TextAsset generated info and not usable JSON
        recordingList.RemoveRange(recordingList.Count - 2, 2); // remove last 2 lines, because 2nd to last is the button press (which we don't need since it starts recording again), and last one is empty
    }

    /// <summary>
    /// Gets keyboard key down (Space key) and toggles playback state. Based on state, starts or stops the playback coroutine.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playRecording = !playRecording;
            if (playRecording == true)
                StartCoroutine("Play");
            else
                StopCoroutine("Play");
        }
    }

    /// <summary>
    /// Replays recorded JSON data in real-time by iterating over the recordingList. Calculates time between each iteration based on data provided by JSON data entry in the list.
    /// </summary>
    /// <returns></returns>
    IEnumerator Play()
    {
        foreach (string s in recordingList)
        {
            // get the sensor data from current entry in recording list
            SensorDataRaw sensorData = jsonWorker.JsonToObjectReturn(s);
            // get the timestamp for current entry; we know that the data entry is going to start either with the button press or a position change (where we really just need the 1st data), so we just check for those; get timestamp from that data entry;
            foreach(Datastream ds in sensorData.datastreams)
            {
                var val = string.Concat(ds.current_value.Where(c => !char.IsWhiteSpace(c)));

                if (ds.id == "user_button")
                {
                    currentDataTimestamp = ds.at;
                    break;
                }
                if (ds.id == "posX")
                {
                    currentDataTimestamp = ds.at;
                    break;
                }
            }

            // for the first data entry prevDataTimestamp is going to be empty, so to not get errors we set it to currentDataTimestamp
            if (String.IsNullOrEmpty(previousDataTimestamp))
            {
                previousDataTimestamp = currentDataTimestamp;
            }

            // convert timestamps for current and previous data from JSON to DateTime objects and get the time between them
            DateTime curTime = ConvertStringToDateTime(currentDataTimestamp);
            DateTime prevTime = ConvertStringToDateTime(previousDataTimestamp);
            TimeSpan duration = curTime.Subtract(prevTime);

            // convert TimeSpan to floats separately to minutes, seconds and milliseconds and calculate those into seconds to later use in WaitForSeconds
            float minutes = duration.Minutes;
            float seconds = duration.Seconds;
            float milliseconds = duration.Milliseconds;
            float timeBetweenData = (minutes * 60f) + (seconds) + (milliseconds / 1000f);

            jsonWorker.JsonToObject(s);
            yield return new WaitForSeconds(timeBetweenData);
            previousDataTimestamp = currentDataTimestamp;
        }
        playRecording = false;
    }

    /// <summary>
    /// Converts a date/time string into a DateTime and returns it.
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    DateTime ConvertStringToDateTime(string dt)
    {
        CultureInfo cultureInfo = new CultureInfo("lv-LV");
        DateTime time = DateTime.ParseExact(dt, "yyyy-MM-dd HH:mm:ss.fff", cultureInfo);
        return time;
    }
}
