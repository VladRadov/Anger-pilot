using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EducationView : MonoBehaviour
{
    [SerializeField] private Mask _mask;
    [SerializeField] private Image _helpTextOne;
    [SerializeField] private Image _helpTextTwo;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    public void SetActiveMask(bool value)
    {
        _mask.gameObject.SetActive(!value);
        _mask.showMaskGraphic = value;
        Debug.Log("Mask: " + value);
    }

    public void OnCloseEducation()
    {
        if (_helpTextOne.gameObject.activeSelf)
            _helpTextOne.gameObject.SetActive(false);
        else
            SetActive(false);
    }
}
