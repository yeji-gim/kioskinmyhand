using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class burgerOrder : MonoBehaviour
{
    public static burgerOrder Instance { get; private set; }
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
    public GameObject orderPrefab;  
    public GridLayoutGroup gridLayoutGroup;
    public Color purple = new Color(0.3568f, 0.1451f, 0.9529f);
    public List<burgerOrderElement> orders = new List<burgerOrderElement>();
    public Dictionary<string, int> items = new Dictionary<string, int>();

    private string selectedType;
    public string selectedItem;
    private string selectedDrink;
    private string selectedSide;
    private string selectedDetails;
    private int selectedQuantity;
    private int baseprice;
    public int selectedIndex;

    bool clickDrink = false;
    bool clicktSide = false;
    bool restrictclickDrink = false;
    bool restrictclickSide = false;

    private int count;

    [Header("UI")]
    public TMP_Text[] optionCountTexts;
    public TMP_Text[] optionTotalTexts;
    public TMP_Text[] nameTexts;
    public Button[] optionButtons;
    public TMP_Text[] optionLabels;

    [Header("��Ʈ")]
    public GameObject side;
    public GameObject drink;

    private void Start()
    {
        initializeUI();
        setItems();
    }

    public void initializeUI()
    {
        foreach (TMP_Text countText in optionCountTexts)
            countText.text = "1";

        foreach (TMP_Text totalText in optionTotalTexts)
            totalText.text = "0";

        count = 1;
        selectedIndex = -1;
    }

    public void setItems()
    {
        items.Add("��������", 5000);
        items.Add("ġŲ����", 5200);
        items.Add("����������", 5500);
        items.Add("�ֱ׹���", 5500);
        items.Add("�����������", 7000);
        items.Add("��ؽ�����", 7500);
        items.Add("ġŲ�ʰ�", 4000);
        items.Add("����Ƣ��", 2000);
        items.Add("ġ�ƽ", 2500);
        items.Add("��ٸ�ġŲ", 3000);
        items.Add("���̽�ũ��", 1500);
        items.Add("����Ÿ��Ʈ", 3000);
        items.Add("�ݶ�", 2000);
        items.Add("���̴�", 2000);
        items.Add("�������ֽ�", 2500);
        items.Add("����", 2000);
    }

    public void resetButton()
    {
        foreach (Button button in optionButtons)
            UIManager.Instance.changeColorButton(button, Color.white);

        foreach (TMP_Text label in optionLabels)
            label.color = Color.black;
    }

    public void addQuantity()
    {
        count++;
        updateQuantityAndTotal();
    }

    public void subtractQuatntity()
    {
        if (count > 1)
        {
            count--;
            updateQuantityAndTotal();
        }
        else
            return;
    }

    void updateQuantityAndTotal()
    {
        int newTotalPrice = baseprice * count;

        int index = UIManager.difficulty - 1;
        if (!selectedItem.Contains("����")) index = 0;

        optionCountTexts[index].text = count.ToString();
        optionTotalTexts[index].text = newTotalPrice.ToString();
    }

    int getItemPrice(string itemName, Dictionary<string, int> items)
    {
        if (items.ContainsKey(itemName))
            return items[itemName];
        else
            return 0;
    }

    // �̸��� ���� ��������
    public void getName()
    {
        selectedItem = EventSystem.current.currentSelectedGameObject.name;
        baseprice = getItemPrice(selectedItem, items);

        int index = UIManager.difficulty - 1;
        if (!selectedItem.Contains("����")) index = 0;

        optionTotalTexts[index].text = (int.Parse(optionTotalTexts[index].text) + baseprice).ToString();
        nameTexts[index].text = selectedItem;

        if(index == 0) UIManager.Instance.openDifficultyOption(UIManager.Instance.burgereasyoption);
        else if(index == 1) UIManager.Instance.openDifficultyOption(UIManager.Instance.burgermediumoption);
        else UIManager.Instance.openDifficultyOption(UIManager.Instance.burgerdiffucltoption);
    }

    // ������ ����
    public void setInfo(string what, string name, Button[] buttons, int purpleIndex, int num)
    {
        if(what == "detail")
            selectedDetails = name;
        if (what == "side")
            selectedSide = name;
        if (what == "drink")
            selectedDrink = name;

        for (int i = num; i< buttons.Length; i++)
        {
            UIManager.Instance.changeColorButton(buttons[i], i == purpleIndex ? purple : Color.white);
            optionLabels[i].GetComponent<TMP_Text>().color = i == purpleIndex ? Color.white : Color.black; 
        }
    }
    public void detailsno() => setInfo("detail","����", optionButtons.Take(3).ToArray(), 0,0);
    public void detailonion() => setInfo("detail", "���� ����", optionButtons.Take(3).ToArray(), 1,0);
    public void detailpickle() => setInfo("detail", "��Ŭ ����", optionButtons.Take(3).ToArray(), 2,0);

    // ��ǰ���� ��Ʈ���� ����
    public void isSetCheck(string type, Button[] buttons, int purpleIndex)
    {
        selectedType = type;
        if (type == "��ǰ")
        {
            resetButton();
            side.gameObject.SetActive(false);
            drink.gameObject.SetActive(false);
        }
        else
        {
            side.gameObject.SetActive(true);
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            UIManager.Instance.changeColorButton(buttons[i], i == purpleIndex ? purple : Color.white);
            optionLabels[i].GetComponent<TMP_Text>().color = i == purpleIndex ? Color.white : Color.black;
        }
    }

    public void isnoSet() => isSetCheck("��ǰ", optionButtons.Take(5).ToArray(), 3);
    public void isSet()=> isSetCheck("��Ʈ", optionButtons.Take(5).ToArray(), 4);

    public void chicken()
    {
        if (!restrictclickSide)
        {
            drink.gameObject.SetActive(true);
            clicktSide = true;
            setInfo("side", "��ٸ�ġŲ", optionButtons.Take(8).ToArray(), 7,5);
            baseprice += 1000;
            optionTotalTexts[1].text = (baseprice * count).ToString();
            restrictclickSide = true;
        }
    }

    public void frenchfries()
    {
        restrictclickSide = false;
        drink.gameObject.SetActive(true);
        setInfo("side", "����Ƣ��", optionButtons.Take(8).ToArray(), 5,5);
        if (clicktSide)
        {
            baseprice -= 1000;
            optionTotalTexts[1].text = (baseprice * count).ToString();
            clicktSide = false;
        }
    }

    public void cheesestick()
    {
        drink.gameObject.SetActive(true);
        restrictclickSide = false;
        setInfo("side", "ġ�ƽ", optionButtons.Take(8).ToArray(), 6,5);
        if (clicktSide)
        {
            baseprice -= 1000;
            clicktSide = false;
            optionTotalTexts[1].text = (baseprice * count).ToString();
        }
    }

    public void orangejuice()
    {
        if (!restrictclickDrink)
        {
            clickDrink = true;
            setInfo("drink", "�������ֽ�", optionButtons.Take(12).ToArray(), 10,8);
            baseprice += 500;
            optionTotalTexts[2].text = (baseprice * count).ToString();
            restrictclickDrink = true;
        }
    }

    public void cola()
    {
        restrictclickDrink = false;
        setInfo("drink", "�ݶ�", optionButtons.Take(12).ToArray(), 8,8);
        if (clickDrink)
        {
            baseprice -= 500;
            optionTotalTexts[2].text = (baseprice * count).ToString();
            clickDrink = false;
        }
    }

    public void soda()
    {
        restrictclickDrink = false;
        setInfo("drink", "���̴�", optionButtons.Take(12).ToArray(), 9,8);
        if (clickDrink)
        {
            baseprice -= 500;
            optionTotalTexts[2].text = (baseprice * count).ToString();
            clickDrink = false;
        }
    }

    public void water()
    {
        restrictclickDrink = false;
        setInfo("drink", "����", optionButtons.Take(12).ToArray(), 11,8);
        if (clickDrink)
        {
            baseprice -= 500;
            clickDrink = false;
            optionTotalTexts[2].text = (baseprice * count).ToString();
        }
    }

    // �ɼ� ���� ����
    public void resetOption()
    {
        selectedDetails = "";
        selectedDrink = "";
        selectedItem = "";
        selectedSide = "";
        selectedType = "";
        count = 1;
        for (int i = 0; i < optionCountTexts.Length; i++)
        {
            optionCountTexts[i].text = count.ToString();
            optionTotalTexts[i].text = "0";
        }
        baseprice = 0;
        resetButton();
        restrictclickDrink = false;
        restrictclickSide = false;
        clickDrink = false;
        clicktSide = false;

        side.gameObject.SetActive(false);
        drink.gameObject.SetActive(false);
    }

    // �ֹ� �߰�
    public void addToOrder()
    {
        selectedIndex++;

        if (orders.Count >= 2)
        {
            UIManager.Instance.fullOrder.SetActive(true);
            return;
        }
        // �ֹ� ������ ����
        burgerOrderElement newOrder = new burgerOrderElement();
        newOrder.details = selectedDetails;
        newOrder.drink = selectedDrink;
        newOrder.quantity = count;
        newOrder.item = selectedItem;
        newOrder.side = selectedSide;
        newOrder.type = selectedType;
        newOrder.index = selectedIndex;

        // �ֹ��� ����Ʈ�� �߰�
        orders.Add(newOrder);
        resetOption();
        resetButton();

        // �ֹ� Ȯ�� ȣ��
        AddQuestToGrid(newOrder);
        UIManager.Instance.closeDifficultyOption(UIManager.Instance.burgereasyoption);
        UIManager.Instance.closeDifficultyOption(UIManager.Instance.burgermediumoption);
        UIManager.Instance.closeDifficultyOption(UIManager.Instance.burgerdiffucltoption);
    }

    public void deleteOrder(int index)
    {
        orders.RemoveAll(order => order.index == index);
    }

    void AddQuestToGrid(burgerOrderElement order)
    {
        GameObject questObject = Instantiate(orderPrefab, gridLayoutGroup.transform);
        cancelOrder questIndex = questObject.GetComponent<cancelOrder>();
        if (questIndex != null)
        {
            questIndex.SetCellIndex(selectedIndex);
        }

        TMP_Text itemText = questObject.GetComponentsInChildren<TMP_Text>()[1];
        if (UIManager.difficulty == 1 || !order.item.Contains("����"))
            itemText.text = $"- {order.quantity}��";
        else if (UIManager.difficulty == 2)
            itemText.text = $"- {order.quantity}�� - �߰� ���� : {order.details}";
        else if (UIManager.difficulty == 3)
        {
            if (order.type == "��ǰ")
                itemText.text = $"- {order.quantity}��";
            else
                itemText.text = $"- {order.quantity}�� - {order.side} - {order.drink}";
        }
        TMP_Text detailsText = questObject.GetComponentsInChildren<TMP_Text>()[0];
        detailsText.text = $"{order.item}";
    }
}