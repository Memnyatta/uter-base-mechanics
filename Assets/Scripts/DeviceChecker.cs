using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceChecker : MonoBehaviour 
{
    public PlayerInput _input;
    public string _deviceName;

    public delegate void IconChangeHandler(string deviceName);
    public static event IconChangeHandler IconChange;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _deviceName = "Keyboard:/Keyboard";
    }

    public void ChangeIcon()
    {
        if(_input != null )
        {
            _deviceName = _input.devices[0].ToString();
        }
        if(IconChange!= null)
        {
            IconChange(_deviceName);
        }
    }
}
