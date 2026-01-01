using System;
using Cysharp.Threading.Tasks;
using Project.Application.Ports.Persistance.LocalStorage;
using Project.Application.Ports.Persistance.LocalStorage.Interface;
using Project.Core.Enums;
using Project.Infrastructure.Persistance;
using Project.Presentation.Infrastructures.Base;
using UnityEngine;

namespace Project.Presentation.Infrastructures.Persistance
{
    public class LocalStorage : BaseService,ILocalStorage
    {
        public IUserStorage User { get; set; }
        
        protected override async UniTask InitializeService()
        {
            User = new UserStorage(SavePlayerPrefs,LoadPlayerPrefs);
            await UniTask.Yield();
        }

        private bool SavePlayerPrefs(LocalStorageKey key, object value)
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

        private object LoadPlayerPrefs(LocalStorageKey key, Type T, object defaultValue)
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
    }
}