using System;
using System.Collections.Generic;

namespace OOPL.Forms.UserActions
{
    public class ControllersManager
    {
        private readonly Dictionary<Type, IUserController> _controllers = new Dictionary<Type, IUserController>();

        public void Add<T>(T controller) where T : IUserController
        {
            _controllers.Add(typeof(T), controller);
        }

        private IUserController _current;

        public IUserController Current
        {
            get { return _current ?? (_current = new NullUserContrller()); }
            private set { _current = value; }
        }

        public void Set<T>() where T : IUserController
        {
            if (Current.GetType() == typeof (T))
            {
                return;
            }

            Current = (T)_controllers[typeof(T)];
        }
    }
}