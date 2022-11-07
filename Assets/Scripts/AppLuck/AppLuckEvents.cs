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

    public static event Action _onPlacementCloseEvent;
    public static event Action onPlacementCloseEvent
    {
        add
        {
            if (_onPlacementCloseEvent == null || !_onPlacementCloseEvent.GetInvocationList().Contains(value))
            {
                _onPlacementCloseEvent += value;
            }
        }
        remove
        {
            if (_onPlacementCloseEvent.GetInvocationList().Contains(value))
            {
                _onPlacementCloseEvent -= value;
            }
        }
    }

    public void placementClose()
    {
        if (_onPlacementCloseEvent != null)
        {
            _onPlacementCloseEvent();
            foreach (Action act in _onPlacementCloseEvent.GetInvocationList())
            {
                _onPlacementCloseEvent -= act;
            }
        }
    }
}
