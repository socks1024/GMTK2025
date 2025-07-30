using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Tools.UITools.Animation
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickAnim : MonoBehaviour
    {
        #region Serialize Field

        [Header("按下时动效")]

        [Range(1,10)]
        public float AnimAmplitude = 2;

        public float AnimDuration;
        
        public AnimationCurve ScaleAnimCurve;

        #endregion

        private Vector3 _baseScale;

        private Button _button;

        void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            _baseScale = transform.localScale;

            _button.interactable = false;

            transform.DOScale(_baseScale * AnimAmplitude, AnimDuration)
                .SetEase(ScaleAnimCurve)
                .OnComplete(() => _button.interactable = true);
        }

        // IEnumerator ClickAnimation()
        // {
        //     button.interactable = false;

        //     float time = 0;

        //     while (time < AnimDuration)
        //     {
        //         transform.localScale = baseScale * Mathf.Abs(ScaleAnimCurve.Evaluate(time / AnimDuration) * AnimAmplitude + 1);

        //         yield return 0;

        //         time += Time.deltaTime;

        //     }

        //     transform.localScale = baseScale;

        //     button.interactable = true;
        // }
    }
}