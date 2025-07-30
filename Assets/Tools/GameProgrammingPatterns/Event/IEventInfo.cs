using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tools.GameProgrammingPatterns.Event
{
    public interface IEventInfo
    {

    }

    public class EventInfo : IEventInfo
    {
        public UnityAction actions;

        public EventInfo(UnityAction action)
        {
            this.actions += action;
        }
    }

    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;

        public EventInfo(UnityAction<T> action)
        {
            this.actions += action;
        }
    }
}