using System;
using System.Linq;
using UnityEngine;

public class AppLuckEvents : MonoBehaviour
{
    public void Awake()
    {
        gameObject.name = "AppLuckEvents";
        DontDestroyOnLoad(this);
    }
    private static event Action _onInitSuccessEvent;

    public static event Action onInitSuccessEvent
    {
        add
        {
            if (_onInitSuccessEvent == null || !_onInitSuccessEvent.GetInvocationList().Contains(value))
            {
                _onInitSuccessEvent += value;
            }
        }

        remove
        {
            if (_onInitSuccessEvent.GetInvocationList().Contains(value))
            {
                _onInitSuccessEvent -= value;
            }
        }
    }

    public void onInitSuccess()
    {
        if (_onInitSuccessEvent != null)
        {
            _onInitSuccessEvent();
        }
    }

    private static event Action<string> _onPlacementLoadSuccessEvent;

    public static event Action<string> onPlacementLoadSuccessEvent
    {
        add
        {
            if (_onPlacementLoadSuccessEvent == null || !_onPlacementLoadSuccessEvent.GetInvocationList().Contains(value))
            {
                _onPlacementLoadSuccessEvent += value;
            }
        }
        remove
        {
            if (_onPlacementLoadSuccessEvent.GetInvocationList().Contains(value))
            {
                _onPlacementLoadSuccessEvent -= value;
            }
        }
    }

    public void placementLoadSuccess(string sk)
    {
        if (_onPlacementLoadSuccessEvent != null)
        {
            _onPlacementLoadSuccessEvent(sk);
        }
    }
}
