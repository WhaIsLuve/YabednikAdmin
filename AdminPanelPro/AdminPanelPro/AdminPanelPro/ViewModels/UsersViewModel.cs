
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AdminPanelPro.Models;
using AdminPanelPro.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AdminPanelPro.ViewModels;

public partial class UsersViewModel : ObservableObject
{
    private readonly ApiService _api;

    [ObservableProperty]
    private ObservableCollection<User> users = new();
    
    public List<string> Roles { get; } =
    [
        "Ученик",
        "Преподаватель",
        "Админ"
    ];

    public Dictionary<string, string> UserRoles = new Dictionary<string, string>()
    {
        {"Ученик", "Student"},
        {"Преподаватель", "Teacher"},
        {"Админ", "Admin"},
        {"Student", "Ученик"},
        {"Teacher", "Преподаватель"},
        {"Admin", "Админ"},
    };

    [ObservableProperty]
    private User? selectedUser;

    public UsersViewModel(ApiService api)
    {
        _api = api;
        _ = Load();
    }

    public async Task Load()
    {
        var data = await _api.GetUsers();
        if (data == null) return;

        Users.Clear();
        foreach (var u in data)
        {
            u.Role = u.Role is null ? null : UserRoles[u.Role];
            Users.Add(u);
        }
    }

    [RelayCommand]
    async Task Create()
    {
        if (SelectedUser == null) return;
        SelectedUser.Role = SelectedUser.Role is null ? null : UserRoles[SelectedUser.Role];
        await _api.CreateUser(SelectedUser);
        await Load();
    }

    [RelayCommand]
    async Task Update()
    {
        if (SelectedUser == null) return;
        SelectedUser.Role = SelectedUser.Role is null ? null : UserRoles[SelectedUser.Role];
        await _api.UpdateUser(SelectedUser);
        await Load();
    }

    [RelayCommand]
    async Task Delete()
    {
        if (SelectedUser?.Email == null) return;
        await _api.DeleteUser(SelectedUser.Email);
        await Load();
    }
}
