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
