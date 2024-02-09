using System.Collections.ObjectModel;
using System.Threading.Tasks; // Add this for Task
using System.Windows.Input;
using ToDo;

public class TasksViewModel
{
    private readonly TaskDataService _taskDataService;

    public ObservableCollection<TaskItem> Tasks { get; private set; } = new ObservableCollection<TaskItem>();

    public ICommand DeleteCommand { get; }

    public TasksViewModel(TaskDataService taskDataService)
    {
        _taskDataService = taskDataService;
        LoadTasksAsync().ConfigureAwait(false); // Load tasks asynchronously

        DeleteCommand = new Command<TaskItem>(DeleteTask);
    }

    private async Task LoadTasksAsync()
    {
        // Try to load tasks from the data service
        var loadedTasks = await _taskDataService.LoadTasksAsync();

        if (loadedTasks != null && loadedTasks.Count > 0)
        {
            // Clear the current tasks and load the persisted tasks
            Tasks.Clear();
            foreach (var task in loadedTasks)
            {
                Tasks.Add(task);
            }
        }
        else
        {
            // No tasks found, initialize with dummy data if necessary
            InitializeWithDummyData();
        }
    }

    private void InitializeWithDummyData()
    {
        Tasks.Add(new TaskItem { TaskName = "Sample Task 1", Description = "Description", DueDate = DateTime.Now });
        Tasks.Add(new TaskItem { TaskName = "Sample Task 2", Description = "Description", DueDate = DateTime.Now });
        // More dummy data initialization...
    }

    public async Task SaveTasksAsync()
    {
        await _taskDataService.SaveTasksAsync(Tasks);
    }

    private async void DeleteTask(TaskItem task)
    {
        bool isUserSure = await Application.Current.MainPage.DisplayAlert(
            "Confirm Delete",
            "Are you sure you want to delete this task?",
            "Yes", "No");

        if (isUserSure && task != null)
        {
            // Perform UI-related actions on the main thread
            Device.BeginInvokeOnMainThread(() =>
            {
                Tasks.Remove(task);
            });

            // Save the changes
            await SaveTasksAsync(); // Awaiting the save operation
        }
    }
}
