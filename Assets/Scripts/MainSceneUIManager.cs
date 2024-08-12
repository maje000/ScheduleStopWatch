using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainSceneUIManager : MonoBehaviour
{
    static MainSceneUIManager instance;

    void Awake()
    {
        instance = this;
        SetFunction(TargetUI.ExitButton, () => Application.Quit());
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
    }

    //[SerializeField] Button showHistoryButton;
    //[SerializeField] Button closeHistoryButton;
    //[SerializeField] Button exitButton;
    //[SerializeField] Button preDayButton;
    //[SerializeField] Button nextDayButton;
    //[SerializeField] Button currentButton;
    //[SerializeField] Button startButton;
    //[SerializeField] Button endButton;

    [SerializeField] List<Button> buttons;

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
}