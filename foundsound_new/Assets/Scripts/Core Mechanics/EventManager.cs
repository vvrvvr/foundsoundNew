using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent OnPause = new UnityEvent();
    public static UnityEvent OnResumeGame = new UnityEvent();
    public static UnityEvent OnNewGame = new UnityEvent();
    public static UnityEvent OnNotePadOpened = new UnityEvent();
    public static UnityEvent OnNotePadClosed = new UnityEvent();
}