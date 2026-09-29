using System;
using eShop.UseCases.PluginInterface.StateStore;

namespace eShop.StateStore.DI
{
    public class StateStoreBase : IStateStore
    {
        protected Action? listeners;

        public void AddStateChangeListener(Action listener)
        {
            listeners += listener;
        }

        public void RemoveStateChangeListener(Action listener)
        {
            listeners -= listener;
        }

        public void BroadcastStateChange()
        {
            listeners?.Invoke();
        }
    }
}