using System;
using Cysharp.Threading.Tasks;
using Project.Application.Ports.Persistance;
using Project.Application.Ports.Persistance.LocalStorage;
using Project.Core.Enums;
using UnityEngine;

namespace Project.Infrastructure.Persistance
{
    public class UserStorage : IUserStorage
    {
        public string Username
        {
            get => GetUsername();
            set => SetUsername(value);
        }

        public string NickName
        {
            get => GetNickName();
            set => SetNickName(value);
        }

        public Func<LocalStorageKey, object, bool> SetVariableFunc { get; set; }
        public Func<LocalStorageKey, Type, object, object> GetVariableFunc { get; set; }

        public UserStorage(Func<LocalStorageKey, object, bool> setVariableFunc,
            Func<LocalStorageKey, Type, object, object> getVariableFunc)
        {
            SetVariableFunc = setVariableFunc;
            GetVariableFunc = getVariableFunc;
        }

        public string GetUsername()
        {
            return (string)GetVariableFunc(LocalStorageKey.UserName, typeof(string), String.Empty);
        }

        public async UniTask SetUsername(string username)
        {
            SetVariableFunc(LocalStorageKey.UserName, username);
        }

        public string GetNickName()
        {
            return (string)GetVariableFunc(LocalStorageKey.NickName, typeof(string), String.Empty);
        }

        public async UniTask SetNickName(string nickName)
        {
            SetVariableFunc(LocalStorageKey.NickName, nickName);
        }
    }
}