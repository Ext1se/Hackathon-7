using System;
using System;
using UnityEngine;

public class Game1 : MonoBehaviour
{
    private DialogManager _dialogManager;


    private void Awake()
    {
        _dialogManager = GetComponent<DialogManager>();
    }

    private void Start()
    {
        _dialogManager.SetDialogVariable(DialogManager.TUTORIAL_STATE, (int)1);
        _dialogManager.ShowDialog();
    }
}
