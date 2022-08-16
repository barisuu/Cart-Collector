using System;

public static class EventManager
{
    public static event EventHandler CollectMoney;

    public static void OnCollectMoney()
    {
        CollectMoney?.Invoke(null, EventArgs.Empty);
    }
}
