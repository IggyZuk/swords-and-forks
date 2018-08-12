using UnityEngine;
using Momentum;

public class AI
{
    CommanderID comID;

    public AI(CommanderID comID)
    {
        this.comID = comID;

        Task.Add().Time(1f).Loop(-1).OnRepeat(_ =>
        {
            Debug.Log("Thinking...");
        });
    }
}
