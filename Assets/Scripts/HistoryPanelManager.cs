using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryPanelManager : MonoBehaviour
{
    [SerializeField] GameObject targetGob;
    [SerializeField] VerticalListController verticalListController;

    DateTime currentDisplayDay;

    void Start()
    {
        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.ShowHistoryButton, ShowHistoryPanel);
        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.CloseHistoryButton, CloseHistoryPanel);
        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.PreDayButton, PreDayButtonFunction);
        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.NextDayButton, NextDayButtonFunction);
        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.CurrentDayButton, CurrentDayButtonFunction);
        currentDisplayDay = DateTime.Now;
    }

    public void ShowHistoryPanel()
    {
        // 객체 활성화
        targetGob.SetActive(true);

        // 모든 리스트 출력
        //PrintAllHistory();
        DateTime currentTime = DateTime.Now;
        currentDisplayDay = currentTime;
        PrintHistory(currentDisplayDay);
    
    }
    public void CloseHistoryPanel()
    {
        targetGob.SetActive(false);
        verticalListController.RemoveAllItem();
    }

    private void CurrentDayButtonFunction()
    {
        // 추후 달력을 보여주면서 원하는 날짜를 선택하게끔
        // 달력 UI를 나타나게 하는 기능을 추가하고 그 UI에서 따로 기능을 구현하는게 맞을까?
        Debug.Log("CurrentDayButtonFucntion:: 미구현 기능");
    }

    private void PreDayButtonFunction()
    {
        // 현재 보여주고 있는 날의 전날을 보여줌
        currentDisplayDay = currentDisplayDay.AddDays(-1);


        verticalListController.RemoveAllItem();
        PrintHistory(currentDisplayDay);
    }

    private void NextDayButtonFunction()
    {
        // 현재 보여주고 있는 날의 다음 날을 보여줌
        currentDisplayDay = currentDisplayDay.AddDays(1);
        verticalListController.RemoveAllItem();
        PrintHistory(currentDisplayDay);
    }

    private void PrintAllHistory()
    {
        IEnumerator<ScheduleDataManager.ScheduleData> enumerator = ScheduleDataManager.GetData;
        while (enumerator.MoveNext())
        {
            ScheduleDataManager.ScheduleData scheduleData = enumerator.Current;
            string content = scheduleData.scheduleContent;
            string startTime = String.Format("{0}:{1}", scheduleData.startHour, scheduleData.startMinute);
            string endTime = String.Format("{0}:{1}", scheduleData.endHour, scheduleData.endMinute);
            verticalListController.AddItem(content, startTime, endTime);
        }
    }

    private void PrintHistory(int year, int month, int day)
    {
        IEnumerator<ScheduleDataManager.ScheduleData> enumerator = ScheduleDataManager.GetData;
        while (enumerator.MoveNext())
        {
            ScheduleDataManager.ScheduleData scheduleData = enumerator.Current;
            if (scheduleData.startYear == year && scheduleData.startMonth == month && scheduleData.startDay == day)
            {
                string content = scheduleData.scheduleContent;
                string startTime = String.Format("{0}:{1}", scheduleData.startHour, scheduleData.startMinute);
                string endTime = String.Format("{0}:{1}", scheduleData.endHour, scheduleData.endMinute);
                verticalListController.AddItem(content, startTime, endTime);
            }
        }
    }

    private void PrintHistory(DateTime dateTime)
    {
        PrintHistory(dateTime.Year, dateTime.Month, dateTime.Day);
    }
}
