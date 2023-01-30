using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPolicy : MonoBehaviour
{
    [SerializeField] private GameObject PolicyPanel;

    public void OpenAchievmentPanel(bool status)
    {
        PolicyPanel.SetActive(status);
    }
}
