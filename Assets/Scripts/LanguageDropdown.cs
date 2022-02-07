using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageDropdown : MonoBehaviour
{
    private TMPro.TMP_Dropdown dropdown;
    private Dictionary<int, string> languagesIndex;
    private Dictionary<string, int> codesLanguageIndex;

    void Awake()
    {
        dropdown = GetComponent<TMPro.TMP_Dropdown>();
        languagesIndex = new Dictionary<int, string>(){
            {0, "en"},
            {1, "es"}
        };
        codesLanguageIndex = new Dictionary<string, int>(){
            {"en", 0},
            {"es", 1}
        };
    }

    // Start is called before the first frame update
    void Start()
    {   
        dropdown.value = codesLanguageIndex[LocalizationSettings.SelectedLocale.Identifier.Code];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLanguage()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(languagesIndex[dropdown.value]);
    }
}
