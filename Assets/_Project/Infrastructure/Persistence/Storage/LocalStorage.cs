using System;
using Project.Application.Ports.Persistence.Storage;
using Project.Core.Enums;
using UnityEngine;

namespace Project.Infrastructure.Persistence.Storage
{
    public class LocalStorage : ILocalStorage
    {
        public bool SavePlayerPrefs(LocalStorageKey key, object value)
        {
            switch (value)
            {
                case string stringValue:
                    PlayerPrefs.SetString(key.ToString(), stringValue);
                    break;
                case int intValue:
                    PlayerPrefs.SetInt(key.ToString(), intValue);
                    break;
                case float floatValue:
                    PlayerPrefs.SetFloat(key.ToString(), floatValue);
                    break;
                case bool boolValue:
                    PlayerPrefs.SetInt(key.ToString(), boolValue ? 1 : 0);
                    break;
                default:
                    throw new NotImplementedException($"[{nameof(LocalStorage)}] SavePlayerPref: Type {value.GetType()} not implemented");
            }
            
            return true;
        }

        public object LoadPlayerPrefs(LocalStorageKey key, Type T, object defaultValue)
        {
            object value;
            if (T == typeof(string))
            {
                value = PlayerPrefs.GetString(key.ToString(), Convert.ToString(defaultValue));
            }
            else if (T == typeof(int))
            {
                value = PlayerPrefs.GetInt(key.ToString(), Convert.ToInt32(defaultValue));
            }
            else if (T == typeof(float))
            {
                value = PlayerPrefs.GetFloat(key.ToString(),Convert.ToSingle(defaultValue));
            }
            else if(T == typeof(bool))
            {
                value = PlayerPrefs.GetInt(key.ToString(),Convert.ToBoolean(defaultValue)?1:0)==1;
            }
            else
            {
                throw new NotImplementedException($"[{nameof(LocalStorage)}] LoadPlayerPrefs: Type {T} not implemented");
            }

            return value;
        }

        public bool DeletePlayerPrefs(LocalStorageKey key)
        {
            PlayerPrefs.DeleteKey(key.ToString());
            return true;
        }
    }
}