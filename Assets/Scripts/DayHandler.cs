using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Season
{
    spring,
    summer,
    fall,
    winter
}

public class DayHandler : MonoBehaviour
{
    public static DayHandler Instance { get; set; }

    public Season currentSeason;

    public int day;

    private void Awake()
    {
        DontDestroyOnLoad(this);

    }

    private void Update()
    {
        switch (day)
        {
            case 0:
                currentSeason = Season.spring;
                break;
            case 30:
                currentSeason = Season.summer;
                break;
            case 60:
                currentSeason = Season.fall;
                break;
            case 90:
                currentSeason = Season.winter;
                break;
            case 120:
                day = 0;
                currentSeason = Season.spring;
                break;
        }
    }

}
