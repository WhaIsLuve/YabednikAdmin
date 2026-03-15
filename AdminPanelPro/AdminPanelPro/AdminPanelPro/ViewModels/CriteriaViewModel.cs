
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AdminPanelPro.Models;
using AdminPanelPro.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AdminPanelPro.ViewModels;

public partial class CriteriaViewModel : ObservableObject
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
    private ObservableCollection<Criteria> criteria = new();

    [ObservableProperty]
    private Criteria? selectedCriteria;

    public CriteriaViewModel(ApiService api)
    {
        _api = api;
        _ = Load();
    }

    async Task Load()
    {
        var data = await _api.GetCriteria();
        if (data == null) return;

        Criteria.Clear();
        foreach (var c in data)
        {
            c.ScorableEntityType = c.ScorableEntityType is null ? null : EntitiesType[c.ScorableEntityType];
            Criteria.Add(c);
        }
    }

    [RelayCommand]
    async Task Create()
    {
        if (SelectedCriteria == null) return;

        await _api.CreateCriteria(new
        {
            name = SelectedCriteria.Name,
            entityType = SelectedCriteria.ScorableEntityType is null ? null : EntitiesType[SelectedCriteria.ScorableEntityType]
        });

        await Load();
    }

    [RelayCommand]
    async Task Update()
    {
        if (SelectedCriteria == null) return;

        await _api.UpdateCriteria(SelectedCriteria.Id, new
        {
            name = SelectedCriteria.Name,
            entityType = SelectedCriteria.ScorableEntityType is null ? null : EntitiesType[SelectedCriteria.ScorableEntityType]
        });

        await Load();
    }

    [RelayCommand]
    async Task Delete()
    {
        if (SelectedCriteria == null) return;

        await _api.DeleteCriteria(SelectedCriteria.Id);
        await Load();
    }
}
