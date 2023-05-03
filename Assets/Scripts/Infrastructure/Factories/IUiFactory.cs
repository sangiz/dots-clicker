using Assets.Scripts.UI.Widgets;

namespace Assets.Scripts.Infrastructure.Factories
{
    public interface IUiFactory
    {
        ConnectablePointWidget CreateConnectablePointWidget();
        LevelSelectionButton CreateLevelSelectionButton();
    }
}