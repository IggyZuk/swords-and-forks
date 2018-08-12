using UnityEngine;
using UnityEngine.UI;
using Momentum;

public class Peasant : MonoBehaviour
{
    [SerializeField] Image body;
    [SerializeField] Image outline;
    [SerializeField] Image resource;

    //bool isMoving 

    Pos pos = new Pos();
    CommanderID comID;

    Task movementTask;

    public void Init(int x, int y, CommanderID comID)
    {
        pos.x = x;
        pos.y = y;

        this.comID = comID;

        body.color = Controller.Instance.commanders[comID].color;


        Task.Add().Loop(-1).OnRepeat(t =>
        {
            if (movementTask != null) return;

            Pos[] neighbours = Controller.Instance.grid.GetNeighbours(pos);

            MoveTo(neighbours[Random.Range(0, neighbours.Length)]);
        });
    }

    public void MoveTo(Pos targetPos)
    {
        if (movementTask != null) return;

        Vector2 origin = Controller.Instance.grid.GetTile(pos).transform.position;
        Vector2 target = Controller.Instance.grid.GetTile(targetPos).transform.position;

        pos = targetPos;

        movementTask = Task.Add()
           .Time(1f)
           .OnUpdate(t => this.transform.position = Vector2.LerpUnclamped(origin, target, Ease.InOutBack(t.progress)))
           .OnComplete(t => movementTask = null);
    }
}
