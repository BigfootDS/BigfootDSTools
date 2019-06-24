using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public enum DesiredDayLengthInMinutes {
    Five_minutes = 5,           // gameCustomTimescale = 288 // game day lasts 300 seconds
    Ten_minutes = 10,           // gameCustomTimescale = 144 // game day lasts 600 seconds
    Fifteen_minutes = 15,       // gameCustomTimescale = 96 // game day lasts 900 seconds
    Twentyfour_minutes = 24,    // gameCustomTimescale = 60; // game day lasts 1440 seconds
    Half_hour = 30,             // gameCustomTimescale = 48; // game day lasts 1800 seconds
    One_hour = 60,              // gameCustomTimescale = 24; // game day lasts 3600 seconds
    Two_hours = 120,            // gameCustomTimescale = 12; // game day lasts 7200 seconds
    Four_hours = 240,           // gameCustomTimescale = 6; // game day lasts 14400 seconds

};

[Serializable]
public class DayLengthWithScales
{
    public int realtimeMinuteDuration;
    public int gameCustomTimescale;
}

public class GameTimeManager : MonoBehaviour
{

    [Header("Time References")]     // --------------------------------------------------------------
    /// <summary>
    /// gameClockMessage is the key output from the GameTimeManager system. Feed it into your UI/etc to display it in the game.
    /// </summary>
    public string gameClockMessage;
    public DayLengthWithScales[] dayLengthWithScales;
    public DesiredDayLengthInMinutes desiredDayLength;
    [Header("Current Time")]
    public int day = 0;
    [RangeAttribute(0, 23)] public int hours = 0;
    [RangeAttribute(0, 59)] public int minutes = 0;
    [RangeAttribute(0, 59)] public float seconds = 0;
    public float gameCustomTimescale = 5;
    [RangeAttribute(0, 86400)] public float totalSecondsInCurrentDay = 0;
    public int currentDayTotalSeconds;
    [RangeAttribute(0, 1)] public float percentageOfCurrentDay = 0;
    public List<float> debugDayLengthRealtime;


    [Header("Day Cycle")]           // --------------------------------------------------------------
    public Transform solarContainer;
    public Color sunColour;
    public Color moonColour;
    public Vector3 targetSunRot = new Vector3(360, 0, 0);


    // --------------------------------------------------------------------------------------

    void Start ()
    {
        ChangeDesiredDayLengthEnum(DesiredDayLengthInMinutes.Twentyfour_minutes);
        StartCoroutine("GameClock");        
    }

    public void ChangeDesiredDayLengthEnum (DesiredDayLengthInMinutes newDayLengthSelection)
    {
        desiredDayLength = newDayLengthSelection;
        SetGameCustomTimescale();
    }

    [ContextMenu("Day Length 5 Minutes")]
    public void SetDayLengthFiveMinutes ()
    {
        desiredDayLength = DesiredDayLengthInMinutes.Five_minutes;
        SetGameCustomTimescale();
    }

    [ContextMenu("Day Length 10 Minutes")]
    public void SetDayLengthTenMinutes()
    {
        desiredDayLength = DesiredDayLengthInMinutes.Ten_minutes;
        SetGameCustomTimescale();
    }

    [ContextMenu("Day Length 15 Minutes")]
    public void SetDayLengthFifteenMinutes()
    {
        desiredDayLength = DesiredDayLengthInMinutes.Fifteen_minutes;
        SetGameCustomTimescale();
    }

    [ContextMenu("Day Length 24 Minutes")]
    public void SetDayLengthTwentyfourMinutes()
    {
        desiredDayLength = DesiredDayLengthInMinutes.Twentyfour_minutes;
        SetGameCustomTimescale();
    }

    [ContextMenu("Day Length 30 Minutes")]
    public void SetDayLengthThirtyMinutes()
    {
        desiredDayLength = DesiredDayLengthInMinutes.Half_hour;
        SetGameCustomTimescale();
    }


    [ContextMenu("Day Length 1 Hour")]
    public void SetDayLengthOneHour()
    {
        desiredDayLength = DesiredDayLengthInMinutes.One_hour;
        SetGameCustomTimescale();
    }

    [ContextMenu("Day Length 2 Hours")]
    public void SetDayLengthTwoHours()
    {
        desiredDayLength = DesiredDayLengthInMinutes.Two_hours;
        SetGameCustomTimescale();
    }

    [ContextMenu("Day Length 4 Hours")]
    public void SetDayLengthFourHours()
    {
        desiredDayLength = DesiredDayLengthInMinutes.Four_hours;
        SetGameCustomTimescale();
    }

    [ContextMenu("Day Length From DesiredDayLength")]
    public void SetGameCustomTimescale ()
    {
        int customTimescaleTemp = (int)desiredDayLength;
        Debug.Log($"The new day duration is about to become {customTimescaleTemp} minutes.");
        foreach (DayLengthWithScales dayLenghtdata in dayLengthWithScales)
        {
            print ("Debug looping through day length datat now");
            if (customTimescaleTemp == dayLenghtdata.realtimeMinuteDuration)
            {
                gameCustomTimescale = dayLenghtdata.gameCustomTimescale;
                Debug.Log($"The game's timescale is now {gameCustomTimescale}");
            }
        }
    }

    /// <summary>
    /// Takes a time as string and sets it as the game's current time.
    /// </summary>
    public void StringToGameTime (string desiredTime){
        if (DateTime.TryParse(desiredTime, out DateTime result)){
            Debug.Log($"Converted the provided string of ({desiredTime}) to a DateTime with {result.Second} seconds.");
            totalSecondsInCurrentDay = result.Second;
            
        } else {
            Debug.Log("Woops, the provided time could not be parsed as a suitable new time for the game clock. Please try calling StringToGameTime(stringo) but provide stringo as a different format. Thanks.");
        }

    }



    [ContextMenu("Set time to 9 AM")]
    public void SetGameTimeTo9AM () {
        StringToGameTime("9:00:00");
    }








    // Core loop of the clock system. All you have to do is change the values that get fed into it.
    public IEnumerator GameClock ()
    {
        while (true)
        {
            // Track game time while playing
            totalSecondsInCurrentDay += Time.deltaTime * gameCustomTimescale;
            if (percentageOfCurrentDay >= 1 || totalSecondsInCurrentDay >= 86400)
            {
                debugDayLengthRealtime.Add(Time.timeSinceLevelLoad);
                day++;
                totalSecondsInCurrentDay = 0;
                percentageOfCurrentDay = 0;
            }

            // Figure out how far into the day we currently are
            percentageOfCurrentDay = (totalSecondsInCurrentDay / 86400);

            // Rotate the sun based on the day's progression
            solarContainer.localEulerAngles = Vector3.Lerp(Vector3.zero, targetSunRot, percentageOfCurrentDay);
            
            // Convert the day's progression into time usable for a 24hour clock
            currentDayTotalSeconds = Mathf.RoundToInt(totalSecondsInCurrentDay);
            seconds = currentDayTotalSeconds % 60;
            minutes = (currentDayTotalSeconds / 60) % 60;
            hours = (currentDayTotalSeconds / 3600) % 24;


            // Display clock time in UI
            // Old way:
            //string = "Day " + day + ": " + hours.ToString("D2") + minutes.ToString("D2");
            // New way:
            //string = $"Day {day}: {hours.ToString("D2")}:{minutes.ToString("D2")}";
            gameClockMessage = $"Day {day}: {hours.ToString("D2")}:{minutes.ToString("D2")}";
            yield return null;
        }
    }



}
