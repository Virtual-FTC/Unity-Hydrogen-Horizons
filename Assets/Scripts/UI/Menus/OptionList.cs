using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

/// <summary>
/// OptionList offers multiple strings to choose from using buttons.
/// </summary>
[AddComponentMenu("UI/Option List")]
public class OptionList : MonoBehaviour
{
    [Header("Option List Object References")]
    [Tooltip("The text to show the current option.")]
    public TextMeshProUGUI optionText;

    [Header("Option List Variables")]
    [Tooltip("The list of option choices.")]
    public List<string> options;
    [Tooltip("The event invoked on changing the current option.")]
    public UnityEvent<string> onChangeOption;
    public UnityEvent<int> onChangeOptionIndex;
    [HideInInspector] public int optionIndex;
    [HideInInspector] public string option;

    public void Awake()
    {
        SetOption(optionIndex);
    }

    /// <summary>
    /// Sets the chosen option to the one given.
    /// </summary>
    /// <param name="index"></param>
    public void SetOption(int index)
    {
        optionIndex = index;
        option = options[optionIndex];
        optionText.text = option;
        UpdateOption(index);
    }

    public virtual void UpdateOption(int index)
    {

    }

    /// <summary>
    /// Increments/decrements the current option by the given amount.
    /// </summary>
    /// <param name="direction"></param>
    public void ChangeOption(int direction)
    {
        optionIndex += direction;

        if (optionIndex < 0)
            optionIndex = options.Count - 1;
        else if (optionIndex >= options.Count)
            optionIndex = 0;

        SetOption(optionIndex);
        onChangeOption.Invoke(options[optionIndex]);
        onChangeOptionIndex.Invoke(optionIndex);
    }

    public void SetAllOptions(List<string> options)
    {
        this.options = options;
        optionIndex = 0;
        SetOption(0);
    }
}

