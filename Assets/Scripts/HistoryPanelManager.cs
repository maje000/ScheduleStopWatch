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
        // ��ü Ȱ��ȭ
        targetGob.SetActive(true);

        // ��� ����Ʈ ���
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
        // ���� �޷��� �����ָ鼭 ���ϴ� ��¥�� �����ϰԲ�
        // �޷� UI�� ��Ÿ���� �ϴ� ����� �߰��ϰ� �� UI���� ���� ����� �����ϴ°� ������?
        Debug.Log("CurrentDayButtonFucntion:: �̱��� ���");
    }

    private void PreDayButtonFunction()
    {
        // ���� �����ְ� �ִ� ���� ������ ������
        currentDisplayDay = currentDisplayDay.AddDays(-1);


        verticalListController.RemoveAllItem();
        PrintHistory(currentDisplayDay);
    }

    private void NextDayButtonFunction()
    {
        // ���� �����ְ� �ִ� ���� ���� ���� ������
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
