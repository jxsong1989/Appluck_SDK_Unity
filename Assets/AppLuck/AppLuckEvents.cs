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

    private static event Action _onInitFailedEvent;
    public static event Action onInitFailedEvent
    {
        add
        {
            if (_onInitFailedEvent == null || !_onInitFailedEvent.GetInvocationList().Contains(value))
            {
                _onInitFailedEvent += value;
            }
        }

        remove
        {
            if (_onInitFailedEvent.GetInvocationList().Contains(value))
            {
                _onInitFailedEvent -= value;
            }
        }
    }

    public void onInitFailed()
    {
        if (_onInitFailedEvent != null)
        {
            _onInitFailedEvent();
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

    private static event Action<string, int> _onInteractiveAdsHiddenEvent;
    public static event Action<string, int> onInteractiveAdsHidden
    {
        add
        {
            if (_onInteractiveAdsHiddenEvent == null || !_onInteractiveAdsHiddenEvent.GetInvocationList().Contains(value))
            {
                _onInteractiveAdsHiddenEvent += value;
            }
        }
        remove
        {
            if (_onInteractiveAdsHiddenEvent.GetInvocationList().Contains(value))
            {
                _onInteractiveAdsHiddenEvent -= value;
            }
        }
    }

    public void interactiveAdsHidden(string msg)
    {
        if (_onInteractiveAdsHiddenEvent != null)
        {
            string[] ss = msg.Split(':');
            _onInteractiveAdsHiddenEvent(ss[0], int.Parse(ss[1]));
            foreach (Action<string, int> act in _onInteractiveAdsHiddenEvent.GetInvocationList())
            {
                _onInteractiveAdsHiddenEvent -= act;
            }
        }
    }

    private static event Action<string> _onInteractiveAdsDisplayedEvent;
    public static event Action<string> onInteractiveAdsDisplayed
    {
        add
        {
            if (_onInteractiveAdsDisplayedEvent == null || !_onInteractiveAdsDisplayedEvent.GetInvocationList().Contains(value))
            {
                _onInteractiveAdsDisplayedEvent += value;
            }
        }
        remove
        {
            if (_onInteractiveAdsDisplayedEvent.GetInvocationList().Contains(value))
            {
                _onInteractiveAdsDisplayedEvent -= value;
            }
        }
    }

    public void interactiveAdsDisplayed(string placementId)
    {
        if (_onInteractiveAdsDisplayedEvent != null)
        {
            _onInteractiveAdsDisplayedEvent(placementId);
        }
    }

    private static event Action<string, string> _onUserInteractionEvent;
    public static event Action<string, string> onUserInteractionEvent
    {
        add
        {
            if (_onUserInteractionEvent == null || !_onUserInteractionEvent.GetInvocationList().Contains(value))
            {
                _onUserInteractionEvent += value;
            }
        }
        remove
        {
            if (_onUserInteractionEvent.GetInvocationList().Contains(value))
            {
                _onUserInteractionEvent -= value;
            }
        }
    }

    public void userInteraction(string msg)
    {
        Debug.LogError("userInteraction unity : " + msg);
        if (_onUserInteractionEvent != null)
        {
            string[] s = msg.Split(':');
            _onUserInteractionEvent(s[0], s[1]);
        }
    }
}
