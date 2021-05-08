using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuDriver : MonoBehaviour
{
    public Slider difficultySlider;
    public Toggle soundToggle;
    public Toggle musicToggle;
    public Dropdown levelDropdown;

    public void Start()
    {
        DirectoryInfo di = new DirectoryInfo(Application.streamingAssetsPath + "/Levels/");
        FileInfo[] files = di.GetFiles("*.txt");
        List<string> fnames = new List<string>();

        foreach(FileInfo file in files)
        {
            fnames.Add(file.Name);
        }

        levelDropdown.ClearOptions();
        levelDropdown.AddOptions(fnames);
        levelDropdown.RefreshShownValue();

        PlayerPrefs.SetInt("difficulty", (int)difficultySlider.value);
        PlayerPrefs.SetInt("sound", 1);
        PlayerPrefs.SetInt("music", 1);
        PlayerPrefs.SetString("level", levelDropdown.options[levelDropdown.value].text);
    }

    // Start is called before the first frame update
    public void SetDifficulty()
    {
        PlayerPrefs.SetInt("difficulty", (int)difficultySlider.value);
    }

    public void SetSound()
    {
        if (soundToggle.isOn)
        {
            PlayerPrefs.SetInt("sound", 1);
        }
        else
        {
            PlayerPrefs.SetInt("sound", 0);
        }
    }

    public void SetMusic()
    {
        if (musicToggle.isOn)
        {
            PlayerPrefs.SetInt("music", 1);
        }
        else
        {
            PlayerPrefs.SetInt("music", 0);
        }
    }

    public void SetLevel()
    {
        PlayerPrefs.SetString("level", levelDropdown.options[levelDropdown.value].text);
    }
}
