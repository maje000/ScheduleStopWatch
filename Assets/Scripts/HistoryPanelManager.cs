using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryPanelManager : MonoBehaviour
{
    [SerializeField] GameObject targetGob;
    [SerializeField] VerticalListController verticalListController;

    void Start()
    {
        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.ShowHistoryButton, ShowHistoryPanel);
        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.CloseHistoryButton, CloseHistoryPanel);
    }

    public void ShowHistoryPanel()
    {
        // ��ü Ȱ��ȭ
        targetGob.SetActive(true);

        // ��� ����Ʈ ���
        //PrintAllHistory();
        DateTime currentTime = DateTime.Now;
        PrintHistory(currentTime.Year, currentTime.Month, currentTime.Day);
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

    public void CloseHistoryPanel()
    {
        targetGob.SetActive(false);
        verticalListController.RemoveAllItem();
    }
}
