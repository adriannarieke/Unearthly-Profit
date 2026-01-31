using System;
using UnityEngine;

[Serializable]
public struct Timer
{
    [SerializeField] float timeLimit;
    [SerializeField] float timeElapsed;

    public float TimeElapsed => timeElapsed;
    public float TimeLimit => timeLimit;
    public float TimeRemaining => TimeLimit - timeElapsed;
    public float FractionOfTimeElapsed => TimeElapsed / TimeLimit;
    public float FractionOfTimeRemaining => TimeRemaining / TimeLimit;
    public int Laps => (int)(TimeElapsed / TimeLimit);

    public Timer(float timeLimit)
    {
        this.timeLimit = timeLimit;
        timeElapsed = 0f;
    }

    public Timer(Timer timer)
    {
        timeLimit = timer.timeLimit;
        timeElapsed = 0f;
    }

    public void Update(float timeElapsed)
    {
        this.timeElapsed = Mathf.Max(0f, this.timeElapsed + timeElapsed);
    }

    public void Reset()
    {
        timeElapsed = 0f;
    }
}

[Serializable]
public struct DateTimer
{
    /// <summary>
    /// Time limit (in seconds)
    /// </summary>
    [SerializeField] float timeLimit;
    DateTime start;

    public float TimeLimit => timeLimit;
    /// <summary>
    /// Time elapsed (in seconds)
    /// </summary>
    public float TimeElapsed => (float)(DateTime.Now - start).TotalSeconds;
    /// <summary>
    /// Time remaining (in seconds)
    /// </summary>
    public float TimeRemaining => timeLimit - TimeElapsed;
    public bool OutOfTime => TimeElapsed >= TimeLimit;

    public float FractionOfTimeElapsed => TimeElapsed / TimeLimit;
    public float FractionOfTimeRemaining => TimeRemaining / TimeLimit;

    public DateTimer(float timeLimit)
    {
        this.timeLimit = timeLimit;
        start = DateTime.Now;
    }

    public void Reset()
    {
        this = new DateTimer(timeLimit);
    }

    /// <summary>
    /// Format date timer
    /// </summary>
    public static string Format(float timeElapsed)
    {
        return TimeSpan.FromSeconds(timeElapsed).ToString();
    }

    public static DateTimer FromMilliseconds(int milliseconds)
    {
        return new DateTimer(milliseconds / 1000f);
    }
    public static DateTimer FromMinutes(int minutes)
    {
        return new DateTimer(minutes * 60);
    }
    public static DateTimer FromHours(int hours)
    {
        return new DateTimer(hours * 3600);
    }
}
