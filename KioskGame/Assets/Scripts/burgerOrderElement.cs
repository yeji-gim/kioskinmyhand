using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burgerOrderElement
{
    public string type;  // ex: "단품" "세트"
    public string item;  // ex: "비프버거", "치킨버거", "슈림프버거" "계란버거" "비프앤슈림프버거" "더블비프버거"등
    public string drink;  // ex: "콜라" "사이다" "오렌지쥬스" "물" "아메리카노(HOT)" "아메리카노(ICE)"
    public string side; // ex : "치킨너겟" "초코아이스크림" "소프트아이스크림" "감자튀김" "치즈스틱"
    public string details; // ex : "양파빼고" "피클빼고" "없음"
    public int quantity;
    public int index;
}
