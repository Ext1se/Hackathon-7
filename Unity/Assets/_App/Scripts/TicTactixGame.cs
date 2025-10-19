using UnityEngine;
using UnityEngine.UI;

public class TicTactixGame : MonoBehaviour
{
    [SerializeField] private GameObject _dialogHelp;
    
    [SerializeField] private GameObject _dialogStep0;

    private bool _isCompleted = false;
    private bool _isImageFound = false;
    private GameObject _currentDialog;
    
    private void Awake()
    {
        //_dialogHelp.gameObject.SetActive(false);
        //_dialogStep0.gameObject.SetActive(false);
    }

    private void Start()
    {
        //Reset();

        /*if (!_isImageFound)
        {
            ShowHelp();
        }*/
    }
    
    public void Reset()
    {
        CloseDialog();
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
    
    private void SetOpenBoxContent()
    {
    }

    public void SetStep4()
    {
        _isCompleted = true;
        _currentDialog = null;
        //_currentDialog = _dialogStep4;
    }

    public void CloseDialog()
    {
        _currentDialog = null;

        _dialogStep0.gameObject.SetActive(false);

        _isCompleted = true;
    }

    public void OnImageFound()
    {
        _isImageFound = true;

        /*if (_isCompleted)
        {
            return;
        }*/
        
        _dialogHelp.gameObject.SetActive(false);
        
        /*if (_currentDialog != null)
        {
            _currentDialog.gameObject.SetActive(true);
        }*/
    }

    public void OpenUrl()
    {
        Application.OpenURL("https://t.me/tictactix");
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
