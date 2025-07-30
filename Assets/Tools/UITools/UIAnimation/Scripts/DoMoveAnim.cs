using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tools.UITools.Animation
{
    public class DoMoveAnim : SerializedMonoBehaviour
    {
        #region Serialized Field

        [EnumToggleButtons]
        public MoveEventMode EventMode;

        [ShowIf("EventMode", MoveEventMode.Single)]
        public MoveEvent MoveEvent;

        [ShowIf("EventMode", MoveEventMode.List)]
        public List<MoveEvent> MoveEvents = new List<MoveEvent>();

        [ShowIf("EventMode", MoveEventMode.Dictionary)]
        public Dictionary<string, MoveEvent> MoveEventsDictionary = new Dictionary<string,MoveEvent>();

        #endregion

        private bool _moving = false;

        public void PlayMoveAnim()
        {
            if (EventMode == MoveEventMode.Single)
            {
                PlayMoveAnim(MoveEvent);
            }
        }

        public void PlayMoveAnim(int index)
        {
            if (EventMode == MoveEventMode.List)
            {
                PlayMoveAnim(MoveEvents[index]);
            }
        }

        public void PlayMoveAnim(string key)
        {
            if (EventMode == MoveEventMode.Dictionary)
            {
                PlayMoveAnim(MoveEventsDictionary[key]);
            }
        }

        public void PlayMoveAnim(MoveEvent moveEvent)
        {
            transform.DOComplete();
            
            if (_moving) return;

            _moving = true;

            transform.DOMove(moveEvent.EndPoint.position, moveEvent.MoveTime)
                .SetEase(moveEvent.MoveEventAnimCurve)
                .OnComplete(() => _moving = false);
        }

        // private IEnumerator MoveAnimation(MoveEvent moveEvent)
        // {
        //     m_Moving = true;

        //     float time = 0;

        //     Vector3 startPos = transform.position;

        //     while (time < moveEvent.MoveTime)
        //     {
        //         transform.position = Vector3.LerpUnclamped(startPos, moveEvent.EndPoint.position, moveEvent.MoveEventAnimCurve.Evaluate(time / moveEvent.MoveTime));

        //         yield return 0;

        //         time += Time.deltaTime;
        //     }

        //     m_Moving = false;
        // }

        void Start()
        {
            PlayMoveAnim();
        }
    }

    public enum MoveEventMode
    {
        Single,
        List,
        Dictionary,
    }

    [Serializable]
    public struct MoveEvent
    {
        public Transform EndPoint;

        public float MoveTime;

        public AnimationCurve MoveEventAnimCurve;
    }
}