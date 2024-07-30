using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalListController : MonoBehaviour
{
    [SerializeField] GameObject contentPrefab;
    [SerializeField] Transform contentHolder;

    List<GameObject> lists = new List<GameObject>();

    public void AddItem(string scheduleContent, string startTime, string endTime)
    {
        if (contentPrefab != null && contentHolder != null)
        {
            GameObject item = Instantiate(contentPrefab, contentHolder);
            item.GetComponent<HistoryContentItem>().SetData(scheduleContent, startTime, endTime);
            lists.Add(item);
        }
    }

    public void RemoveItem(int index)
    {
        // index must be on Range in item list
        if (index < lists.Count && index >= 0)
        {
            // 대상 item을 리스트에서 제거
            GameObject tartgetItem = lists[index];
            lists.RemoveAt(index);

            // 대상 item을 제거
            Destroy(tartgetItem);
        }
    }

    public void RemoveAllItem()
    {
        // Destroy는 해당 프레임이 끝나기 전까지 오브젝트가 메모리에 남아 있음.
        // 그렇기에 모든 오브젝트를 삭제하는 명령을 한 뒤 리스트를 비워줘도 에러가 나지 않을 것이라 판단함.
        foreach (GameObject item in lists)
        {
            // 모든 아이템을 삭제. 
            Destroy(item);
        }

        lists.Clear();
    }
}
