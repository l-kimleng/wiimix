using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Windows;
using WiiMix.Wpf.ViewModels;
using WiiMix.Wpf.Views;

namespace WiiMix.Wpf
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<ShellView>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (ShellView) this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<object, ShellViewModel>();
        }
    }
}
