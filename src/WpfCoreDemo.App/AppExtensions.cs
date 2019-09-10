using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace WpfCoreDemo.App
{
    public static class AppExtensions
    {
        public static TViewModel CreateViewModel<TViewModel>(this Application app)
        {
            return ((App)app).ServiceProvider.GetRequiredService<TViewModel>();
        }
    }
}
