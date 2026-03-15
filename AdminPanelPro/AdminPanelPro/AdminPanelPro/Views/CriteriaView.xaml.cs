
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using AdminPanelPro.ViewModels;

namespace AdminPanelPro.Views;

public partial class CriteriaView : UserControl
{
    public CriteriaView()
    {
        InitializeComponent();
        DataContext = App.AppHost.Services.GetRequiredService<CriteriaViewModel>();
    }
}
