using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{

    public TextMeshProUGUI[] itemText;

    public Item[] items;

    public GameObject inventory;


    private void Update()
    {
        for(int i = 0; i < items.Length; i++)
        {
            itemText[i].text = items[i].count + "/" + items[i].maxCount;
        }
    }

}
