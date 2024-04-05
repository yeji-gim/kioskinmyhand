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
        if (UIManager.difficulty == 1 || !order.item.Contains("����"))
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
            if (order.type.Contains("��ǰ"))
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
        return (Random.Range(0, 2) == 0) ? "��ǰ" : "��Ʈ";
    }
    string GetRandomItem()
    {
        string[] items = { "��������", "ġŲ����", "����������", "�ֱ׹���", "��ؽ�����", "�����������", "ġŲ�ʰ�", "����Ÿ��Ʈ", "���̽�ũ��", "����Ƣ��", "ġ�ƽ", "��ٸ�ġŲ" };
        return items[Random.Range(0, items.Length)];
    }

    string GetRandomBurger()
    {
        string[] items = { "��������", "ġŲ����", "����������", "�ֱ׹���", "��ؽ�����", "�����������" };
        return items[Random.Range(0, items.Length)];
    }
    string GetRandomDrink()
    {
        string[] items = { "�ݶ�", "���̴�", "�������ֽ�", "����"};
        return items[Random.Range(0, items.Length)];
    }

    string GetRandomSide()
    {
        string[] items = { "ġŲ�ʰ�", "��ٸ�ġŲ", "���̽�ũ��", "����Ƣ��", "ġ�ƽ","����Ÿ��Ʈ" };
        return items[Random.Range(0, items.Length)];
    }

    string GetRandomDetails()
    {
        string[] items = { "���� ����", "��Ŭ ����", "����" };
        return items[Random.Range(0, items.Length)];
    }

    string GetSetSide()
    {
        string[] items = { "ġ�ƽ", "��ٸ�ġŲ", "����Ƣ��" };
        return items[Random.Range(0, items.Length)];
    }
}
