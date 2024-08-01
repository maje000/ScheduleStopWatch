using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    int _year;
    int _month;
    int _day;
    int _hour;
    int _minute;
    int _second;
    
    void Start()
    {
        DateTime now = DateTime.Now;
        UpdateTime(now);
    }

    // Update is called once per frame
    void Update()
    {
        DateTime now = DateTime.Now;
        if (_second != now.Second)
        {
            // 초, 분, 시간, 날, 달로 나누어서 분기를 구분하는 것도 괜찮을 듯
            UpdateTime(now);
            
            OnUpdateEvent();
            //Debug.Log(DateTime.Now);
        }
    }

    private void UpdateTime(DateTime time)
    {
        _year = time.Year;
        _month = time.Month;
        _day = time.Day;
        _hour = time.Hour;
        _minute = time.Minute;
        _second = time.Second;
    }

    [SerializeField] private TMPro.TextMeshProUGUI currentTimeText;
    private const string currentTimeDisplayFormat = "{0:D2}/{1:D2}  {2:D2}:{3:D2}:{4:D2}";

    private void OnUpdateEvent()
    {
        currentTimeText.text = string.Format(currentTimeDisplayFormat, _month, _day, _hour, _minute, _second);
    }
}
