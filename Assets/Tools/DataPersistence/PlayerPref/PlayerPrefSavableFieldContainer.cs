using System.Collections.Generic;
using UnityEngine;

namespace Tools.DataPersistence.PlayerPref {
    public abstract class PlayerPrefSavableFieldContainer<T,A,B> : MonoBehaviour where T :PlayerPrefSavableField<A,B>
    {
        private List<T> _fields = new();

        void Start()
        {
            _fields = SetFields();
            ConnectEvent();
            _fields.ForEach(f => f.Load());
        }

        protected abstract List<T> SetFields();

        protected abstract void ConnectEvent();

        public void SaveSettings()
        {
            _fields.ForEach(f => f.Save());
        }

        public void LoadSettings()
        {
            _fields.ForEach(f => f.Load());
        }

        public void ResetSettings()
        {
            _fields.ForEach(f => f.Reset());
        }
    }
}