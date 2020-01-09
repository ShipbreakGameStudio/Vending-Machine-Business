using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum VendingMachineState { on, off }

public class VendingMachine : MonoBehaviour
{

    [Header("Images")]
    public Image inventory;
    public Image pressE;
    public Image stateImage;

    [Header("TextMeshProUGUI")]
    public TextMeshProUGUI itemText0;
    public TextMeshProUGUI itemText1;
    public TextMeshProUGUI itemText2;

    public TextMeshProUGUI cashText;

    [Header("Sprites")]
    public Sprite onState;
    public Sprite offState;
    
    [Header("Item Handeling")]
    public Item[] items;
    public Inventory playerInventory;

    [Header("Player")]
    [HideInInspector] public Player player;
    bool playerInRange = false;

    [Header("Cash")]
    public CashSystem cash;

    [Header("State")]
    public VendingMachineState currentState;

    [Header("Buy")]
    public int cost;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        itemText0.text = items[0].count.ToString() + "/" + items[0].maxCount.ToString();
        itemText1.text = items[1].count.ToString() + "/" + items[1].maxCount.ToString();
        itemText2.text = items[2].count.ToString() + "/" + items[2].maxCount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerInRange = true;
            pressE.gameObject.SetActive(true); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInRange = false;
        pressE.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!cash.bought) { gameObject.SetActive(false); return; }
        UpdateItemText();
        if(player.currentState != PlayerState.interact && playerInRange)
        {
            if(Input.GetKeyDown(KeyCode.E)) {
                pressE.gameObject.SetActive(false);
                inventory.gameObject.SetActive(true);
                player.GetComponent<Player>().currentState = PlayerState.interact;
            }
        }

        if(player.currentState == PlayerState.interact && playerInRange)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                inventory.gameObject.SetActive(false);
                pressE.gameObject.SetActive(true);
                player.currentState = PlayerState.walk;
            }
        }
    }

    public void UpdateItemText()
    {
        itemText0.text = items[0].count.ToString() + "/" + items[0].maxCount.ToString();
        itemText1.text = items[1].count.ToString() + "/" + items[1].maxCount.ToString();
        itemText2.text = items[2].count.ToString() + "/" + items[2].maxCount.ToString();

        cashText.text = "Cash: " + cash.moneyInCash.ToString() + "$";
    }

    public void RefillItem(Item item)
    {
        if (currentState == VendingMachineState.off)
        {
            for (int i = 0; i < playerInventory.items.Length; i++)
            {
                if (playerInventory.items[i].itemName == item.itemName && playerInventory.items[i].count > 0)
                {
                    if (item.count < item.maxCount)
                    {
                        int wieVielNoch = item.maxCount - item.count;
                        int wieVielÜbrig = playerInventory.items[i].count - wieVielNoch;
                        if(wieVielÜbrig >= 0)
                        {
                            if(currentState == VendingMachineState.off)
                            {
                                playerInventory.items[i].count -= wieVielNoch;
                                StartCoroutine(RefillCoroutine(item));
                            }
                        }
                    }
                }
            }
        }
    }


    public void NewDay()
    {
        if (currentState == VendingMachineState.on)
        {
            int howMuchItems = Random.Range(1, 4);
            for (int i = 0; i < howMuchItems; i++)
            {
                Item whichItem = items[Random.Range(0, items.Length)];
                int howMuch = Random.Range(0, whichItem.count);
                whichItem.count -= howMuch;
                if ((cash.moneyInCash + whichItem.costs) + 1 < cash.maxMoneyInCash)
                {
                    cash.moneyInCash += whichItem.costs;
                }
            }
        }
        
    }

    public void ChangeState()
    {
        if(currentState == VendingMachineState.on)
        {
            stateImage.sprite = offState;
            currentState = VendingMachineState.off;
        } else
        {
            stateImage.sprite = onState;
            currentState = VendingMachineState.on;
        }

    }

    IEnumerator RefillCoroutine(Item item)
    {
        for(int i = item.count; i < item.maxCount; i++)
        {
            if(currentState == VendingMachineState.off)
            {
                item.count++;
                yield return new WaitForSeconds(0.675f);
            }
        }
    }
}
