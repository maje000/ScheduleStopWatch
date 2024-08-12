using System;
using UnityEngine;
using UnityEngine.UI;

public class StopWatchStateMachine : MonoBehaviour
{
    const string ON_WAKE = "기상";
    const string ON_SLEEP = "취침";
    const string DO_JOB = "일";
    const string TAKE_FOOD = "식사";

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

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name}:: Start is Excuted");

        sdManager = GetComponent<ScheduleDataManager>();

        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.StartButton, OnStartSchedule);
        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.EndButton, EndSchedule);
        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.WakeButton, () => sdManager.AddScehdule(ON_WAKE));
        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.SleepButton, () => sdManager.AddScehdule(ON_SLEEP));
        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.DoJobButton, () => OnStartSchedule(DO_JOB));
        MainSceneUIManager.SetFunction(MainSceneUIManager.TargetUI.TakeFoodButton, () => OnStartSchedule(TAKE_FOOD));

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

    /// <summary>
    /// 입력한 스케쥴을 시작
    /// </summary>
    private void OnStartSchedule()
    {
        // 일정 내용이 공란일 경우 에러를 보내거나 되돌림
        if (String.IsNullOrEmpty(inputField.text)) return;

        // 데이터 기록
        sdManager.StartSchedule(inputField.text);

        // 버튼 상태 변화
        ChangeState(StopWatchState.OnSchedule);
    }

    /// <summary>
    /// 스케쥴 내용을 입력하며 시작
    /// </summary>
    /// <param name="scheduleContent"></param>
    private void OnStartSchedule(string scheduleContent)
    {
        inputField.text = scheduleContent;

        sdManager.StartSchedule(scheduleContent);
        ChangeState(StopWatchState.OnSchedule);
    }

    private void EndSchedule()
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
    }
}
