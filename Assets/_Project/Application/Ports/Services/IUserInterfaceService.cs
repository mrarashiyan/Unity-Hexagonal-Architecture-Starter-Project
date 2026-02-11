using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Application.Ports.Screen;
using UnityEngine;

namespace Project.Application.Ports.Services
{
    public interface IUserInterfaceService
    {
        public void AddScreen(IScreenView screen);
        public void RemoveScreen(IScreenView screen);
        public void AddLoadingOverlay(IScreenView screen);
        public UniTask ShowScreen<TScreenType>(TScreenType screenType) where TScreenType : Type;
        public UniTask HideScreen<TScreenType>(TScreenType screenType) where TScreenType : Type;
        public UniTask SwitchScreen<TScreenType>(TScreenType from, TScreenType to) where TScreenType : Type;
        public UniTask ShowLoadingOverlay();
        public UniTask HideLoadingOverlay();
        public UniTask ShowDefaultScreen();

        void SetDefaultScreen<TScreenType>(TScreenType screenView) where TScreenType : Type;
    }
}