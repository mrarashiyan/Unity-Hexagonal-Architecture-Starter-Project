using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Application.Ports.Screen;
using Project.Application.Ports.Services;
using Project.Infrastructure.Base;
using UnityEngine;

namespace Project.Infrastructure.Services
{
    public class UserInterfaceService : BaseService, IUserInterfaceService
    {
        private Dictionary<Type, IScreenView> _screens = new();
        private IScreenView _loadingOverlay;
        private Type _defaultScreen;

        public void AddScreen(IScreenView screen)
        {
            _screens.Add(screen.GetType(), screen);
        }

        public void RemoveScreen(IScreenView screen)
        {
            _screens.Remove(screen.GetType());
        }

        public void AddLoadingOverlay(IScreenView screen)
        {
            _loadingOverlay = screen;
        }

        public async UniTask ShowScreen<TScreenType>(TScreenType screenType) where TScreenType : Type
        {
            _screens.TryGetValue(screenType, out IScreenView screenView);
            if (screenView != null)
            {
                await screenView.ShowScreen();
            }
            else
                throw new Exception($"Screen {screenType} not found");
        }

        public async UniTask HideScreen<TScreenType>(TScreenType screenType) where TScreenType : Type
        {
            _screens.TryGetValue(screenType, out IScreenView screenView);
            if (screenView != null)
            {
                await screenView.HideScreen();
            }
            else
                throw new Exception($"Screen {typeof(IScreenView)} not found");
        }

        public async UniTask SwitchScreen<TScreenType>(TScreenType from, TScreenType to) where TScreenType : Type
        {
            await ShowLoadingOverlay();

            if (from != null)
                await HideScreen(from);

            if (to != null)
                await ShowScreen(to);

            await ShowLoadingOverlay();
        }

        public async UniTask ShowLoadingOverlay()
        {
            if (_loadingOverlay != null)
                await _loadingOverlay.ShowScreen();
        }

        public async UniTask HideLoadingOverlay()
        {
            if (_loadingOverlay != null)
                await _loadingOverlay.HideScreen();
        }

        public void SetDefaultScreen<TScreenType>(TScreenType screenType) where TScreenType : Type
        {
            _defaultScreen = screenType;
        }

        public async UniTask ShowDefaultScreen()
        {
            await ShowScreen(_defaultScreen);
        }


        protected override async UniTask InitializeService()
        {
        }
    }
}