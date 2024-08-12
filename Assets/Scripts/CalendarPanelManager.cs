using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CalendarPanelManager : MonoBehaviour
{
    private const string YEAR_DISPAY_FORMAT = "{0:D4}";
    private const string MONTH_DISPAY_FORMAT = "{0:D2}";
    private const string DAY_DISPAY_FORMAT = "{0:D2}";

    [SerializeField] GameObject targetGob;

    [SerializeField] TextMeshProUGUI _yearText;
    [SerializeField] TextMeshProUGUI _monthText;

    //[SerializeField] Button _preMonthButton;
    //[SerializeField] Button _nextMonthButton;
    //[SerializeField] Button _closeCalendarButton;
    [SerializeField] Button[] _days;

    private void Start()
    {
        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.PreMonthButton, DisplayPreMonth);
        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.NextMonthButton, DisplayNextMonth);
        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.CloseCalendarButton, () => targetGob.SetActive(false));
        //_preMonthButton.onClick.AddListener(DisplayPreMonth);
        //_nextMonthButton.onClick.AddListener(DisplayNextMonth);
        //_closeCalendarButton.onClick.AddListener(() => targetGob.SetActive(false));
    }

    public void OpenPanel()
    {
        DateTime now = DateTime.Now;
        DisplayMonth(now.Year, now.Month);

        targetGob.SetActive(true);
    }

    //[ContextMenu("DisplayMonth")]
    private void DisplayMonth()
    {
        DisplayMonth(2024, 8);
    }

    private void DisplayMonth(int year, int month)
    {
        _yearText.text = string.Format(YEAR_DISPAY_FORMAT, year);
        _monthText.text = string.Format(MONTH_DISPAY_FORMAT, month);

        DateTime time = new DateTime(year, month, 1);
        int startWeek = (int)time.DayOfWeek; // start Week sun == 0, *** sat == 6
        int lastDay = DateTime.DaysInMonth(year, month);

        int dayCount = 1;
        foreach(Button day in _days)
        {
            // 시작하는 요일 체크
            if (startWeek > 0)
            {
                day.GetComponentInChildren<TextMeshProUGUI>().text = string.Format(DAY_DISPAY_FORMAT, 0);
                day.GetComponent<Image>().color = Color.clear;
                day.interactable = false;
                startWeek--;
            }
            // 마지막 일자를 지날 경우
            else if (dayCount > lastDay)
            {
                day.GetComponentInChildren<TextMeshProUGUI>().text = string.Format(DAY_DISPAY_FORMAT, 0);
                day.GetComponent<Image>().color = Color.clear;
                day.interactable = false;
            }
            else
            {
                day.GetComponentInChildren<TextMeshProUGUI>().text = string.Format(DAY_DISPAY_FORMAT, dayCount);
                day.GetComponent<Image>().color = Color.white;
                day.interactable = true;
                dayCount++;
            }
        }
    }

    //[ContextMenu("DisplayPreMonth")]
    private void DisplayPreMonth()
    {
        int.TryParse(_yearText.text, out int year);
        int.TryParse(_monthText.text, out int month);

        month--;

        if (month > 12)
        {
            year++;
            month = 1;
        }
        else if (month < 1)
        {
            year--;
            month = 12;
        }

        DisplayMonth(year, month);
    }

    //[ContextMenu("DisplayNextMonth")]
    private void DisplayNextMonth()
    {
        int.TryParse(_yearText.text, out int year);
        int.TryParse(_monthText.text, out int month);

        month++;

        if (month > 12)
        {
            year++;
            month = 1;
        }
        else if (month < 1)
        {
            year--;
            month = 12;

        }

        DisplayMonth(year, month);
    }
}
