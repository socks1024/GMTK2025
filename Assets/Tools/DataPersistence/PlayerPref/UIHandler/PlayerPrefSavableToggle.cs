using UnityEngine.UI;

namespace Tools.DataPersistence.PlayerPref {
    public abstract class PlayerPrefSavableToggle : PlayerPrefSavableField<bool, Toggle>
    {
        public override void ConnectEvent(Toggle eventHandler)
        {
            base.ConnectEvent(eventHandler);
            eventHandler.onValueChanged.AddListener(OnSetUI);
        }

        protected override bool ConvertValue(string value)
        {
            return System.Convert.ToBoolean(value);
        }

        protected override void SetUIObject(bool value)
        {
            _currEventHandler.isOn = value;
        }
    }
}