using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class TaskItem : INotifyPropertyChanged
{
    public string? Id { get; set; } // Unique identifier for each task
    public string? TaskName { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }

    private bool isCompleted;
    public bool IsCompleted 
    { 
        get => isCompleted;
        set
        {
            if (isCompleted != value)
            {
                isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

