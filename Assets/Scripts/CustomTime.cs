using System;

public class CustomTime
{
    private DateTime _lastTime;
    private float _deltaTime;
    public float DeltaTime => _deltaTime;

    public void Update()
    {
        var currentTime = DateTime.Now;
        _deltaTime = (float)(currentTime - _lastTime).TotalSeconds;
        _lastTime = currentTime;
    }
}
