using EyeXFramework.Wpf;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.Windows;
using UBTalker.Services;

namespace UBTalker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// 
    /// IMPORTANT NOTE ON UNITY:
    ///     By default, Unity's RegisterType method registers only a type,
    ///     and when performing Injection, will create a new instance of
    ///     the class. This means that two separate views using the same
    ///     service will get two different instances of it, and this is
    ///     especially bad for the MainWindow, as the second instance will
    ///     prevent the application from shutting down. To avoid this and
    ///     force Unity to provide singletons, a ContainerControlledLifetimeManager
    ///     should be used.
    /// </summary>
    public partial class App : Application
    {
        private IUnityContainer _container;
        private WpfEyeXHost _eyeXHost;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Start EyeXHost
            _eyeXHost = new WpfEyeXHost();
            //_eyeXHost.Start();

            // Setup Unity Container
            _container = new UnityContainer();
            RegisterSingleton<IMainWindow, MainWindow>();
            RegisterServices();

            // Set up Microsoft Service Locator to use Unity
            // Service instances can be requested using ServiceLocator.Current.GetInstance<Type>()
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(_container));

            // Run main window and open initial page
            var mainWindow = _container.Resolve<MainWindow>();
            mainWindow.Show();
            mainWindow.SetPage(new Uri("Views/HubView.xaml", UriKind.Relative));

#if DEBUG
            mainWindow.Title += " (DEBUG MODE) - Be sure to build in release mode for use";
#endif
        }
        
        /// <summary>
        /// Register a singleton
        /// </summary>
        /// <typeparam name="InterfaceType"></typeparam>
        /// <typeparam name="ImplementationType"></typeparam>
        private void RegisterSingleton<InterfaceType, ImplementationType>()
            where ImplementationType : InterfaceType
        {
            _container.RegisterType<InterfaceType, ImplementationType>(new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Registers all services with Unity
        /// All new services should be registered here
        /// </summary>
        private void RegisterServices()
        {
            RegisterSingleton<IDataStoreService, DataStoreService>();
            RegisterSingleton<IPhraseService, PhraseService>();
            RegisterSingleton<ICallLightService, CallLightService>();
            RegisterSingleton<IClientService, ClientService>();
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _eyeXHost.Dispose();
            _container.Dispose();
        }
    }
}
