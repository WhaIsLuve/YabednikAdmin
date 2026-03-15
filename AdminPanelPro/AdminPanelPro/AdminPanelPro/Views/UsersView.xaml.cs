
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using AdminPanelPro.ViewModels;

namespace AdminPanelPro.Views;

public partial class UsersView : UserControl
{
    public UsersView()
    {
        InitializeComponent();
        DataContext = App.AppHost.Services.GetRequiredService<UsersViewModel>();
    }
}
