using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class cafeOrder : MonoBehaviour
{
    public static cafeOrder Instance { get; private set; }
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
    public GameObject orderPrefab;  // Äù½ºÆ® Ç¥½Ã¸¦ À§ÇÑ ÇÁ¸®ÆÕ
    public GridLayoutGroup gridLayoutGroup;  // ÁÖ¹®À» Ç¥½ÃÇÒ GridLayoutGroup
    public Color purple = new Color(0.3568f, 0.1451f, 0.9529f);
    public List<cafeOrderElement> orders = new List<cafeOrderElement>();
    public Dictionary<string, int> items = new Dictionary<string, int>();

    private string selectedTemperature;
    public string selectedCoffeeType;
    private string selectedSize;
    private string selectedCaffeine;
    private int selectedShot;
    private int baseprice;
    public int selectedIndex;

    bool clickLarge = false;
    bool clicktwoshot = false;
    bool clickdecaff = false;

    bool restrictclickLarge = false;
    bool restrictclicktwoshot = false;
    bool restrictclickdecaff = false;
    private int count;
    public static string temperature;

    [Header("UI")]
    public TMP_Text[] optionCountTexts;
    public TMP_Text[] optionTotalTexts;
    public TMP_Text[] nameTexts;
    public Button[] optionButtons;
    public TMP_Text[] optionLabels;
    

    private void Start()
    {
        InitializeUI();
        setItems();
    }
    private void InitializeUI()
    {
        foreach (TMP_Text countText in optionCountTexts)
        {
            countText.text = "1";
        }
        foreach (TMP_Text totalText in optionTotalTexts)
        {
            totalText.text = "0";
        }
        count = 1;
        selectedIndex = -1;
    }
    public void setItems()
    {
        items.Add("¾Æ¸Þ¸®Ä«³ë(ICE)", 4000);
        items.Add("¾Æ¸Þ¸®Ä«³ë(HOT)", 3500);
        items.Add("Ä«Æä¶ó¶¼(ICE)", 4500);
        items.Add("Ä«Æä¶ó¶¼(HOT)", 4000);
        items.Add("Ä«ÇªÄ¡³ë(HOT)", 4000);
        items.Add("Ä«ÇªÄ¡³ë(ICE)", 4500);
        items.Add("ÃÊÄÚ¶ó¶¼(HOT)", 4500);
        items.Add("ÃÊÄÚ¶ó¶¼(ICE)", 5000);
        items.Add("³ìÂ÷¶ó¶¼(HOT)", 5000);
        items.Add("³ìÂ÷¶ó¶¼(ICE)", 5500);
        items.Add("µþ±â¶ó¶¼(HOT)", 5500);
        items.Add("µþ±â¶ó¶¼(ICE)", 6000);
        items.Add("ÃÊÄÚÄÉÀÌÅ©", 6500);
        items.Add("µþ±âÄÉÀÌÅ©", 7000);
        items.Add("³ìÂ÷ÄÉÀÌÅ©", 7000);
    }
    public void resetButton()
    {
        foreach (Button button in optionButtons)
        {
            UIManager.Instance.changeColorButton(button, Color.white);
        }
        foreach (TMP_Text label in optionLabels)
        {
            label.color = Color.black;
        }
    }

    public void resetQuantity()
    {
        count = 1;
    }

    public void addQuantity()
    {
        count++;
        UpdateQuantityAndTotal();
    }

    public void subtracQuatntity()
    {
        if (count > 1)
        {
            count--;
            UpdateQuantityAndTotal();
        }
        else
            return;
    }

    public void UpdateQuantityAndTotal()
    {
        int newTotalPrice = baseprice * count;

        int index = UIManager.difficulty - 1;
        if (selectedCoffeeType.Contains("ÄÉÀÌÅ©")|| UIManager.difficulty == 1) index = 0;
        else if (selectedCoffeeType.Contains("ÃÊÄÚ¶ó¶¼") || selectedCoffeeType.Contains("µþ±â¶ó¶¼") || selectedCoffeeType.Contains("³ìÂ÷¶ó¶¼")) index = 1;

        optionCountTexts[index].text = count.ToString();
        optionTotalTexts[index].text = newTotalPrice.ToString();
    }
    public void setInfo(string type, string name, Button[] buttons, int purpleIndex, int start, int end)
    {
        if (type == "size")
            selectedSize = name;
        if (type == "shot")
            if (name == "1shot") selectedShot = 1;
            else selectedShot = 2;
        if (type == "caffeine")
            selectedCaffeine = name;
        for (int i = start; i < end; i++)
        {
            UIManager.Instance.changeColorButton(buttons[i], i == purpleIndex ? purple : Color.white);
            optionLabels[i].GetComponent<TMP_Text>().color = i == purpleIndex ? Color.white : Color.black;
        }
    }
    public void regularSize()
    {
        selectedSize = "Regular";
        restrictclickLarge = false;
        int index = 0;
        if (UIManager.difficulty == 2)
        {
            index = 1;
            setInfo("size", "Regular", optionButtons.Take(2).ToArray(), 0,0,2);
        }
        if (UIManager.difficulty == 3)
        {
            if (selectedCoffeeType.Contains("ÃÊÄÚ¶ó¶¼") || selectedCoffeeType.Contains("µþ±â¶ó¶¼") || selectedCoffeeType.Contains("³ìÂ÷¶ó¶¼"))
            {
                index = 1;
                setInfo("size", "Regular", optionButtons.Take(4).ToArray(), 0, 0, 2);
            }
            else
            {
                index = 2;
                setInfo("size", "Regular", optionButtons.Take(4).ToArray(), 2, 2, 4);
            }
        }
        if (clickLarge)
        {
            baseprice -= 500;
            optionTotalTexts[index].text = (baseprice * count).ToString();
            clickLarge = false;
        }

    }

    public void LargeSize()
    {
        clickLarge = true;
        baseprice += 500;
        int index = 0;
        if (UIManager.difficulty == 2  && !restrictclickLarge)
        {
            setInfo("size", "Large", optionButtons.Take(2).ToArray(), 1,0,2);
            index = 1;
        }
        if (UIManager.difficulty == 3 && !restrictclickLarge)
        {
            if (selectedCoffeeType.Contains("ÃÊÄÚ¶ó¶¼") || selectedCoffeeType.Contains("µþ±â¶ó¶¼") || selectedCoffeeType.Contains("³ìÂ÷¶ó¶¼"))
            {
                setInfo("size", "Large", optionButtons.Take(4).ToArray(), 1, 0, 2);
                index = 1;
            }
            else
            {
                setInfo("size", "Large", optionButtons.Take(4).ToArray(), 3, 2, 4);
                index = 2;
            }
        }
        optionTotalTexts[index].text = (baseprice * count).ToString();
        restrictclickLarge = true;
    }

    public void getName()
    {
        selectedCoffeeType = EventSystem.current.currentSelectedGameObject.name;
        baseprice = GetItemPrice(selectedCoffeeType, items);
        if (selectedCoffeeType.Contains("HOT"))
            selectedTemperature = "HOT";
        else
            selectedTemperature = "ICE";

        int index = UIManager.difficulty - 1;
        if (selectedCoffeeType.Contains("ÄÉÀÌÅ©") || UIManager.difficulty == 1) index = 0;
        else if (selectedCoffeeType.Contains("ÃÊÄÚ¶ó¶¼") || selectedCoffeeType.Contains("µþ±â¶ó¶¼") || selectedCoffeeType.Contains("³ìÂ÷¶ó¶¼")) index = 1;

        optionTotalTexts[index].text = (int.Parse(optionTotalTexts[index].text) + baseprice).ToString();
        nameTexts[index].text = selectedCoffeeType;

        if(index == 0) UIManager.Instance.openDifficultyOption(UIManager.Instance.easyOption);
        else if (index == 1) UIManager.Instance.openDifficultyOption(UIManager.Instance.mediumOption);
        else UIManager.Instance.openDifficultyOption(UIManager.Instance.diffucltOption);
    }

    int GetItemPrice(string itemName, Dictionary<string, int> items)
    {
        if (items.ContainsKey(itemName))
        {
            return items[itemName];
        }
        else
        {
            return 0; 
        }
    }
    public void decaffeine()
    {
        if (!restrictclickdecaff)
        {
            setInfo("caffeine", "µðÄ«ÆäÀÎ", optionButtons.Take(7).ToArray(), 6,4,7);
            clickdecaff = true;
            baseprice += 300;
            optionTotalTexts[2].text = (baseprice * count).ToString();
            restrictclickdecaff = true;
        }
    }

    public void cocoacaffeine()
    {
        restrictclickdecaff = false;
        setInfo("caffeine", "ÄÚÄÚ¾ÆÇâ", optionButtons.Take(7).ToArray(), 4,4,7);
        if (clickdecaff)
        {
            baseprice -= 300;
            optionTotalTexts[2].text = (baseprice * count).ToString();
            clickdecaff = false;
        }
    }

    public void fruitcaffeine()
    {
        int newTotalPrice = baseprice * count;
        restrictclickdecaff = false;
        setInfo("caffeine", "°úÀÏÇâ", optionButtons.Take(7).ToArray(), 5,4,7);
        if (clickdecaff)
        {
            baseprice -= 300;
            clickdecaff = false;
            optionTotalTexts[2].text = (baseprice * count).ToString();
        }
    }

    public void oneshot()
    {
        restrictclicktwoshot = false;
        setInfo("shot", "1shot", optionButtons.Take(9).ToArray(), 7,7,9);
        if (clicktwoshot)
        {
            clicktwoshot = false;
            baseprice -= 500;
            optionTotalTexts[2].text = (baseprice * count).ToString();
        }
    }

    public void twoshot()
    {
        clicktwoshot = true;
        setInfo("shot", "2shot", optionButtons.Take(9).ToArray(), 8,7,9);
        if (!restrictclicktwoshot)
        {
            baseprice += 500;
            optionTotalTexts[2].text = (baseprice * count).ToString();
            restrictclicktwoshot = true;
        }
    }

    public void resetOption()
    {
        selectedTemperature = "";
        selectedCoffeeType = "";
        selectedSize = "";
        count = 1;
        for (int i = 0; i < optionCountTexts.Length; i++)
        {
            optionCountTexts[i].text = count.ToString();
            optionTotalTexts[i].text = "0";
        }
        baseprice = 0;
        resetButton();
        restrictclicktwoshot = false;
        restrictclickdecaff = false;
        restrictclickLarge = false;
        clickdecaff = false;
        clickLarge = false;
        clicktwoshot = false;
    }

    public void addToOrder()
    {
        selectedIndex++;

        if (orders.Count >= 2)
        {
            UIManager.Instance.fullOrder.SetActive(true);
            return;
        }
        // ÁÖ¹® Á¤º¸¸¦ »ý¼º
        cafeOrderElement newOrder = new cafeOrderElement();
        newOrder.temperature = selectedTemperature;
        newOrder.coffeeType = selectedCoffeeType;
        newOrder.quantity = count;
        newOrder.size = selectedSize;
        newOrder.shot = selectedShot;
        newOrder.coffebean = selectedCaffeine;
        newOrder.index = selectedIndex;

        // ÁÖ¹®À» ¸®½ºÆ®¿¡ Ãß°¡
        orders.Add(newOrder);
        resetOption();
        resetButton();

        // ÁÖ¹® È®ÀÎ ·ÎÁ÷ È£Ãâ
        AddQuestToGrid(newOrder);
        UIManager.Instance.closeDifficultyOption(UIManager.Instance.easyOption);
        UIManager.Instance.closeDifficultyOption(UIManager.Instance.mediumOption);
        UIManager.Instance.closeDifficultyOption(UIManager.Instance.diffucltOption);
    }
    public void deleteOrder(int index)
    {
        orders.RemoveAll(order => order.index == index);
    }
    void AddQuestToGrid(cafeOrderElement order)
    {
        GameObject questObject = Instantiate(orderPrefab, gridLayoutGroup.transform);
        cancelOrder questIndex = questObject.GetComponent<cancelOrder>();
        if (questIndex != null)
            questIndex.SetCellIndex(selectedIndex);
        if (order.coffeeType.Contains("ÄÉÀÌÅ©"))
        {
            TMP_Text detailsText = questObject.GetComponentsInChildren<TMP_Text>()[0];
            detailsText.text = $"{order.coffeeType}";

            TMP_Text itemText = questObject.GetComponentsInChildren<TMP_Text>()[1];
            itemText.text = $"- {order.quantity}°³";
        }
        else
        {
            TMP_Text detailsText = questObject.GetComponentsInChildren<TMP_Text>()[0];
            detailsText.text = $"{order.coffeeType}";

            TMP_Text itemText = questObject.GetComponentsInChildren<TMP_Text>()[1];
            if (UIManager.difficulty == 1)
                itemText.text = $"- {order.quantity}ÀÜ";
            if (UIManager.difficulty == 2)
                itemText.text = $"- {order.quantity}ÀÜ - {order.size}";
            if (UIManager.difficulty == 3)
            {
                if (order.coffeeType.Contains("ÃÊÄÚ¶ó¶¼") || order.coffeeType.Contains("µþ±â¶ó¶¼") || order.coffeeType.Contains("³ìÂ÷¶ó¶¼"))
                    itemText.text = $"- {order.size} - {order.quantity}ÀÜ";
                else
                    itemText.text = $"- {order.quantity}ÀÜ - {order.size} - {order.coffebean} - {order.shot}¹ø";
            }
        }
    }
    
}
