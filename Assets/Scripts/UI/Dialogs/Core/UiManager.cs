using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Dialogs.Core
{
    public class UiManager : IUiManager
    {
        private readonly Dictionary<Type, Dialog> dialogs = new Dictionary<Type, Dialog>();

        public void AddDialog(Dialog dialog)
        {
            dialogs.Add(dialog.GetType(), dialog);
        }

        public T GetDialog<T>() where T : Dialog
        {
            if (dialogs.TryGetValue(typeof(T), out Dialog dialog))
            {
                return dialog as T;
            }

            Debug.LogError($"Dialog { typeof(T) } not found");

            return default;
        }

        public void ForwardDialog<T>(Dialog exitToDialog, Action callBack = null) where T : Dialog
        {
            GetDialog<T>().Forward(exitToDialog, callBack);
        }

        public void OpenDialog<T>(Action callBack = null) where T : Dialog
        {
            GetDialog<T>().Open(callBack);
        }

        public void CloseDialog<T>(Action callBack = null) where T : Dialog
        {
            GetDialog<T>().Close(callBack);
        }
    }
}
