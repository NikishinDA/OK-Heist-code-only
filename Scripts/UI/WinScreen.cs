using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private Image skinProgressBar;
    [SerializeField] private float progressPerLevel = 0.4f;
    [SerializeField] private Button nextButton;
    
    [SerializeField] private Text moneyText;
    [SerializeField] private int moneyPerLevel;
    private int _skinNumber;
    [SerializeField] private Image pbBackground;
    [SerializeField] private Image pbImage;
    
    [SerializeField] private Sprite[] skinsBackgrounds;
    [SerializeField] private Sprite[] skinsSprites;

    [SerializeField] private Image stolenImage;
    [SerializeField] private Sprite[] stolenSprites;
    [SerializeField] private int[] safeLevels;
    [SerializeField] private int[] paintingLevels;
    [SerializeField] private int[] diamondLevels;
    private void Awake()
    {
        nextButton.onClick.AddListener(OnNextButtonClick);
        _skinNumber = PlayerPrefs.GetInt(PlayerPrefsStrings.SkinNumber.Name, PlayerPrefsStrings.SkinNumber.DefaultValue);
        if (_skinNumber < 0)
        {
            
            pbBackground.sprite = skinsBackgrounds[skinsBackgrounds.Length - 1];
            pbImage.sprite = skinsSprites[skinsSprites.Length-1];
        }
        else
        {
            
            pbBackground.sprite = skinsBackgrounds[_skinNumber];
            pbImage.sprite = skinsSprites[_skinNumber];
        }
    } 
    private void Start()
    {
        moneyText.text = "0";
        int level = PlayerPrefs.GetInt(PlayerPrefsStrings.Level.Name, PlayerPrefsStrings.Level.DefaultValue) 
                    % SceneManager.sceneCountInBuildSettings;
        if (safeLevels.Contains(level))
        {
            stolenImage.sprite = stolenSprites[0];
            stolenImage.SetNativeSize();
        }
        else if (paintingLevels.Contains(level))
        {
            stolenImage.sprite = stolenSprites[1];
            stolenImage.SetNativeSize();
        }
        else if (diamondLevels.Contains(level))
        {
            stolenImage.sprite = stolenSprites[2];
            stolenImage.SetNativeSize();
        }
        StartCoroutine(SkinProgress(2f , 1f));
        StartCoroutine(MoneyCounter(1f));
    }
    private IEnumerator MoneyCounter(float time)
    {
        WaitForSeconds wait = new WaitForSeconds(.5f);
        yield return wait;
        int money = 0;
        int endMoney = moneyPerLevel;
        for (float t = 0 ; t < time; t += Time.deltaTime)
        {
            money = (int) Mathf.Lerp(0, endMoney, t / time);
            moneyText.text = money.ToString();
            yield return null;
        }
        moneyText.text = endMoney.ToString();
        yield return wait;
        for (float t = 0 ; t < time; t += Time.deltaTime)
        {
            money = (int) Mathf.Lerp(endMoney, 0, t / time);
            moneyText.text = money.ToString();
            yield return null;
        }
        moneyText.text = "0";
    }
    private  IEnumerator SkinProgress(float waitTime, float time)
    {        

        float weaponProgress = PlayerPrefs.GetFloat(PlayerPrefsStrings.SkinProgress.Name,
            PlayerPrefsStrings.SkinProgress.DefaultValue);
        float endProgress = weaponProgress + progressPerLevel;
        if (endProgress >= 1)
        {
            endProgress = 1;
            PlayerPrefs.SetFloat(PlayerPrefsStrings.SkinProgress.Name, 0);
            _skinNumber++;
            _skinNumber %= skinsSprites.Length;
            PlayerPrefs.SetInt(PlayerPrefsStrings.SkinNumber.Name, _skinNumber);
        }
        else
        {
            PlayerPrefs.SetFloat(PlayerPrefsStrings.SkinProgress.Name, endProgress);
        }

        skinProgressBar.fillAmount = weaponProgress;
        yield return new WaitForSeconds(waitTime);
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            skinProgressBar.fillAmount = Mathf.Lerp(weaponProgress, endProgress, t / time);
            yield return null;
        }
    }

    private void OnNextButtonClick()
    {
        SceneLoader.LoadNextLevel();
    }
}
