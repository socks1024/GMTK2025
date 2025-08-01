using UnityEngine.UI;

namespace Tools.DataPersistence.PlayerPref {
    public abstract class PlayerPrefSavableSlider : PlayerPrefSavableField<float, Slider>
    {
        public override void ConnectEvent(Slider eventHandler)
        {
            base.ConnectEvent(eventHandler);
            eventHandler.onValueChanged.AddListener(HandlerWriteValue);
        }

        protected override float ConvertValue(string value)
        {
            return (float)System.Convert.ToDouble(value);
        }

        protected override void SetHandler(float value)
        {
            _currEventHandler.value = value;
        }
    }
}