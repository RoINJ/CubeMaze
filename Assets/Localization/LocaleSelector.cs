using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    public void SetLocale(int localeId)
    {
        StartCoroutine(ChangeLocale(localeId));
    }

    private IEnumerator ChangeLocale(int localeId)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeId];
    }
}
