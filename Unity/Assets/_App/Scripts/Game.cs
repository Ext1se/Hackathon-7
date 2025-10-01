using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Image _transition;

    [SerializeField] private GameObject _closeBoxContent;
    [SerializeField] private GameObject _openBoxContent;

    [SerializeField] private GameObject _dialogHelp;
    
    [SerializeField] private GameObject _dialogStep0;
    [SerializeField] private GameObject _dialogStep1;
    [SerializeField] private GameObject _dialogStep2;
    [SerializeField] private GameObject _dialogStep3;
    [SerializeField] private GameObject _dialogStep4;

    private bool _isCompleted = false;
    private bool _isImageFound = false;
    private GameObject _currentDialog;
    
    private void Awake()
    {
        _transition.gameObject.SetActive(false);
        _openBoxContent.gameObject.SetActive(false);
        _closeBoxContent.gameObject.SetActive(true);
        
        _dialogHelp.gameObject.SetActive(false);
        
        _dialogStep0.gameObject.SetActive(false);
        _dialogStep1.gameObject.SetActive(false);
        _dialogStep2.gameObject.SetActive(false);
        _dialogStep3.gameObject.SetActive(false);
    }

    private void Start()
    {
        Reset();

        if (!_isImageFound)
        {
            ShowHelp();
        }
    }
    
    public void Reset()
    {
        CloseDialog();

        _transition.gameObject.SetActive(false);
        _openBoxContent.gameObject.SetActive(false);
        _closeBoxContent.gameObject.SetActive(true);
        
        _isCompleted = false;
        SetStep0();
    }

    public void ShowHelp()
    {
        if (_currentDialog != null)
        {
            _currentDialog.gameObject.SetActive(false);
        }
        
        _dialogHelp.gameObject.SetActive(true);
    }

    public void SetStep0()
    {
        _currentDialog = _dialogStep0;
        _dialogStep0.gameObject.SetActive(true);
    }
    
    public void SetStep1()
    {
        _currentDialog = _dialogStep1;
        
        _transition.gameObject.SetActive(true);
        
        Invoke(nameof(SetOpenBoxContent), 1f);
        Invoke(nameof(UnlockStep1), 2f);
        
        _dialogStep0.gameObject.SetActive(false);
    }

    private void SetOpenBoxContent()
    {
        _closeBoxContent.gameObject.SetActive(false);
        _openBoxContent.gameObject.SetActive(true);
    }

    private void UnlockStep1()
    {
        if (!_isImageFound)
        {
            return;
        }
        
        _transition.gameObject.SetActive(false);
        _dialogStep1.gameObject.SetActive(true);
    }
    
    public void SetStep2()
    {
        _currentDialog = _dialogStep2;
        
        _dialogStep1.gameObject.SetActive(false);
        _dialogStep2.gameObject.SetActive(true);
    }

    public void SetStep3()
    {
        _currentDialog = _dialogStep3;
        _dialogStep2.gameObject.SetActive(false);
        _dialogStep3.gameObject.SetActive(true);
    }
    
    public void SetStep4()
    {
        _transition.gameObject.SetActive(false);
        
        _isCompleted = true;
        _currentDialog = null;
        //_currentDialog = _dialogStep4;
        
        _dialogStep3.gameObject.SetActive(false);
        _dialogStep4.gameObject.SetActive(true);
    }

    public void CloseDialog()
    {
        _currentDialog = null;

        _transition.gameObject.SetActive(false);
        
        _dialogStep0.gameObject.SetActive(false);
        _dialogStep1.gameObject.SetActive(false);
        _dialogStep2.gameObject.SetActive(false);
        _dialogStep3.gameObject.SetActive(false);
        _dialogStep4.gameObject.SetActive(false);

        _isCompleted = true;
    }

    public void OnImageFound()
    {
        _isImageFound = true;

        if (_isCompleted)
        {
            return;
        }
        
        _dialogHelp.gameObject.SetActive(false);
        
        if (_currentDialog != null)
        {
            _currentDialog.gameObject.SetActive(true);
        }
    }

    public void OpenUrl()
    {
        Application.OpenURL("https://souzmultpark.ru/");
    }
    
    public void OnImageLost()
    {
        _isImageFound = false;

        if (_isCompleted)
        {
            return;
        }
        
        if (_currentDialog != null)
        {
            _currentDialog.gameObject.SetActive(false);
        }
        
        _dialogHelp.gameObject.SetActive(true);
    }
}
