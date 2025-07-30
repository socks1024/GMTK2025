using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tools.UITools.Text.TextBubble
{
    public class TextBubble : MonoBehaviour
    {
        private RectTransform _textRect;

        private RectTransform _panelRect;

        private TextMeshProUGUI _tmp;

        private ContentSizeFitter _fitter;

        private Image _image;

        public UnityAction OnSetText;

        void Awake()
        {
            _image = GetComponentInChildren<Image>();
            _tmp = GetComponentInChildren<TextMeshProUGUI>();
            _textRect = _tmp.GetComponent<RectTransform>();
            _panelRect = _image.GetComponent<RectTransform>();
            _fitter = _tmp.GetComponent<ContentSizeFitter>();
        }

        void Start()
        {
            RefreshText();
        }

        public void RefreshText()
        {
            SetText(_tmp.text);
        }

        public void SetText(string text)
        {
            _tmp.text = text;

            SetSizeWithCurrentText();

            OnSetText?.Invoke();
        }

        public void SetPivot(Vector2 pivot)
        {
            GetComponent<RectTransform>().pivot = pivot;

            SetSizeWithCurrentText();
        }

        public void SetAnchor(Vector2 anchor)
        {
            GetComponent<RectTransform>().anchorMax = anchor;
            GetComponent<RectTransform>().anchorMin = anchor;

            SetSizeWithCurrentText();
        }

        public void SetRect(Vector2 anchorPreset)
        {
            SetPivot(anchorPreset);
            SetAnchor(anchorPreset);
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        public void SetColor(Color color)
        {
            _image.color = color;
        }

        private void SetSizeWithCurrentText()
        {
            if (_tmp.preferredWidth >= TextMaxWidth)
            {
                _textRect.sizeDelta = new Vector2(TextMaxWidth, 0);
                _fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                _fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            }
            else
            {
                _fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                _fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            }

            _fitter.SetLayoutHorizontal();
            _fitter.SetLayoutVertical();

            _panelRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _textRect.sizeDelta.x);
            _panelRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _textRect.sizeDelta.y);

            GetComponent<RectTransform>().sizeDelta = _textRect.sizeDelta;
        }

        #region Inspector Field

        public float TextMaxWidth = 100;

        #endregion
    }
}