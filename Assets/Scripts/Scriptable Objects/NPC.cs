using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NPC : ScriptableObject
{

    public int money;
    public int bagSpace;
    public int hunger;

    public Item[] items;

    public Item[] inventory;

}
