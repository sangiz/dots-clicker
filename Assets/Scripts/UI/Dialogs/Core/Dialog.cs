using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SF = UnityEngine.SerializeField;

namespace Assets.Scripts.UI.Dialogs.Core
{
    public class Dialog : MonoBehaviour
    {
        [SF] protected Selectable defaultSelection;

        private Dialog exitToDialog = null;
        private Selectable lastSelected = null;

        public virtual void Process(float delta)
        {

        }

        public virtual void Forward(Dialog exitToDialog, Action callback = null)
        {
            exitToDialog.gameObject.SetActive(false);
            gameObject.SetActive(true);

            this.exitToDialog = exitToDialog;

            exitToDialog.lastSelected = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            SelectButton();
            OnOpen();

            callback?.Invoke();
        }

        public void Open(Action callback = null)
        {
            gameObject.SetActive(true);

            SelectButton();

            OnOpen();

            callback?.Invoke();
        }

        protected virtual void OnOpen()
        {
        }

        private void SelectButton()
        {
            if (lastSelected != null)
            {
                lastSelected.Select();
                lastSelected = null;
            }
            else
            {
                defaultSelection?.Select();
            }
        }

        protected virtual void OnClose()
        {
        }

        public void Close(Action callback = null)
        {
            OnClose();
            gameObject.SetActive(false);
            callback?.Invoke();
        }

        public virtual void OnCancel()
        {
            if (!gameObject.activeInHierarchy)
                return;

            Close();

            if (exitToDialog)
            {
                exitToDialog.Open();
            }
        }
    }
}
