using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private UIDocument _document;
    [SerializeField] private StyleSheet _styleSheet;

    public static event Action<float> ScaledChanged;
    
    private void Start()
    {
        StartCoroutine(Generate());
    }

    private void OnValidate()
    {
        if(Application.isPlaying) return;
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        yield return null;
        var root = _document.rootVisualElement;
        root.Clear();
        
        root.styleSheets.Add(_styleSheet);

        var container = Create("container");

        var viewBox = Create("view-box","bordered-box");
        container.Add(viewBox);
        
        var controlBox = Create("control-box","bordered-box");
        container.Add(controlBox);

        var spinBtn = Create<Button>();
        spinBtn.text = "Spin";
        controlBox.Add(spinBtn);
        
        var scaleSlider = Create<Slider>();
        scaleSlider.lowValue = 0.5f;
        scaleSlider.highValue = 2f;
        scaleSlider.value = 1f;
        scaleSlider.RegisterValueChangedCallback(v => ScaledChanged?.Invoke(v.newValue));
        controlBox.Add(scaleSlider);
        
        root.Add(container);
    }

    VisualElement Create(params string[] classNames)
    {
        return Create<VisualElement>(classNames);
    }
    
    T Create<T>(params string[] classNames) where T : VisualElement, new()
    {
        var ele = new T();
        foreach (var className in classNames)
        {
            ele.AddToClassList(className);
        }
        return ele;
    }
}
