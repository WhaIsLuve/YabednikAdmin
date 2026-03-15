
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AdminPanelPro.Models;
using AdminPanelPro.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AdminPanelPro.ViewModels;

public partial class EntitiesViewModel : ObservableObject
{
    private readonly ApiService _api;
    
    public List<string> EntityTypes { get; } =
    [
        "Дисциплина",
        "Аудитория",
        "Группа"
    ];

    public Dictionary<string, string> EntitiesType = new Dictionary<string, string>()
    {
        {"Дисциплина", "Discipline"},
        {"Аудитория", "Auditorium"},
        {"Группа", "StudentsGroup"},
        {"Discipline", "Дисциплина"},
        {"Auditorium", "Аудитория"},
        {"StudentsGroup", "Группа"},
    };

    [ObservableProperty]
    private ObservableCollection<ScorableEntity> entities = new();

    [ObservableProperty]
    private ScorableEntity? selectedEntity;

    public EntitiesViewModel(ApiService api)
    {
        _api = api;
        _ = Load();
    }

    async Task Load()
    {
        var data = await _api.GetEntities();
        if (data == null) return;

        Entities.Clear();
        foreach (var e in data)
        {
            e.ScorableEntityType = e.ScorableEntityType is null ? null : EntitiesType[e.ScorableEntityType] ;
            Entities.Add(e);
        }
    }

    [RelayCommand]
    async Task Create()
    {
        if (SelectedEntity == null) return;

        await _api.CreateEntity(new
        {
            name = SelectedEntity.Name,
            scorableEntityType = SelectedEntity.ScorableEntityType is null ? null : EntitiesType[SelectedEntity.ScorableEntityType]
        });

        await Load();
    }

    [RelayCommand]
    async Task Update()
    {
        if (SelectedEntity == null) return;

        await _api.UpdateEntity(SelectedEntity.Id, new
        {
            name = SelectedEntity.Name,
            scorableEntityType = SelectedEntity.ScorableEntityType is null ? null : EntitiesType[SelectedEntity.ScorableEntityType]
        });

        await Load();
    }

    [RelayCommand]
    async Task Delete()
    {
        if (SelectedEntity == null) return;

        await _api.DeleteEntity(SelectedEntity.Id);
        await Load();
    }
}
