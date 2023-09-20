using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private float _targetScale = 1;
    private Vector3 _scaleVel;
    
    private void OnEnable()
    {
        MainMenuScript.ScaledChanged += OnScaledChanged;
    }

    private void OnDisable()
    {
        MainMenuScript.ScaledChanged -= OnScaledChanged;
    }

    private void OnScaledChanged(float newScale)
    {
        _targetScale = newScale;
    }

    private void Update()
    {
        transform.localScale =
            Vector3.SmoothDamp(transform.localScale, _targetScale * Vector3.one, ref _scaleVel, 0.15f);
    }
}
