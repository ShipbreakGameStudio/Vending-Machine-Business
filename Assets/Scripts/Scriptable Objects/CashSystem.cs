using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Has to be for an own machine! Every machine needs one of these!
 */

[CreateAssetMenu]
public class CashSystem : ScriptableObject
{

    public int moneyInCash;

    public int maxMoneyInCash;

    public bool bought;

}
