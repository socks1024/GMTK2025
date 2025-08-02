using UnityEngine;
using UnityEngine.Events;

namespace Tools.DataPersistence.PlayerPref {
    public abstract class PlayerPrefSavableField<T,U>
    {
        protected T _currValue;

        public T CurrValue{ get{ return _currValue; }}

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

            SetHandler(_currValue);
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

        /// <summary>
        /// 连接持久化字段和字段容器，必须在初始化时调用
        /// </summary>
        /// <param name="eventHandler">字段容器</param>
        public virtual void ConnectEvent(U eventHandler)
        {
            HandlerWriteValue += (T value) => _currValue = value;
            _currEventHandler = eventHandler;
        }

        /// <summary>
        /// 将string转成目标类型
        /// </summary>
        /// <param name="value">读取到的string值</param>
        /// <returns>目标类型</returns>
        protected abstract T ConvertValue(string value);

        /// <summary>
        /// 调整字段容器中对应字段的值
        /// </summary>
        /// <param name="value">要调整的值</param>
        protected abstract void SetHandler(T value);

        /// <summary>
        /// 在字段容器输入修改时更新值，并触发其他关联的回调
        /// </summary>
        /// <param name="value">输入的修改值</param>
        public UnityAction<T> HandlerWriteValue;
    }
}