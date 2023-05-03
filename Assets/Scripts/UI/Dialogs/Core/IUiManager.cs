using System;

namespace Assets.Scripts.UI.Dialogs.Core
{
    public interface IUiManager
    {
        void AddDialog(Dialog dialog);
        T GetDialog<T>() where T : Dialog;
        void ForwardDialog<T>(Dialog exitToDialog, Action callBack = null) where T : Dialog;
        void OpenDialog<T>(Action callBack = null) where T : Dialog;
        void CloseDialog<T>(Action callBack = null) where T : Dialog;
    }
}
