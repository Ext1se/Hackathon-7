using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class DialogView : MonoBehaviour
{
    [SerializeField] private UnityUITypewriterEffect _typewriterEffect;

    private void OnEnable()
    {
        _typewriterEffect.enabled = true;
    }

    private void OnDisable()
    {
        _typewriterEffect.enabled = false;
    }
}
