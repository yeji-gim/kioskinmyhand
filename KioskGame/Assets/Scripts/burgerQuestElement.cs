using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burgerQuestElement
{
    public string type; 
    public string item;  
    public string drink;  
    public string side; 
    public string details; 
    public int quantity; 

    public burgerQuestElement()
    {
        if (UIManager.difficulty == 1)
        {
            this.item = GetRandomBurger();
            this.quantity = GetRandomQuantity();
        }
        if (UIManager.difficulty == 2)
        {
            this.item = GetRandomItem();
            this.quantity = GetRandomQuantity();
            this.details = GetRandomDetails();
        }
        if (UIManager.difficulty == 3)
        {
            this.type = GetRandomType();
            this.item = GetRandomBurger();
            this.quantity = GetRandomQuantity();
            this.side = GetSetSide();
            this.drink = GetRandomDrink();
        }
    }
    public bool Equals(burgerOrderElement order)
    {
        if (UIManager.difficulty == 1 || !order.item.Contains("버거"))
        {
            return item == order.item &&
                   quantity == order.quantity;
        }
        else if (UIManager.difficulty == 2)
        {
            return quantity == order.quantity &&
                       item == order.item &&
                       details == order.details;
        }
        else if (UIManager.difficulty == 3)
        {
            if (order.type.Contains("단품"))
            {
                return item == order.item &&
                       type == order.type &&
                       quantity == order.quantity;
            }
            else
            {
                return quantity == order.quantity &&
                       item == order.item &&
                       type == order.type &&
                       drink == order.drink &&
                       side == order.side;
            }
        }
        return false;
    }
    int GetRandomQuantity()
    {
        return Random.Range(1, 3);
    }
    string GetRandomType()
    {
        return (Random.Range(0, 2) == 0) ? "단품" : "세트";
    }
    string GetRandomItem()
    {
        string[] items = { "비프버거", "치킨버거", "슈림프버거", "애그버거", "비앤슈버거", "더블비프버거", "치킨너겟", "에그타르트", "아이스크림", "감자튀김", "치즈스틱", "통다리치킨" };
        return items[Random.Range(0, items.Length)];
    }

    string GetRandomBurger()
    {
        string[] items = { "비프버거", "치킨버거", "슈림프버거", "애그버거", "비앤슈버거", "더블비프버거" };
        return items[Random.Range(0, items.Length)];
    }
    string GetRandomDrink()
    {
        string[] items = { "콜라", "사이다", "오렌지주스", "생수"};
        return items[Random.Range(0, items.Length)];
    }

    string GetRandomSide()
    {
        string[] items = { "치킨너겟", "통다리치킨", "아이스크림", "감자튀김", "치즈스틱","에그타르트" };
        return items[Random.Range(0, items.Length)];
    }

    string GetRandomDetails()
    {
        string[] items = { "양파 빼고", "피클 빼고", "없음" };
        return items[Random.Range(0, items.Length)];
    }

    string GetSetSide()
    {
        string[] items = { "치즈스틱", "통다리치킨", "감자튀김" };
        return items[Random.Range(0, items.Length)];
    }
}
