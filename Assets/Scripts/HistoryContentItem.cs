using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HistoryContentItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scheduleContent;
    [SerializeField] TextMeshProUGUI _startEndTime;

    /// <summary>
    /// Set History content item's display data.
    /// </summary>
    /// <param name="scheduleContent"></param>
    /// <param name="startTime">00:00</param>
    /// <param name="endTime">00:00</param>
    public void SetData(string scheduleContent, string startTime, string endTime)
    {
        _scheduleContent.text = scheduleContent;
        _startEndTime.text = startTime + "/" + endTime;
    }
}
