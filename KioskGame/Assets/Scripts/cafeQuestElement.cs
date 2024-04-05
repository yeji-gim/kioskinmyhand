using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cafeQuestElement
{
    public string type;  
    public string item;  
    public string size;  
    public int quantity;  
    public string coffebean; 
    public int shot;

    public cafeQuestElement()
    {
        if (UIManager.difficulty == 1)
        {
            this.type = GetRandomType();
            this.item = GetRandomItem();
            this.quantity = GetRandomQuantity();
        }
        if (UIManager.difficulty == 2)
        {
            this.type = GetRandomType();
            this.item = GetRandomItem();
            this.quantity = GetRandomQuantity();
            this.size = GetRandomSize();
        }
        if (UIManager.difficulty == 3)
        {
            this.type = GetRandomType();
            this.item = GetRandomItem();
            this.quantity = GetRandomQuantity();
            this.size = GetRandomSize();
            this.coffebean = GetRandomcoffeBean();
            this.shot = GetShot();
        }
    }
    public cafeQuestElement(string type, string item, string size, int quantity)
    {
        this.type = type;
        this.item = item;
        this.quantity = quantity;
        this.size = size;
    }

    public cafeQuestElement(string type, string item, int quantity, string coffebean, int shot)
    {
        this.type = type;
        this.item = item;
        this.quantity = quantity;
        this.coffebean = coffebean;
        this.shot = shot;
    }
    public bool Equals(cafeOrderElement order)
    {
        string name = order.coffeeType.Split('(')[0];

        if (item.Contains("케이크"))
        {
            return item == order.coffeeType &&
            quantity == order.quantity;
        }
        else
        {
            if (UIManager.difficulty == 1)
            {
                
                return type == order.temperature &&
                       item == name &&
                       quantity == order.quantity;
            }
            else if (UIManager.difficulty == 2)
            {
                return type == order.temperature &&
                       item == name &&
                       size == order.size &&
                       quantity == order.quantity;
            }
            else if (UIManager.difficulty == 3)
            {
                if (item.Contains("초코라떼") || item.Contains("녹차라떼") || item.Contains("딸기라떼"))
                {
                    return type == order.temperature &&
                               item == name &&
                               quantity == order.quantity;
                }
                else
                {
                    return type == order.temperature &&
                           item == name &&
                           size == order.size &&
                           quantity == order.quantity &&
                           coffebean == order.coffebean &&
                           shot == order.shot;
                }
            }
        }
        return false;
    }

    string GetRandomType()
    {
        return (Random.Range(0, 2) == 0) ? "ICE" : "HOT";
    }

    string GetRandomItem()
    {
        string[] items = { "아메리카노", "카페라떼", "카푸치노", "초코라떼", "녹차라떼", "딸기라떼", "초코케이크", "딸기케이크", "녹차케이크" };
        return items[Random.Range(0, items.Length)];
    }

    int GetRandomQuantity()
    {
        return Random.Range(1, 3);
    }

    string GetRandomcoffeBean()
    {
        string[] items = { "디카페인", "과일향", "코코아향" };
        return items[Random.Range(0, items.Length)];
    }

    int GetShot()
    {
        return Random.Range(1, 2);
    }
    string GetRandomSize()
    {
        return (Random.Range(0, 2) == 0) ? "Regular" : "Large";
    }
}
