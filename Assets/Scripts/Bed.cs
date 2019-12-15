using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bed : MonoBehaviour
{

    public Animator fadeAnimator;
    public Image fade;

    public DayHandler dayHandler;

    public NPC[] npcs;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

            int howMuchNPCsThisNight = Random.Range(1, npcs.Length);

            for(int i = howMuchNPCsThisNight; i < npcs.Length; i++)
            {
                int howMuchBuy = Random.Range(1, npcs[i].bagSpace - 1);
                Item whatItem = npcs[i].items[Random.Range(0, npcs[i].items.Length)];
                if(npcs[i].money >= whatItem.costs && npcs[i].bagSpace > 0)
                {
                    npcs[i].bagSpace -= howMuchBuy;
                    npcs[i].hunger -= 10;
                    Debug.Log(npcs[i].bagSpace + " " + npcs[i].items);
                } else 
                {
                    Debug.Log("Error");
                    
                }
            }

            Player player = collision.GetComponent<Player>();
            player.currentState = PlayerState.interact;
            StartCoroutine(FadeCoroutine(player));
        }
    }

    IEnumerator FadeCoroutine(Player player)
    {
        dayHandler.day += 1;
        fade.gameObject.SetActive(true);
        fadeAnimator.SetBool("fadeIn", true);
        yield return new WaitForSeconds(3.5f);
        fadeAnimator.SetBool("fadeOut", true);
        player.currentState = PlayerState.walk;
        yield return new WaitForSeconds(3.5f);
        fadeAnimator.SetBool("fadeOut", false);
        fade.gameObject.SetActive(false);
    }

}
