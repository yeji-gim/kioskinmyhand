using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Base : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> objects = new Dictionary<Type, UnityEngine.Object[]>();
    
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type); // enum을 String으로 변환
        UnityEngine.Object[] objectstype = new UnityEngine.Object[names.Length]; // 오브젝트 타입 저장 공간 만들기
        objects.Add(typeof(T), objectstype);

        // 매핑시키기
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
            return null; // Object 클래스를 상속한 모든 클래스는 참조타입이기 때문에 null을 반환할 수 있다.

        foreach (T component in go.GetComponentsInChildren<T>())
        {
            if (string.IsNullOrEmpty(name) || component.name == name)
                return component;
        }

        return null;
    }

}
