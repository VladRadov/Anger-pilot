using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EducationManager : MonoBehaviour
{
    [SerializeField] private EducationView _educationView;
    [SerializeField] private Button _closeEducation;

    private void Start()
    {
        OnStart();
    }

    private async void OnStart()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.IsEducation == 0)
        {
            _closeEducation.onClick.AddListener(() => { _educationView.OnCloseEducation(); });
            _educationView.SetActive(true);
            _educationView.SetActiveMask(true);
            await Task.Delay(100);
            ContainerSaveerPlayerPrefs.Instance.SaveerData.IsEducation = 1;
            _educationView.SetActiveMask(false);
        }
        else
            _educationView.SetActive(false);
    }
}
