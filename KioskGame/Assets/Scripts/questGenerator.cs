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
            // �� ����Ʈ�� ũ�Ⱑ �ٸ��� �������� ����
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
                // quests ����Ʈ�� ��� �� orders ����Ʈ�� ���� ���� ������ �������� ����
                return false;
            }
        }
        return true;
    }
    public bool burgerquestsEqual(List<burgerQuestElement> quests, List<burgerOrderElement> orders)
    {
        if (quests.Count != orders.Count)
        {
            // �� ����Ʈ�� ũ�Ⱑ �ٸ��� �������� ����
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
                // quests ����Ʈ�� ��� �� orders ����Ʈ�� ���� ���� ������ �������� ����
                return false;
            }
        }
        return true;
    }
    void burgerDisplayQuests()
    {
        // GridLayoutGroup �ʱ�ȭ
        ClearQuestGrid();
        
        // ������ ����Ʈ ��ҵ��� GridLayoutGroup�� ǥ��
        foreach (burgerQuestElement quest in burgerquestElements)
        {
            GameObject questObject = Instantiate(UIManager.Instance.cafeQuestPrefab, UIManager.Instance.gridLayoutGroup.transform);
            TMP_Text detailsText = questObject.GetComponentsInChildren<TMP_Text>()[1];
            TMP_Text itemText = questObject.GetComponentsInChildren<TMP_Text>()[0];
            if (UIManager.difficulty == 1 || !quest.item.Contains("����"))
            {      
                detailsText.text = $"{quest.item}";
                itemText.text = $"    - {quest.quantity}��";
            }
            else if (UIManager.difficulty == 2)
            {
                detailsText.text = $"{quest.item}";
                itemText.text = $"    - {quest.quantity}�� - �߰� ���� : {quest.details}";
            }
            else if (UIManager.difficulty == 3)
            {
                if (quest.item.Contains("����"))
                {
                    if (quest.type == "��ǰ")
                        itemText.text = $"    - {quest.quantity}��";

                    if (quest.type == "��Ʈ")
                        itemText.text = $"    - {quest.quantity}�� - {quest.side} - {quest.drink}";
                }
                detailsText.text = $"{quest.item}({quest.type})";
            }
        }
    }
    void cafeDisplayQuests()
    {
        // GridLayoutGroup �ʱ�ȭ
        ClearQuestGrid();

        // ������ ����Ʈ ��ҵ��� GridLayoutGroup�� ǥ��
        foreach (cafeQuestElement quest in cafequestElements)
        {
            GameObject questObject = Instantiate(UIManager.Instance.cafeQuestPrefab, UIManager.Instance.gridLayoutGroup.transform);
            TMP_Text detailsText = questObject.GetComponentsInChildren<TMP_Text>()[1];
            TMP_Text itemText = questObject.GetComponentsInChildren<TMP_Text>()[0];
            if (quest.item.Contains("����ũ"))
            {
                detailsText.text = $"{quest.item}";
                itemText.text = $"    - {quest.quantity}��";
            }
            else
            {
                if (UIManager.difficulty == 1)
                {
                    if (quest.item.Contains("����ũ"))
                    {
                        detailsText.text = $"{quest.item}";
                        itemText.text = $"    - {quest.quantity}��";
                    }
                    else
                    {
                        itemText.text = $"    - {quest.quantity}��";
                        detailsText.text = $"{quest.item}({quest.type})";
                    }
                }
                if (UIManager.difficulty == 2)
                {
                    itemText.text = $"    - {quest.size} - {quest.quantity}��";
                    detailsText.text = $"{quest.item}({quest.type})";
                }
                if (UIManager.difficulty == 3)
                {
                    if (quest.item.Contains("���ڶ�") || quest.item.Contains("������") || quest.item.Contains("�����"))
                    {
                        itemText.text = $"    - {quest.size} - {quest.quantity}��";
                        detailsText.text = $"{quest.item}({quest.type})";
                    }
                    else
                    {
                        itemText.text = $"    - {quest.quantity}�� - {quest.size} - {quest.coffebean} - {quest.shot}��";
                        detailsText.text = $"{quest.item}({quest.type})";
                    }
                }
            }
        }
    }

    public void ClearQuestGrid()
    {
        // GridLayoutGroup�� ��� �ڽ� ��ü ����
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
                    Transform check = childGameObject.transform.Find("check"); // �ڽĿ�����Ʈ �� check�̸��� ���� ���� ������Ʈ ã��
                    Transform blank = childGameObject.transform.Find("blank"); // �ڽĿ�����Ʈ �� blank�̸��� ���� ���� ������Ʈ ã��

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
                    Transform check = childGameObject.transform.Find("check"); // �ڽĿ�����Ʈ �� check�̸��� ���� ���� ������Ʈ ã��
                    Transform blank = childGameObject.transform.Find("blank"); // �ڽĿ�����Ʈ �� blank�̸��� ���� ���� ������Ʈ ã��

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
