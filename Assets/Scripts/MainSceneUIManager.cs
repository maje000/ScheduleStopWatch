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
        ShowHistoryButton,
        CloseHistoryButton,
        ExitButton,
        PreDayButton,
        NextDayButton,
        CurrentDayButton,
    }

    [SerializeField] Button showHistoryButton;
    [SerializeField] Button closeHistoryButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button preDayButton;
    [SerializeField] Button nextDayButton;
    [SerializeField] Button currentButton;

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