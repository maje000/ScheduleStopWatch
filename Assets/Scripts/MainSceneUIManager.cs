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