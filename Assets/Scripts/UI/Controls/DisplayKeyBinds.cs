using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[Serializable]
public class KBindOverride
{
    public string officialName;
    public string overrideName;
}

public class DisplayKeyBinds : MonoBehaviour
{
    [SerializeField] private InputActionAsset controls;
    public PlayerInput input;
    public GameObject bindPrefab;
    private InputActionMap _inputActionMap;
    private UIDataFill datafill;
    public List<KBindOverride> BindOverrides = new List<KBindOverride>();
    // Start is called before the first frame update
    void Start()
    {
        datafill = GetComponent<UIDataFill>();
        _inputActionMap = controls.FindActionMap("Gameplay");
        foreach(InputAction action in _inputActionMap.actions)
        {
            //Bind b = new Bind { bind = action.GetBindingDisplayString(), name = action.name };
            string bindName = action.name;
            foreach(KBindOverride kbind in BindOverrides)
            {
                if(bindName == kbind.officialName) { bindName = kbind.overrideName; }
            }

            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                { "bind", action.GetBindingDisplayString().ToUpper() },
                { "name", bindName},
                { "controlScheme", input.currentControlScheme }
            };


            GameObject kb = Instantiate(bindPrefab, transform);
            datafill.Fill(data, kb);
        }
    }
}
