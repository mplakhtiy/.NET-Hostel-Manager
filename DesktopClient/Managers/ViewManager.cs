﻿using System;
using System.Linq;
using System.Windows;
using DesktopClient.EventArgsExtenctions;
using DesktopClient.Service;
using DesktopClient.View;

namespace DesktopClient.Managers
{
    internal class ViewManager
    {
        private LoginWindow loginWindow;
        private CheckInService checkInService;
        private BedroomService bedroomService;

        public ViewManager(LoginWindow loginWindow)
        {
            this.loginWindow = loginWindow;
            loginWindow.Show();
            checkInService = new CheckInService();
            bedroomService = new BedroomService();
            subscribeEvents();
        }

        private void subscribeEvents()
        {
            Managers.EventManager.UserLoggedIn += onUserLoggedIn;
            Managers.EventManager.SaveCheckInButtonPressed += onSaveCheckInButtonPressed;
            Managers.EventManager.SaveNewBedroomButtonPressed += onSaveNewBedroomButtonPressed;
            Managers.EventManager.CreateNewBedroomButtonPressed += onCreateNewBedroomButtonPressed;
            Managers.EventManager.DeleteBedroomButtonPressed += onDeleteBedroomButtonPressed;
            Managers.EventManager.UpdateBedroomButtonPressed += onUpdateBedroomButtonPressed;
            Managers.EventManager.DeleteCheckInButtonPressed += onDeleteCheckInButtonPressed;
        }

        private async void onDeleteCheckInButtonPressed(object source, CheckInEventArgs eventArgs)
        {
            await checkInService.DoCheckOutAsync(eventArgs.CheckIn);
        }

        private async void onDeleteBedroomButtonPressed(object source, BedroomEventArgs eventArgs)
        {
            await bedroomService.RemoveAsync(eventArgs.Bedroom);
        }

        private void onUpdateBedroomButtonPressed(object source, BedroomEventArgs eventArgs)
        {
            new BedroomEditorWindow(eventArgs).Show();
        }

        private async void onSaveCheckInButtonPressed(object source,CheckInEventArgs eventArgs)
        {
           await checkInService.CreateAsync(eventArgs.CheckIn);
        }

        private async void onSaveNewBedroomButtonPressed(object source, BedroomEventArgs eventArgs)
        {
            await bedroomService.CreateOrUpdateAsync(eventArgs.Bedroom);
            foreach (Window window in Application.Current.Windows.Cast<Window>().Where(window => window.IsActive))
            {
                window.Close();
            }
        }

        private void onCreateNewBedroomButtonPressed(object source,EventArgs eventArgs)
        {
            new BedroomEditorWindow().Show();
        }
        
        private void onUserLoggedIn(object source, UserEventArgs eventArgs)
        {
            new CheckInManagementWindow(eventArgs).Show();
            loginWindow.Close();
        }
    }
}