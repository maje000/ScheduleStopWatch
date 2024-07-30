using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainSceneUIManager : MonoBehaviour
{
    static MainSceneUIManager instance;

    void Awake()
    {
        instance = this;
    }

    public enum TargetUI
    {
        ShowHistoryButton,
        CloseHistoryButton,
    }

    [SerializeField] Button showHistoryButton;
    [SerializeField] Button closeHistoryButton;

    public static void SetFunction(TargetUI targetUI, UnityAction action)
    {
        if (targetUI == TargetUI.ShowHistoryButton)
        {
            instance.showHistoryButton.onClick.RemoveAllListeners();
            instance.showHistoryButton.onClick.AddListener(action);
        }
        else if (targetUI == TargetUI.CloseHistoryButton)
        {
            instance.closeHistoryButton.onClick.RemoveAllListeners();
            instance.closeHistoryButton.onClick.AddListener(action);
        }
    }
}