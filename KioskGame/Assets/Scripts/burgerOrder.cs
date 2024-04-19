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

    [Header("세트")]
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
        items.Add("비프버거", 5000);
        items.Add("치킨버거", 5200);
        items.Add("슈림프버거", 5500);
        items.Add("애그버거", 5500);
        items.Add("더블비프버거", 7000);
        items.Add("비앤슈버거", 7500);
        items.Add("치킨너겟", 4000);
        items.Add("감자튀김", 2000);
        items.Add("치즈스틱", 2500);
        items.Add("통다리치킨", 3000);
        items.Add("아이스크림", 1500);
        items.Add("에그타르트", 3000);
        items.Add("콜라", 2000);
        items.Add("사이다", 2000);
        items.Add("오렌지주스", 2500);
        items.Add("생수", 2000);
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
        if (!selectedItem.Contains("버거")) index = 0;

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

    // 이름과 가격 가져오기
    public void getName()
    {
        selectedItem = EventSystem.current.currentSelectedGameObject.name;
        baseprice = getItemPrice(selectedItem, items);

        int index = UIManager.difficulty - 1;
        if (!selectedItem.Contains("버거")) index = 0;

        optionTotalTexts[index].text = (int.Parse(optionTotalTexts[index].text) + baseprice).ToString();
        nameTexts[index].text = selectedItem;

        if(index == 0) UIManager.Instance.openDifficultyOption(UIManager.Instance.burgereasyoption);
        else if(index == 1) UIManager.Instance.openDifficultyOption(UIManager.Instance.burgermediumoption);
        else UIManager.Instance.openDifficultyOption(UIManager.Instance.burgerdiffucltoption);
    }

    // 디테일 설정
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
    public void detailsno() => setInfo("detail","없음", optionButtons.Take(3).ToArray(), 0,0);
    public void detailonion() => setInfo("detail", "양파 빼고", optionButtons.Take(3).ToArray(), 1,0);
    public void detailpickle() => setInfo("detail", "피클 빼고", optionButtons.Take(3).ToArray(), 2,0);

    // 단품인지 세트인지 설정
    public void isSetCheck(string type, Button[] buttons, int purpleIndex)
    {
        selectedType = type;
        if (type == "단품")
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

    public void isnoSet() => isSetCheck("단품", optionButtons.Take(5).ToArray(), 3);
    public void isSet()=> isSetCheck("세트", optionButtons.Take(5).ToArray(), 4);

    public void chicken()
    {
        if (!restrictclickSide)
        {
            drink.gameObject.SetActive(true);
            clicktSide = true;
            setInfo("side", "통다리치킨", optionButtons.Take(8).ToArray(), 7,5);
            baseprice += 1000;
            optionTotalTexts[1].text = (baseprice * count).ToString();
            restrictclickSide = true;
        }
    }

    public void frenchfries()
    {
        restrictclickSide = false;
        drink.gameObject.SetActive(true);
        setInfo("side", "감자튀김", optionButtons.Take(8).ToArray(), 5,5);
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
        setInfo("side", "치즈스틱", optionButtons.Take(8).ToArray(), 6,5);
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
            setInfo("drink", "오렌지주스", optionButtons.Take(12).ToArray(), 10,8);
            baseprice += 500;
            optionTotalTexts[2].text = (baseprice * count).ToString();
            restrictclickDrink = true;
        }
    }

    public void cola()
    {
        restrictclickDrink = false;
        setInfo("drink", "콜라", optionButtons.Take(12).ToArray(), 8,8);
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
        setInfo("drink", "사이다", optionButtons.Take(12).ToArray(), 9,8);
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
        setInfo("drink", "생수", optionButtons.Take(12).ToArray(), 11,8);
        if (clickDrink)
        {
            baseprice -= 500;
            clickDrink = false;
            optionTotalTexts[2].text = (baseprice * count).ToString();
        }
    }

    // 옵션 리셋 설정
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

    // 주문 추가
    public void addToOrder()
    {
        selectedIndex++;

        if (orders.Count >= 2)
        {
            UIManager.Instance.fullOrder.SetActive(true);
            return;
        }
        // 주문 정보를 생성
        burgerOrderElement newOrder = new burgerOrderElement();
        newOrder.details = selectedDetails;
        newOrder.drink = selectedDrink;
        newOrder.quantity = count;
        newOrder.item = selectedItem;
        newOrder.side = selectedSide;
        newOrder.type = selectedType;
        newOrder.index = selectedIndex;

        // 주문을 리스트에 추가
        orders.Add(newOrder);
        resetOption();
        resetButton();

        // 주문 확인 호출
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
        if (UIManager.difficulty == 1 || !order.item.Contains("버거"))
            itemText.text = $"- {order.quantity}개";
        else if (UIManager.difficulty == 2)
            itemText.text = $"- {order.quantity}개 - 추가 사항 : {order.details}";
        else if (UIManager.difficulty == 3)
        {
            if (order.type == "단품")
                itemText.text = $"- {order.quantity}개";
            else
                itemText.text = $"- {order.quantity}개 - {order.side} - {order.drink}";
        }
        TMP_Text detailsText = questObject.GetComponentsInChildren<TMP_Text>()[0];
        detailsText.text = $"{order.item}";
    }
}