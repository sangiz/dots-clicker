using Assets.Scripts.Infrastructure.Factories;
using Assets.Scripts.UI.Widgets;
using IgnSDK.Infrastructure.Pools;

namespace Assets.Scripts.Infrastructure.Pools
{
    public class ConnectablePointsPool : AbstractPool<ConnectablePointWidget>, IConnectablePointsPool
    {
        private readonly IUiFactory uiFactory;

        public ConnectablePointsPool(int initialItemsCount, IUiFactory uiFactory) : base(initialItemsCount)
        {
            this.uiFactory = uiFactory;
            ParentName = nameof(ConnectablePointsPool);

            Populate();
        }

        public override ConnectablePointWidget Take()
        {
            if (PooledItems.Count <= 0)
            {
                return null;
            }

            var uiItem = PooledItems.Dequeue();
            uiItem.gameObject.SetActive(true);

            uiItem.OnDisabled += OnDisable;

            return uiItem;
        }

        public void OnDisable(ConnectablePointWidget connectablePointWidget)
        {
            connectablePointWidget.OnDisabled -= OnDisable;
            Put(connectablePointWidget);
        }

        protected override ConnectablePointWidget CreateNewItem() => uiFactory.CreateConnectablePointWidget();
    }
}
