using System;
using UnityEngine;
using UnityEngine.UI;

public class StopWatchStateMachine : MonoBehaviour
{
    ScheduleDataManager sdManager;

    [SerializeField] Button startButton;
    [SerializeField] Button endButton;
    [SerializeField] TMPro.TMP_InputField inputField;

    public enum StopWatchState
    {
        None = -1,
        BeforeStartSchedule = 0,
        OnSchedule = 1,
    }

    StopWatchState currentState = StopWatchState.None;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name}:: Start is Excuted");

        sdManager = GetComponent<ScheduleDataManager>();

        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.StartButton, OnStartButton);
        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.EndButton, OnEndButton);

        // 최초 초기화
        if (sdManager.IsOnSchedule)
        {
            inputField.text = sdManager.GetCurrentScheduleContent;
            ChangeState(StopWatchState.OnSchedule);
        }
        else
        {
            ChangeState(StopWatchState.BeforeStartSchedule);
        }
    }

    private void OnStartButton()
    {
        // 일정 내용이 공란일 경우 에러를 보내거나 되돌림
        if (String.IsNullOrEmpty(inputField.text)) return;

        // 데이터 기록
        sdManager.StartSchedule(inputField.text);

        // 버튼 상태 변화
        ChangeState(StopWatchState.OnSchedule);
    }

    private void OnEndButton()
    {
        // 데이터 기록
        sdManager.EndSchedule();

        // 버튼 상태 변화
        ChangeState(StopWatchState.BeforeStartSchedule);
    }

    private void ChangeState(StopWatchState state)
    {
        // 일정이 시작 전 대기 상태
        if (state == StopWatchState.BeforeStartSchedule)
        {
            // // 데이터 기록
            // sdManager.EndSchedule();

            // 시작 버튼 활성화
            startButton.interactable = true;
            // 종료 버튼 비활성화
            endButton.interactable = false;
            // 일정 내용을 공란으로 + 일정 내용 수정 가능하게
            inputField.interactable = true;
            inputField.text = "";
        }
        // 일정을 수행하는 중
        else if (state == StopWatchState.OnSchedule)
        {
            // // 일정 내용이 공란일 경우 에러를 보내거나 되돌림
            // if (String.IsNullOrEmpty(inputField.text)) return;

            // // 데이터 기록
            // sdManager.StartSchedule(inputField.text);

            // 시작 버튼 비활성화
            startButton.interactable = false;
            // 종료 버튼 활성화
            endButton.interactable = true;
            // 일정 내용을 수정 못하게 막기
            inputField.interactable = false;
        }

        currentState = state;
    }
}
