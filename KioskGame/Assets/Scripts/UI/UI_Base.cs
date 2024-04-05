using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Base : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> objects = new Dictionary<Type, UnityEngine.Object[]>();
    
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type); // enum�� String���� ��ȯ
        UnityEngine.Object[] objectstype = new UnityEngine.Object[names.Length]; // ������Ʈ Ÿ�� ���� ���� �����
        objects.Add(typeof(T), objectstype);

        // ���ν�Ű��
        for (int i = 0; i < names.Length; i++)
        {
            objectstype[i] = FindChild<T>(gameObject, names[i]);
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objectstype = null;
        if (objects.TryGetValue(typeof(T), out objectstype) == false)
            return null;
        return objectstype[idx] as T;
    }

    T FindChild<T>(GameObject go, string name) where T : UnityEngine.Object
    {
        if (go == null)
            return null; // Object Ŭ������ ����� ��� Ŭ������ ����Ÿ���̱� ������ null�� ��ȯ�� �� �ִ�.

        foreach (T component in go.GetComponentsInChildren<T>())
        {
            if (string.IsNullOrEmpty(name) || component.name == name)
                return component;
        }

        return null;
    }

}
