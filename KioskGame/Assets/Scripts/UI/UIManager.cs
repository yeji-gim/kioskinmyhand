using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [Header("places")]
    public GameObject cafe;
    public GameObject restaurant;
    [Header("opening")]
    public GameObject chooseDiffculty;
    public GameObject cafeorrestaurant;
    public GameObject cafeKiosk;
    public GameObject restuarantKiosk;
    [Header("menu")]
    public GameObject orderSheet;
    public GameObject icecoffee;
    public GameObject hotcoffee;
    public GameObject dessert;
    [Header("cafeQuest")]
    public GameObject cafeQuestPrefab;
    public GridLayoutGroup gridLayoutGroup;
    public static int difficulty;
    [Header("buttons")]
    public GameObject icebuttonon;
    public GameObject icebuttonoff;
    public GameObject hotbuttonon;
    public GameObject hotbuttonoff;
    public GameObject dessertbuttonon;
    public GameObject dessrtbuttonoff;
    [Header("cafeoption")]
    public GameObject easyOption;
    public GameObject mediumOption;
    public GameObject diffucltOption;
    [Header("burgeroption")]
    public GameObject burgereasyoption;
    public GameObject burgermediumoption;
    public GameObject burgerdiffucltoption;
    [Header("Popup")]
    public GameObject fullOrder;
    public GameObject victoryPopup;
    public GameObject failedPopup;
    public GameObject finishPopup;
    public GameObject readyPopup;
    [Header("burger")]
    public GameObject burger;
    public GameObject drinks;
    public GameObject side;
    public GameObject burgerbuttonon;
    public GameObject burgerbuttonoff;
    public GameObject sidetbuttonon;
    public GameObject sidebuttonoff;
    public GameObject drinkbuttonon;
    public GameObject drinkbuttonoff;
    [Header("finishButon")]
    public Button goInformationButton;
    public Button restartButton;
    Color purple = new Color(0.3568f, 0.1451f, 0.9529f);
    public string shop = "";
    public static int count = 0;
    public static float playTime = 0f;
    private void Start()
    {
        setMenu("ICE");
        burgersetMenu("BURGER");
    }
    private void Update()
    {
        playTime += Time.deltaTime;
    }
    public void openOrderSheet()
    {
        Debug.Log("open");
        orderSheet.SetActive(true);
    }
    public void openGamobject(GameObject gameObject)
    {
        gameObject.gameObject.SetActive(true);
    }

    public void closeGamObject(GameObject gameObject)
    {
        gameObject.gameObject.SetActive(false);
    }

    public void chooseshop(int i)
    {
        switch(i)
        {
            case 1:
                shop = "cafe";
                break;
            case 2:
                shop = "rest";
                break;

        }
    }
    public void closefullOrder()
    {
        fullOrder.gameObject.SetActive(false);
        if (shop == "cafe")
        {
            if (cafeOrder.Instance.selectedCoffeeType.Contains("케이크") || difficulty == 1)
                closeDifficultyOption(easyOption);
            else if (difficulty == 2)
                closeDifficultyOption(mediumOption);
            else if (difficulty == 3)
                closeDifficultyOption(diffucltOption);
        }
        if(shop=="rest")
        {
            if (difficulty == 1 || !burgerOrder.Instance.selectedItem.Contains("버거"))
                closeDifficultyOption(burgereasyoption);
            else if (difficulty == 2)
                closeDifficultyOption(burgermediumoption);
            else if (difficulty == 3)
                closeDifficultyOption(burgerdiffucltoption);
        }
    }

    public void resetMenu()
    {
        icecoffee.gameObject.SetActive(false);
        hotcoffee.gameObject.SetActive(false);
        dessert.gameObject.SetActive(false);
    }

    public void burgerresetMenu()
    {
        burger.gameObject.SetActive(false);
        side.gameObject.SetActive(false);
        drinks.gameObject.SetActive(false);
    }

    public void setMenu(string temperature)
    {
        resetMenu();
        switch (temperature)
        {
            case "ICE":
                buttonActive(icecoffee,icebuttonon, icebuttonoff);
                buttonsInactive(hotbuttonon, hotbuttonoff);
                buttonsInactive(dessertbuttonon, dessrtbuttonoff);
                break;
            case "HOT":
                buttonActive(hotcoffee,hotbuttonon, hotbuttonoff);
                buttonsInactive(icebuttonon, icebuttonoff);
                buttonsInactive(dessertbuttonon, dessrtbuttonoff);
                break;
            case "DESSERT":
                buttonActive(dessert,dessertbuttonon, dessrtbuttonoff);
                buttonsInactive(hotbuttonon, hotbuttonoff);
                buttonsInactive(icebuttonon, icebuttonoff);
                break;
        }
        cafeOrder.temperature = temperature;
    }
    public void burgersetMenu(string type)
    {
        burgerresetMenu();
        switch (type)
        {
            case "BURGER":
                buttonActive(burger,burgerbuttonon, burgerbuttonoff);
                buttonsInactive(sidetbuttonon, sidebuttonoff);
                buttonsInactive(drinkbuttonon, drinkbuttonoff);
                break;
            case "SIDE":
                buttonActive(side,sidetbuttonon, sidebuttonoff);
                buttonsInactive(burgerbuttonon, burgerbuttonoff);
                buttonsInactive(drinkbuttonon, drinkbuttonoff);
                break;
            case "DRINKS":
                buttonActive(drinks,drinkbuttonon, drinkbuttonoff);
                buttonsInactive(burgerbuttonon, burgerbuttonoff);
                buttonsInactive(sidetbuttonon, sidebuttonoff);
                break;
        }
    }
    public void buttonActive(GameObject gameObject, GameObject onButton, GameObject offButton)
    {
        gameObject.SetActive(true);
        onButton.SetActive(true);
        offButton.SetActive(false);
    }

    public void buttonsInactive(GameObject onButton, GameObject offButton)
    {
        onButton.SetActive(false);
        offButton.SetActive(true);
    }

    public void resetKiosk()
    {
        cafe.gameObject.SetActive(false);
        restaurant.gameObject.SetActive(false);
    }

    public void chooseDiffcultyoption(int level)
    {
        difficulty = level;
        if (shop == "cafe")
            questGenerator.Instance.GeneratecafeQuests();
        if (shop == "rest")
            questGenerator.Instance.GenerateburgerQuests();
    }
    public void openDifficultyOption(GameObject optionPanel)
    {
        optionPanel.SetActive(true);
    }

    public void closeDifficultyOption(GameObject optionPanel)
    {
        optionPanel.SetActive(false);
        if (shop == "cafe")
            cafeOrder.Instance.resetOption();
        else if (shop == "rest")
            burgerOrder.Instance.resetOption();
    }

    public void openKioskmenu()
    {
        orderSheet.gameObject.SetActive(false);
        if (shop == "cafe")
            cafeKiosk.gameObject.SetActive(true);
        if (shop == "rest")
            restuarantKiosk.gameObject.SetActive(true);
    }
    public void opencafeOption()
    {
        if (difficulty == 1 || cafeOrder.Instance.selectedCoffeeType.Contains("케이크"))
            openDifficultyOption(easyOption);
        else if (difficulty == 2)
            openDifficultyOption(mediumOption);
        else 
            openDifficultyOption(diffucltOption);
    }
    public void openburgerOption()
    {
        if (difficulty == 1 || !burgerOrder.Instance.selectedItem.Contains("버거"))
            openDifficultyOption(burgereasyoption);
        else if (difficulty == 2)
            openDifficultyOption(burgermediumoption);
        else 
            openDifficultyOption(burgerdiffucltoption);
    }
    public void cafesubmit()
    {
        finishPopup.gameObject.SetActive(true);
        if(questGenerator.Instance.cafequestsEqual(questGenerator.Instance.cafequestElements, cafeOrder.Instance.orders))
        {
            victoryPopup.gameObject.SetActive(true);
            failedPopup.gameObject.SetActive(false);
        }
        else
        {
            failedPopup.gameObject.SetActive(true);
            victoryPopup.gameObject.SetActive(false);
        }
    }

    public void burgersubmit()
    {
        finishPopup.gameObject.SetActive(true);
        if (questGenerator.Instance.burgerquestsEqual(questGenerator.Instance.burgerquestElements, burgerOrder.Instance.orders))
        {
            victoryPopup.gameObject.SetActive(true);
            failedPopup.gameObject.SetActive(false);
        }
        else
        {
            failedPopup.gameObject.SetActive(true);
            victoryPopup.gameObject.SetActive(false);
        }
    }

    public void resetCafe()
    {
        resetKiosk();
        resetMenu();
        clearOrderGrid(cafeOrder.Instance.gridLayoutGroup);
        cafeOrder.Instance.resetOption();
        finishPopup.gameObject.SetActive(false);
        cafeKiosk.gameObject.SetActive(false);
        cafeOrder.Instance.orders = new List<cafeOrderElement>();
    }

    public void resetBurger()
    {
        resetKiosk();
        burgerresetMenu();
        clearOrderGrid(burgerOrder.Instance.gridLayoutGroup);
        burgerOrder.Instance.resetOption();
        finishPopup.gameObject.SetActive(false);
        restuarantKiosk.gameObject.SetActive(false);
        burgerOrder.Instance.orders = new List<burgerOrderElement>();
    }

    public void finishButton(string shopName)
    {
        count++;

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (count > 1 && playTime >= 10f)
                admob.Instance.ShowAd();
        }
        if(shop == "cafe")
        {
            resetCafe();
            setMenu("ICE");
        }
        if(shop == "rest")
        {
            resetBurger();
            burgersetMenu("BURGER");
        }
    }
    public void goinformation()
    {
        finishButton(shop);
        cafe.gameObject.SetActive(true);
        restaurant.gameObject.SetActive(true);
        chooseDiffculty.gameObject.SetActive(false);
        cafeorrestaurant.gameObject.SetActive(true);
    }

    public void retryButton()
    {
        finishButton(shop);
        cafe.gameObject.SetActive(false);
        restaurant.gameObject.SetActive(false);
        chooseDiffculty.gameObject.SetActive(true);
    }
    public void clearOrderGrid(GridLayoutGroup gridLayoutGroup)
    {
        foreach (Transform child in gridLayoutGroup.transform)
            Destroy(child.gameObject);

    }
    public void changeColorButton(Button button, Color color)
    {
        Image image = button.image;
        image.color = color;
    }
    
}
