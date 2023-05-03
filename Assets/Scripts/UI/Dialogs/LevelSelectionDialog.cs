using Assets.Scripts.UI.Dialogs.Core;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Dialogs
{
    public class LevelSelectionDialog : Dialog
    {
        public Transform buttonsHolder;



        [Inject]
        private void Construct(IUiManager uiManager)
        {
            uiManager.AddDialog(this);
        }

        private void OnlevelStarted()
        {
            Close();
        }
    }
}
