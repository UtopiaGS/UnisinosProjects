using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadUtil : MonoBehaviour
{

    private static Queue<Action> actionQueue = new Queue<Action>();

    public static void CallInMainThread(Action action)
    {
        lock (actionQueue)
        {
            actionQueue.Enqueue(action);
        }
    }

    private void FlushQueue()
    {
        lock (actionQueue)
        {
            while (actionQueue.Count > 0)
            {
                var action = actionQueue.Dequeue();
                action();
            }
        }
    }

    private void Update()
    {
        FlushQueue();
    }

    private void LateUpdate()
    {
        FlushQueue();
    }
}
