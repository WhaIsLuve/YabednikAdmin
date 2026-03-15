
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using AdminPanelPro.ViewModels;

namespace AdminPanelPro.Views;

public partial class EntitiesView : UserControl
{
    public EntitiesView()
    {
        InitializeComponent();
        DataContext = App.AppHost.Services.GetRequiredService<EntitiesViewModel>();
    }
}
