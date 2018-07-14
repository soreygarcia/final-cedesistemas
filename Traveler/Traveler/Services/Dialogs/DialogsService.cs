using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Services.Dialogs
{
    public class DialogsService : IDialogsService
    {
        public void HideDialog()
        {
            Acr.UserDialogs.UserDialogs.Instance.HideLoading();
        }

        public void ShowDialog()
        {
            Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
        }

        public async Task ShowMessage(string title, string message)
        {
            await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(message, title, "OK");
        }
    }
}
