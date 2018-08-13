using UnityEngine;

namespace Momentum
{
    [System.Serializable]
    public class Task
    {
        [SerializeField] TaskData data;

        System.Action<TaskData> onStart;
        System.Action<TaskData> onUpdate;
        System.Action<TaskData> onRepeat;
        System.Action<TaskData> onComplete;

        public Task()
        {
            data = new TaskData(this);
        }

        public static Task Add()
        {
            Task task = new Task();
            Core.Juggler.Add(task);
            return task;
        }

        public static void Remove(Task task)
        {
            Core.Juggler.Remove(task);
        }

        public Task Name(string name)
        {
            data.Name = name;
            return this;
        }

        public Task Time(float time = 1f)
        {
            data.Time = time;
            return this;
        }

        public Task Random(float randomTime = 0f)
        {
            data.Random = randomTime;
            return this;
        }

        public Task Delay(float delay = 0f)
        {
            data.Delay = delay;
            return this;
        }

        public Task Loop(int loops = 0)
        {
            if (loops == -1)
            {
                data.Loops = int.MaxValue;
            }
            else
            {
                data.Loops = loops;
            }
            return this;
        }

        // TODO: have multiple children; go one by one though them
        public Task Next(Task task)
        {
            data.Next = task;
            return this;
        }

        public Task Dispose(TaskDisposables container)
        {
            container.Add(this);
            return this;
        }

        public Task OnStart(System.Action<TaskData> callback)
        {
            onStart = callback;
            return this;
        }

        public Task OnUpdate(System.Action<TaskData> callback)
        {
            onUpdate = callback;
            return this;
        }

        public Task OnComplete(System.Action<TaskData> callback)
        {
            onComplete = callback;
            return this;
        }

        public Task OnRepeat(System.Action<TaskData> callback)
        {
            onRepeat = callback;
            return this;
        }

        public void Update(float deltaTime)
        {
            if (data.CurrentDelay < data.Delay)
            {
                data.CurrentDelay += deltaTime;
                return;
            }

            if (data.CurrentTime <= 0f && (data.Loops == 0 || data.CurrentLoop == 0))
            {
                data.CurrentRandom = UnityEngine.Random.Range(-data.Random, data.Random);

                if (onStart != null) onStart(data);
            }

            data.CurrentTime += deltaTime;

            if (onUpdate != null) onUpdate(data);

            if (data.CurrentTime >= data.Time)
            {
                if (data.CurrentLoop == data.Loops)
                {
                    data.IsActive = false;

                    if (onComplete != null) onComplete(data);

                    if (data.Next != null) Core.Juggler.Add(data.Next);
                }
                else if (data.CurrentLoop < data.Loops)
                {
                    data.CurrentLoop++;

                    data.CurrentTime -= data.CurrentTime + deltaTime;

                    data.CurrentRandom = UnityEngine.Random.Range(-data.Random, data.Random);

                    if (data.CurrentLoop <= data.Loops)
                    {
                        if (onRepeat != null) onRepeat(data);
                    }
                }
            }
        }

        public bool IsActive()
        {
            return data.IsActive;
        }

        public void Reset()
        {
            data.IsActive = true;
            data.CurrentTime = 0f;
            data.CurrentRandom = 0f;
            data.CurrentDelay = 0f;
            data.CurrentLoop = 0;
        }
    }
}