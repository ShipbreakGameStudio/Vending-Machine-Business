using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Laptop : MonoBehaviour
{

    public Image pressE;

    Player player;

    public bool playerInRange;

    public GameObject shop;
    public GameObject laptop;
    public GameObject inventory;
    public GameObject jobs;

    public GameObject openShopButton;
    public GameObject openJobsButton;

    public Inventory playerInventory;

    public Animator fadeAnimator;
    public Image fade;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        InputHandler();
    }

    public void BuyItem(Item item)
    {
        if(playerInventory.money >= item.costs)
        {
            for(int i = 0; i < playerInventory.items.Length; i++)
            {
                if(playerInventory.items[i].itemName == item.itemName)
                {
                    if (playerInventory.items[i].count < playerInventory.items[i].maxCount)
                    {
                        playerInventory.items[i].count += 1;
                        playerInventory.money -= item.costs;
                    } else
                    {
                        //Kann nicht kaufen weil inventar voll
                    }
                }
            }
        }
        else
        {
            // Kann nicht kaufen weil kein Geld
        }
    }

    public void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (!laptop.activeInHierarchy)
            {
                //if(!inventory.activeInHierarchy)
                //{
                //    inventory.SetActive(true);
                //}

                shop.SetActive(false);
                jobs.SetActive(false);
                openShopButton.gameObject.SetActive(true);
                openJobsButton.gameObject.SetActive(true);
                pressE.gameObject.SetActive(false);
                laptop.SetActive(true);
                player.currentState = PlayerState.interact;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inventory.activeInHierarchy)
            {
                inventory.SetActive(false);
            }
            if (!laptop.activeInHierarchy)
            {
                laptop.SetActive(true);
            }
            if (jobs.activeInHierarchy)
            {
                laptop.SetActive(true);
                openJobsButton.gameObject.SetActive(true);
                if (inventory.activeInHierarchy)
                {
                    inventory.SetActive(false);
                }
                jobs.SetActive(false);
            }
            else
            {
                laptop.SetActive(false);
                player.currentState = PlayerState.walk;
                pressE.gameObject.SetActive(true);
            }
            if (shop.activeInHierarchy)
            {
                laptop.SetActive(true);
                openShopButton.gameObject.SetActive(true);
                if (inventory.activeInHierarchy)
                {
                    inventory.SetActive(false);
                }
                shop.SetActive(false);
            }
            else
            {
                laptop.SetActive(false);
                player.currentState = PlayerState.walk;
                pressE.gameObject.SetActive(true);
            }

        }


    }

    public void OpenShop()
    {
        if(!shop.activeInHierarchy)
        {
            shop.SetActive(true);
            inventory.SetActive(true);
            openShopButton.gameObject.SetActive(false);
            openJobsButton.gameObject.SetActive(false);
        }
    }

    public void OpenJobs()
    {
        if (!jobs.activeInHierarchy)
        {
            jobs.SetActive(true);
            openJobsButton.gameObject.SetActive(false);
            openShopButton.gameObject.SetActive(false);
        }
    }

    public void AcceptJob(Job job)
    {
        StartCoroutine(FadeCoroutine(3.5f, job));
    }

    IEnumerator FadeCoroutine(float waitTime, Job job)
    {
        fade.gameObject.SetActive(true);
        fadeAnimator.SetBool("fadeIn", true);
        yield return new WaitForSeconds(waitTime);
        laptop.SetActive(false);
        jobs.SetActive(false);
        shop.SetActive(false);
        fadeAnimator.SetBool("fadeOut", true);
        yield return new WaitForSeconds(waitTime);
        player.currentState = PlayerState.walk;
        fadeAnimator.SetBool("fadeOut", false);
        fade.gameObject.SetActive(false);
        playerInventory.money += job.moneyReward;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        playerInRange = true;
        pressE.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInRange = false;
        pressE.gameObject.SetActive(false);
    }

}
