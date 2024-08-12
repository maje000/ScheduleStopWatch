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
        ShowHistoryButton, // HistoryPanel ���� ��ư
        CloseHistoryButton, // HistoryPanel���� ��ư
        ExitButton, // ���� ��ư
        PreDayButton, // ���� �� ��ư in HistoryPanel
        NextDayButton, // ���� �� ��ư in HistoryPanel
        CurrentDayButton, // ���� �� ǥ�� ��ư in HistoryPanbel
        StartButton, // ������ ���� ��ư
        EndButton, // ������ ���� ��ư
        WakeButton, // ��� ��ư
        SleepButton, // ��ħ ��ư
        DoJobButton, // �� ���� ��ư
        TakeFoodButton, // �Ļ� ���� ��ư
        PreMonthButton, // ���� �� ��ư in CalendarPanel
        NextMonthButton, // ���� �� ��ư in CalendarPanel
        CloseCalendarButton, // CalendarPanel ���� ��ư
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