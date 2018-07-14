using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Services.Dialogs
{
    public interface IDialogsService
    {
        void ShowDialog();
        void HideDialog();
        Task ShowMessage(string title, string message);
    }
}
