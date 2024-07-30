using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScheduleDataManager : MonoBehaviour
{
    private static ScheduleDataManager instance;

    private string SAVE_HISTORY_DATA_PATH = "{0}/saveData.Json";
    private string SAVE_STATE_DATA_PATH = "{0}/state_cache.json";

    private struct ScheduleDataList
    {
        public List<ScheduleData> list;
    }

    [System.Serializable]
    public struct ScheduleData
    {
        public string scheduleContent;

        //public DateTime startTime;
        public int startYear;
        public int startMonth;
        public int startDay;
        public int startHour;
        public int startMinute;

        //public DateTime endTime;
        public int endYear;
        public int endMonth;
        public int endDay;
        public int endHour;
        public int endMinute;
    }

    public struct EnvironmentState
    {
        public bool isOnSchedule_cache;
        public string scheduleContent_cache;
        public int startYear_cache;
        public int startMonth_cache;
        public int startDay_cache;
        public int startHour_cache;
        public int startMinute_cache;
    }

    List<ScheduleData> _data = new List<ScheduleData>();
    DateTime _startTime;
    DateTime _endTime;
    string _scheduleContent = "";
    public string GetCurrentScheduleContent
    {
        get { return _scheduleContent; }
    }
    bool _isOnSchedule = false;
    public bool IsOnSchedule
    {
        get { return _isOnSchedule; }
    }

    void Awake()
    {
        instance = this;

        SAVE_HISTORY_DATA_PATH = string.Format(SAVE_HISTORY_DATA_PATH, Application.persistentDataPath);
        SAVE_STATE_DATA_PATH = string.Format(SAVE_STATE_DATA_PATH, Application.persistentDataPath);

        LoadEnvironmentState();
    }

    // Start is called before the first frame update
    void Start()
    {
        _data.Clear();
        LoadFromJson();
    }

    public void StartSchedule(string scheduleContent)
    {
        // 현재 일정이 진행 중일 경우 실행 취소
        if (_isOnSchedule == true) return;

        // 일정의 내용 중 일부 저장
        _startTime = DateTime.Now;
        _scheduleContent = scheduleContent;

        // 현재 상태 변경
        _isOnSchedule = true;
        SaveEnvironmentState();
    }

    public void EndSchedule()
    {
        // 현재 진행 중인 일정이 없을 경우 실행 취소
        if (_isOnSchedule == false) return;

        // 일정의 내용 중 일부 저장
        _endTime = DateTime.Now;

        // 일정을 기록
        ScheduleData data = new ScheduleData()
        {
            startYear = _startTime.Year,
            startMonth = _startTime.Month,
            startDay = _startTime.Day,
            startHour = _startTime.Hour,
            startMinute = _startTime.Minute,

            endYear = _endTime.Year,
            endMonth = _endTime.Month,
            endDay = _endTime.Day,
            endHour = _endTime.Hour,
            endMinute = _endTime.Minute,

            scheduleContent = _scheduleContent
        };

        // 리스트에 저장
        _data.Add(data);
        SaveToJson();
        
        // 현재 상태 변경
        _isOnSchedule = false;
        SaveEnvironmentState();
    }

    public static IEnumerator<ScheduleData> GetData
    {
        get
        {
            return instance._data.GetEnumerator();
        }
    }

    [ContextMenu("Show All Date data")]
    public void practice()
    {
        IEnumerator<ScheduleData> enumerator = _data.GetEnumerator();
        while(enumerator.MoveNext())
        {
            ScheduleData data = enumerator.Current;
            string scheduleContent = data.scheduleContent;
            string startTimeText = TimeToText(data.startMonth, data.startDay, data.startHour, data.startMinute);
            string endTimeText = TimeToText(data.endMonth, data.endDay, data.endHour, data.endMinute);

            Debug.Log($"Content::{scheduleContent} StartTime::{startTimeText} EndTime::{endTimeText}");
        }
    }

    private string TimeToText(int month, int day, int hour, int minute)
    {
        string startTimeText = string.Format("MM:{0}, DD:{1}, HH:{2}, MM:{3}",
            month,
            day,
            hour,
            minute);

        return startTimeText;
    }

    [ContextMenu("SaveTest")]
    public void SaveToJson()
    {
        if (_data != null)
        {
            string json = JsonUtility.ToJson(new ScheduleDataList() { list = _data }, true);

            Debug.Log(json);

            File.WriteAllText(Application.persistentDataPath + "/saveData.json", json);
        }
    }

    [ContextMenu("LoadTest")]
    public void LoadFromJson()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/saveData.json");

        if (!String.IsNullOrWhiteSpace(json))
        {
            ScheduleDataList a = JsonUtility.FromJson<ScheduleDataList>(json);
            _data = a.list;

            practice();
        }
    }
    
    private void SaveEnvironmentState()
    {
        if (_isOnSchedule)
        {
            string json = JsonUtility.ToJson(new EnvironmentState() { 
                isOnSchedule_cache = _isOnSchedule,
                scheduleContent_cache = _scheduleContent,
                startYear_cache = _startTime.Year,
                startMonth_cache = _startTime.Month,
                startDay_cache = _startTime.Day,
                startHour_cache = _startTime.Hour,
                startMinute_cache = _startTime.Minute});
            
            File.WriteAllText(Application.persistentDataPath + "/state_cache.json", json);
        }
        else
        {
            File.WriteAllText(Application.persistentDataPath + "/state_cache.json", "");
        }
    }

    private void LoadEnvironmentState()
    {
        if (!File.Exists(SAVE_STATE_DATA_PATH)) return;
        
        string json = File.ReadAllText(SAVE_STATE_DATA_PATH);
        if (!string.IsNullOrWhiteSpace(json))
        {
            EnvironmentState lastEnvironmentStateData = JsonUtility.FromJson<EnvironmentState>(json);
            if (lastEnvironmentStateData.isOnSchedule_cache)
            {
                _isOnSchedule = lastEnvironmentStateData.isOnSchedule_cache;
                _startTime = new DateTime(
                    lastEnvironmentStateData.startYear_cache,
                    lastEnvironmentStateData.startMonth_cache,
                    lastEnvironmentStateData.startDay_cache,
                    lastEnvironmentStateData.startHour_cache,
                    lastEnvironmentStateData.startMinute_cache,
                    00);
                _scheduleContent = lastEnvironmentStateData.scheduleContent_cache;
            }
        }
    }
}