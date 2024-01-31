using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPreferencesManager //THIS IS STATIC BECAUSE IT DOESN'T INVOLVE ANY INSTANCE SPECIFIC DATA
{
    private const string UnlockedLevelsKey = "UnlockedLevels"; //Constant Key To Access All Completed Levels
    private const string VolumeSettingsKey = "VolumeSetting"; //Constatn Key To Access Volume Setting
    //private const string BrightnessSettingsKey = "BrightnessSetting"; 

    private const float defaultVolume = .5f; //Default Volume Setting

    public static void MarkLevelCompleted(int levelNumber) //Responsible For Marking Level Completion
    {
        List<int> completedLevels = GetCompletedLevels();

        if(!completedLevels.Contains(levelNumber))
        {
            completedLevels.Add(levelNumber); //Adds Number To This List
            SaveCompletedLevels(completedLevels); //Saves Levels
        }
    }

    public static List<int> GetCompletedLevels() //Returns A List Of Completed Levels
    {
        string serializedLevels = PlayerPrefs.GetString("UnlockedLevels", ""); //Set The Existing Data To A Local String

        List<int> completedLevels = new List<int>(); //Create A New List To Hold Levels Declared

        if(!string.IsNullOrEmpty(serializedLevels)) //Check If The Existing Data Isn't Empty
        {
            string[] levelStrings = serializedLevels.Split(","); //Divides The String By Commas

            //Loop Through Every Number In Array
            foreach(string levelString in levelStrings) //For Each Character Divided By Commas, Parse To An Int
            {
                if(int.TryParse(levelString, out int level))
                {
                    completedLevels.Add(level); //Add To The List
                }
            }
        }

        return completedLevels; //Returns The Completed Levels Integer
    }

    private static void SaveCompletedLevels(List<int> completedLevels) //Responsible For Saving Levels
    {
        string serializedLevels = string.Join(",", completedLevels); //Rejoins The String And Seperates Them By Commas
        PlayerPrefs.SetString("UnlockedLevels", serializedLevels); //Sets The String To Numbers Rejoined
        PlayerPrefs.Save(); //Saves Preferences
    }

    private static void ResetPlayerInformation() //Resets Everything For Player To Start A New Game
    {
        List<int> completedLevels = GetCompletedLevels(); //Grabs All Completed Levels

        if(completedLevels != null || completedLevels.Count == 0) //If We 
        {
            completedLevels.Clear(); //Clears The List
        } else 
        {
            Debug.LogError("There Was No Information To Reset");
        }

        SaveCompletedLevels(completedLevels); //Saves The New List

        //Can Add Additional Keys To Reset In The Future
    }

    public static float GetVolumeSetting() //Grabs The Volume Setting Then Returns It
    {
        float volume = PlayerPrefs.GetFloat("VolumeSetting"); //Grabs The Value Using The Key
        return volume;
    }

    public static void SaveVolumeSetting(float volume) //Saves Player Volume Setting
    {
        Debug.Log("Updated Volume Setting");
        PlayerPrefs.SetFloat("VolumeSetting", volume);
        PlayerPrefs.Save();
    }

    public static void DefaultSettings() //Responsible For Default Settings For Each Setting
    {
        SaveVolumeSetting(defaultVolume); //Saves Volume Setting As Default Volume
    }
}
