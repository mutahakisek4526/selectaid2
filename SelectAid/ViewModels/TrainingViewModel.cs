using System;

namespace SelectAid.ViewModels;

public class TrainingViewModel : ViewModelBase
{
    private readonly MainViewModel _main;
    private string _status = "準備完了";

    public TrainingViewModel(MainViewModel main)
    {
        _main = main;
    }

    public string Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }

    public void StartEyeTraining() => Status = $"視線練習開始 {DateTime.Now:T}";
    public void StartGyroTraining() => Status = $"ジャイロ練習開始 {DateTime.Now:T}";
    public void StartScanTraining() => Status = $"スキャン練習開始 {DateTime.Now:T}";
}
