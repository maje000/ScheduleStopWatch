using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainSceneUIManager : MonoBehaviour
{
    static MainSceneUIManager instance;

    HistoryPanelManager historyPanelManager;
    CalendarPanelManager calendarPanelManager;

    [SerializeField] List<Button> buttons;
    [SerializeField] MonoBehaviour[] managers;

    void Awake()
    {
        instance = this;
        SetFunction(TargetUI.ExitButton, () => Application.Quit());

        historyPanelManager = GetComponent<HistoryPanelManager>();
        calendarPanelManager = GetComponent<CalendarPanelManager>();
        //if (exitButton != null)
        //{
        //    exitButton.onClick.RemoveAllListeners();
        //    exitButton.onClick.AddListener(() =>
        //    {
        //        Application.Quit();
        //    });
        //}
    }

    public enum TargetUI
    {
        ShowHistoryButton, // HistoryPanel 여는 버튼
        CloseHistoryButton, // HistoryPanel종료 버튼
        ExitButton, // 종료 버튼
        PreDayButton, // 이전 날 버튼 in HistoryPanel
        NextDayButton, // 다음 날 버튼 in HistoryPanel
        CurrentDayButton, // 현재 날 표시 버튼 in HistoryPanbel
        StartButton, // 스케쥴 시작 버튼
        EndButton, // 스케쥴 종료 버튼
        WakeButton, // 기상 버튼
        SleepButton, // 추침 버튼
        DoJobButton, // 일 시작 버튼
        TakeFoodButton, // 식사 시작 버튼
        PreMonthButton, // 이전 달 버튼 in CalendarPanel
        NextMonthButton, // 다음 달 버튼 in CalendarPanel
        CloseCalendarButton, // CalendarPanel 종료 버튼
    }

    public enum TargetManager
    {
        HistoryPanelManager,
        CalendarPanelManager,
    }

    //[SerializeField] Button showHistoryButton;
    //[SerializeField] Button closeHistoryButton;
    //[SerializeField] Button exitButton;
    //[SerializeField] Button preDayButton;
    //[SerializeField] Button nextDayButton;
    //[SerializeField] Button currentButton;
    //[SerializeField] Button startButton;
    //[SerializeField] Button endButton;


    public static void SetFunction(TargetUI targetUI, UnityAction action)
    {
        //if (targetUI == TargetUI.ShowHistoryButton)
        //{
        //    instance.showHistoryButton.onClick.RemoveAllListeners();
        //    instance.showHistoryButton.onClick.AddListener(action);
        //}
        //else if (targetUI == TargetUI.CloseHistoryButton)
        //{
        //    instance.closeHistoryButton.onClick.RemoveAllListeners();
        //    instance.closeHistoryButton.onClick.AddListener(action);
        //}

        foreach (Button button in instance.buttons)
        {
            if (targetUI.ToString() == button.name)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(action);
            }
        }
    }

    public static Component GetManager(TargetManager targetManager)
    {
        foreach(MonoBehaviour manager in instance.managers)
        {
            if (targetManager.ToString() == manager.GetType().ToString())
            {
                return manager;
            }
        }

        return null;
    }
}