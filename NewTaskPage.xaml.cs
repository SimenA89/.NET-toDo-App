namespace ToDo;

public partial class NewTaskPage : ContentPage
{
    // Delegate declaration. This delegate allows the MainPage (or any other page)
    // to define an action that will occur when a new task is saved.
    public Action<TaskItem>? OnSaveTask { get; set; }
    public NewTaskPage(TaskItem taskItem)
    {
        InitializeComponent();
        BindingContext = taskItem;
        // Additional initialization, if any
    }

    public NewTaskPage()
    {
    }

    // Event handler for the Save button click event.
    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        // Creating a new TaskItem object from the data entered by the user.
        var newTask = new TaskItem
        {
            TaskName = TaskNameEntry.Text, // Task name from the text entry field.
            Description = DescriptionEditor.Text, // Description from the text editor field.
            DueDate = DueDatePicker.Date, // Due date from the date picker.
            IsCompleted = false // New tasks are initially marked as not completed.
        };

        if (string.IsNullOrWhiteSpace(TaskNameEntry.Text))
        {
            // Show an error message or alert to the user
            return;
        }

        // Invoking the OnSaveTask delegate, if it's been set by the calling page.
        // This action adds the new task to the tasks collection in the MainPage.
        OnSaveTask?.Invoke(newTask);

        // Navigate back to the MainPage after the task is saved.
        await Navigation.PopAsync();
    }
}
