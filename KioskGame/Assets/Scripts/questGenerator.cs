using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class questGenerator : MonoBehaviour
{
    public static questGenerator Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public List<cafeQuestElement> cafequestElements;
    public List<burgerQuestElement> burgerquestElements;
    public List<GameObject> quests;
    public static int count_answer = 0;
    private void FixedUpdate()
    {
        if (cafequestElements != null && cafeOrder.Instance.orders != null)
            cafecheckEqual(cafequestElements, cafeOrder.Instance.orders);
        if (burgerquestElements != null && burgerOrder.Instance.orders != null)
            burgercheckEqual(burgerquestElements, burgerOrder.Instance.orders);
    }
    public void GeneratecafeQuests()
    {
        cafequestElements = new List<cafeQuestElement>();

        for (int i = 0; i < 2; i++)
        {
            cafeQuestElement newQuest = new cafeQuestElement();
            cafequestElements.Add(newQuest);
        }
        while (true)
        {
            if (cafequestElements[0].item == cafequestElements[1].item)
            {
                cafequestElements.RemoveAt(1);
                cafeQuestElement newQuest = new cafeQuestElement();
                cafequestElements.Add(newQuest);
            }
            else break;
        }
        cafeDisplayQuests();
    }
    public void GenerateburgerQuests()
    {
        burgerquestElements = new List<burgerQuestElement>();

        for (int i = 0; i < 2; i++)
        {
            burgerQuestElement newQuest = new burgerQuestElement();
            burgerquestElements.Add(newQuest);

        }
        while (true)
        {
            if (burgerquestElements[0].item == burgerquestElements[1].item)
            {
                burgerquestElements.RemoveAt(1);
                burgerQuestElement newQuest = new burgerQuestElement();
                burgerquestElements.Add(newQuest);
            }
            else break;
        }
        burgerDisplayQuests();
    }
    public bool cafequestsEqual(List<cafeQuestElement> quests, List<cafeOrderElement> orders)
    {
        if (quests.Count != orders.Count)
        {
            // 두 리스트의 크기가 다르면 동일하지 않음
            return false;
        }
        foreach (var quest in quests)
        {
            bool found = false;

            foreach (var order in orders)
            {
                if (quest.Equals(order))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                // quests 리스트의 요소 중 orders 리스트에 없는 것이 있으면 동일하지 않음
                return false;
            }
        }
        return true;
    }
    public bool burgerquestsEqual(List<burgerQuestElement> quests, List<burgerOrderElement> orders)
    {
        if (quests.Count != orders.Count)
        {
            // 두 리스트의 크기가 다르면 동일하지 않음
            return false;
        }
        foreach (var quest in quests)
        {
            bool found = false;

            foreach (var order in orders)
            {
                if (quest.Equals(order))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                // quests 리스트의 요소 중 orders 리스트에 없는 것이 있으면 동일하지 않음
                return false;
            }
        }
        return true;
    }
    void burgerDisplayQuests()
    {
        // GridLayoutGroup 초기화
        ClearQuestGrid();
        
        // 생성된 퀘스트 요소들을 GridLayoutGroup에 표시
        foreach (burgerQuestElement quest in burgerquestElements)
        {
            GameObject questObject = Instantiate(UIManager.Instance.cafeQuestPrefab, UIManager.Instance.gridLayoutGroup.transform);
            TMP_Text detailsText = questObject.GetComponentsInChildren<TMP_Text>()[1];
            TMP_Text itemText = questObject.GetComponentsInChildren<TMP_Text>()[0];
            if (UIManager.difficulty == 1 || !quest.item.Contains("버거"))
            {      
                detailsText.text = $"{quest.item}";
                itemText.text = $"    - {quest.quantity}개";
            }
            else if (UIManager.difficulty == 2)
            {
                detailsText.text = $"{quest.item}";
                itemText.text = $"    - {quest.quantity}개 - 추가 사항 : {quest.details}";
            }
            else if (UIManager.difficulty == 3)
            {
                if (quest.item.Contains("버거"))
                {
                    if (quest.type == "단품")
                        itemText.text = $"    - {quest.quantity}개";

                    if (quest.type == "세트")
                        itemText.text = $"    - {quest.quantity}개 - {quest.side} - {quest.drink}";
                }
                detailsText.text = $"{quest.item}({quest.type})";
            }
        }
    }
    void cafeDisplayQuests()
    {
        // GridLayoutGroup 초기화
        ClearQuestGrid();

        // 생성된 퀘스트 요소들을 GridLayoutGroup에 표시
        foreach (cafeQuestElement quest in cafequestElements)
        {
            GameObject questObject = Instantiate(UIManager.Instance.cafeQuestPrefab, UIManager.Instance.gridLayoutGroup.transform);
            TMP_Text detailsText = questObject.GetComponentsInChildren<TMP_Text>()[1];
            TMP_Text itemText = questObject.GetComponentsInChildren<TMP_Text>()[0];
            if (quest.item.Contains("케이크"))
            {
                detailsText.text = $"{quest.item}";
                itemText.text = $"    - {quest.quantity}개";
            }
            else
            {
                if (UIManager.difficulty == 1)
                {
                    if (quest.item.Contains("케이크"))
                    {
                        detailsText.text = $"{quest.item}";
                        itemText.text = $"    - {quest.quantity}개";
                    }
                    else
                    {
                        itemText.text = $"    - {quest.quantity}잔";
                        detailsText.text = $"{quest.item}({quest.type})";
                    }
                }
                if (UIManager.difficulty == 2)
                {
                    itemText.text = $"    - {quest.size} - {quest.quantity}잔";
                    detailsText.text = $"{quest.item}({quest.type})";
                }
                if (UIManager.difficulty == 3)
                {
                    if (quest.item.Contains("초코라떼") || quest.item.Contains("녹차라떼") || quest.item.Contains("딸기라떼"))
                    {
                        itemText.text = $"    - {quest.size} - {quest.quantity}잔";
                        detailsText.text = $"{quest.item}({quest.type})";
                    }
                    else
                    {
                        itemText.text = $"    - {quest.quantity}잔 - {quest.size} - {quest.coffebean} - {quest.shot}번";
                        detailsText.text = $"{quest.item}({quest.type})";
                    }
                }
            }
        }
    }

    public void ClearQuestGrid()
    {
        // GridLayoutGroup의 모든 자식 객체 제거
        foreach (Transform child in UIManager.Instance.gridLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void cafecheckEqual(List<cafeQuestElement> quests, List<cafeOrderElement> orders)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            cafeQuestElement quest = quests[i];
            for (int j = 0; j < orders.Count; j++)
            {
                cafeOrderElement order = orders[j];
                if (quest.Equals(order))
                {
                    Transform childTransform = UIManager.Instance.gridLayoutGroup.transform.GetChild(i);
                    GameObject childGameObject = childTransform.gameObject;
                    Transform check = childGameObject.transform.Find("check"); // 자식오브젝트 중 check이름을 가진 게임 오브젝트 찾기
                    Transform blank = childGameObject.transform.Find("blank"); // 자식오브젝트 중 blank이름을 가진 게임 오브젝트 찾기

                    if (check != null)
                    {
                        check.gameObject.SetActive(true);
                    }
                    if (blank != null)
                    {
                        blank.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    public void burgercheckEqual(List<burgerQuestElement> quests, List<burgerOrderElement> orders)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            burgerQuestElement quest = quests[i];

            for (int j = 0; j < orders.Count; j++)
            {
                burgerOrderElement order = orders[j];
                if (quest.Equals(order))
                {
                    Transform childTransform = UIManager.Instance.gridLayoutGroup.transform.GetChild(i);
                    GameObject childGameObject = childTransform.gameObject;
                    Transform check = childGameObject.transform.Find("check"); // 자식오브젝트 중 check이름을 가진 게임 오브젝트 찾기
                    Transform blank = childGameObject.transform.Find("blank"); // 자식오브젝트 중 blank이름을 가진 게임 오브젝트 찾기

                    if (check != null)
                    {
                        check.gameObject.SetActive(true);
                    }
                    if (blank != null)
                    {
                        blank.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
