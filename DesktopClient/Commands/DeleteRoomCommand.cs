﻿using System;
using System.Windows.Input;
using DesktopClient.ViewModel;

namespace DesktopClient.Commands
{
    class DeleteRoomCommand :ICommand
    {
        private readonly CheckInManagementViewModel viewModel;

        public DeleteRoomCommand(CheckInManagementViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.DeleteRoomAction();
        }

        public event EventHandler CanExecuteChanged;
    }
}
