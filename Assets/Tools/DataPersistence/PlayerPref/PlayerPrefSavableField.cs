using UnityEngine;

namespace Tools.DataPersistence.PlayerPref {
    public abstract class PlayerPrefSavableField<T,U>
    {
        protected T _currValue;

        protected U _currEventHandler;
        
        protected abstract T DefaultValue { get; }

        public abstract string PrefKey { get; }

        public void Load()
        {
            if (PlayerPrefs.HasKey(PrefKey))
            {
                _currValue = ConvertValue(PlayerPrefs.GetString(PrefKey));
            }
            else
            {
                _currValue = DefaultValue;
            }

            SetUIObject(_currValue);
        }

        public void Save()
        {
            PlayerPrefs.SetString(PrefKey, _currValue.ToString());
        }

        public void Reset()
        {
            PlayerPrefs.SetString(PrefKey, DefaultValue.ToString());

            Load();
        }

        public virtual void ConnectEvent(U eventHandler)
        {
            _currEventHandler = eventHandler;
        }

        protected abstract T ConvertValue(string value);

        protected abstract void SetUIObject(T value);

        protected abstract void OnSetUI(T value);
    }
}