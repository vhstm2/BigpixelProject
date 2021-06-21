using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_item : MonoBehaviour
{
    public int ItemNumber;
    public Button ItemButton;
    public GameObject Check;

    public void Click()
    {
        Item_Select.ItemSelect_idx = ItemNumber;
    }
}