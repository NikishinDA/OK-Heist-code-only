using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefModelManager : MonoBehaviour
{
    [SerializeField] private ThiefAnimationController[] skins;
    private ThiefAgentController _agentController;

    private void Awake()
    {
        _agentController = GetComponent<ThiefAgentController>();
        int skinNumber =
            PlayerPrefs.GetInt(PlayerPrefsStrings.SkinNumber.Name, PlayerPrefsStrings.SkinNumber.DefaultValue);
        if (skinNumber < 0)
        {
            Instantiate(skins[skins.Length-1], transform).Initialize(_agentController);
 
        }
        else
        {
            Instantiate(skins[skinNumber], transform).Initialize(_agentController);
        }
    }
}