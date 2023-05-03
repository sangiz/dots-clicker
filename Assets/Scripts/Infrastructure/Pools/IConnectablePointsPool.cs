using Assets.Scripts.UI.Widgets;

namespace Assets.Scripts.Infrastructure.Pools
{
    public interface IConnectablePointsPool
    {
        ConnectablePointWidget Take();
        void Put(ConnectablePointWidget intoPool);
    }
}
