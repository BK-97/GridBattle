using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum LevelType { Default, Tutorial }

[System.Serializable]
public class Level
{
    public Level(LevelData _levelData)
    {
        levelData = _levelData;
    }

    public List<LevelType> LevelTypes = new List<LevelType>();
    public string LoadLevelID;
    private LevelData levelData;


    #region EditorUtils

#if UNITY_EDITOR

    public List<string> LevelNames
    {
        get
        {
            List<string> levelNames = new List<string>();
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                var level = EditorBuildSettings.scenes[i];

                if (level.path.Contains("Level") || level.path.Contains("Test"))
                {

                    int slash = level.path.LastIndexOf('/');
                    string name = level.path.Substring(slash + 1);
                    name = name.Replace(".unity", string.Empty);
                    levelNames.Add(name);
                }
            }
            return levelNames;
        }
    }

    public Dictionary<string, string> LevelProperties
    {
        get
        {
            Dictionary<string, string> levelProperties = new Dictionary<string, string>();
            List<string> levelNames = new List<string>();
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                var level = EditorBuildSettings.scenes[i];

                if (level.path.Contains("Level") || level.path.Contains("Test"))
                {

                    int slash = level.path.LastIndexOf('/');
                    string name = level.path.Substring(slash + 1);
                    name = name.Replace(".unity", string.Empty);
                    levelNames.Add(name);
                    levelProperties.Add(name, level.path);
                }
            }
            return levelProperties;
        }
    }

#endif
    #endregion

    #region Inspector Visibility

    #endregion
    void SetLevelName()
    {

    }

}

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public List<Level> Levels = new List<Level>();


    private Level CreateLevel()
    {
        return new Level(this);
    }

    private void UpdateHyperGUI()
    {
        EventManager.OnLevelDataChange.Invoke();
    }
}