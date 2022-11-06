using SpaceBattleGame.Contracts.Commands;
using System.Reflection;

namespace IoC
{
    public static class IoC
    {
        private static ThreadLocal<ScopeController> _scopeController = new ThreadLocal<ScopeController>();

        private static readonly object _lockObj = new object();

        public static T Resolve<T>(string cmd, params object[] parameters)
        {
            lock (_lockObj)
            {
                if (_scopeController.Value == null)
                {
                    _scopeController.Value = new ScopeController();
                }

                Assembly assembly = Assembly.GetExecutingAssembly();
                Type type = assembly.GetType(cmd);
                if (type != null)
                {
                    ConstructorInfo ctor = type.GetConstructors().FirstOrDefault();
                    T instance = (T)ctor.Invoke(new object[] { _scopeController.Value, parameters });
                    return instance;
                }
                else
                {
                    Dictionary<string, Func<object[], object>> dependencies = _scopeController.Value.GetDependenciesFromCurrentScope();
                    if (dependencies.TryGetValue(cmd, out var value))
                    {
                        return (T)(value(parameters));
                    }
                    return default(T);
                }
            }
        }
    }
}