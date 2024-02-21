using Microsoft.Maui.Controls;
using ToDo.ViewModels;


namespace ToDo
{
    public partial class MainPage : ContentPage
    {
        private readonly TasksViewModel _tasksViewModel;

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await _tasksViewModel.SaveTasksAsync();
        }


        public MainPage(TasksViewModel tasksViewModel)
        {
            InitializeComponent();
            _tasksViewModel = tasksViewModel;
            BindingContext = _tasksViewModel;
        }


        public async void OnAddTaskClicked(object sender, EventArgs e)
        {
            var newTaskPage = new NewTaskPage();
            newTaskPage.OnSaveTask = async task =>
            {
                _tasksViewModel.Tasks.Add(task);
                await _tasksViewModel.SaveTasksAsync();
            };

            await Navigation.PushAsync(newTaskPage);
        }

        private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.BindingContext is TaskItem task)
            {
                // Update the task completion status
                task.IsCompleted = checkBox.IsChecked;
                // Optionally, save the updated task to your data store
            }
        }

        private async void OnTaskTapped(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var task = (TaskItem)layout.BindingContext;
            await Navigation.PushAsync(new NewTaskPage(task));
        }

    }
}
