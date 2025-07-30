using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tools.GameProgrammingPatterns.Event
{
    public class PriorityAction 
    {
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority;

        public UnityAction Action;

        public PriorityAction(int priority, UnityAction action) 
        {
            Priority = priority;
            Action = action;
        }
    }

    public class PriorityAction<T>
    {
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority;

        public UnityAction<T> Action;

        public PriorityAction(int priority, UnityAction<T> action) 
        {
            Priority = priority;
            Action = action;
        }
    }

    public class PriorityEventInfo : IEventInfo
    {
        public List<PriorityAction> allActions = new List<PriorityAction>();

        //真正观察者 对应的 函数信息 记录在其中
        public PriorityEventInfo(PriorityAction action)
        {
            allActions.Add(action);
        }

        public void Remove(PriorityAction action) 
        {
            allActions.Add(action);
            //降序排列
            allActions.Sort((x, y) => y.Priority.CompareTo(x.Priority));
        }

        public void RemoveEvent(PriorityAction action)
        {
            allActions.Remove(action);
            //降序排列
            allActions.Sort((x, y) => y.Priority.CompareTo(x.Priority));
        }
    }

    public class PriorityEventInfo<T> : IEventInfo
    {
        public List<PriorityAction<T>> allActions = new List<PriorityAction<T>>();

        //真正观察者 对应的 函数信息 记录在其中
        public PriorityEventInfo(PriorityAction<T> action)
        {
            allActions.Add(action);
        }

        public void AddEvent(PriorityAction<T> action)
        {
            allActions.Add(action);
            //降序排列
            allActions.Sort((x, y) => y.Priority.CompareTo(x.Priority));
        }

        public void RemoveEvent(PriorityAction<T> action)
        {
            allActions.Remove(action);
            //降序排列
            allActions.Sort((x, y) => y.Priority.CompareTo(x.Priority));
        }
    }
}

