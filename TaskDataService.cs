using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace ToDo
{
    public class TaskDataService
    {
        private string _filePath;

        public TaskDataService()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "tasks.json");
        }

        public async Task SaveTasksAsync(ObservableCollection<TaskItem> tasks)
        {
            try
            {
                string json = JsonSerializer.Serialize(tasks);
                await File.WriteAllTextAsync(_filePath, json);
            }
            catch (Exception ex)
            {
                // Handle or log the exception as necessary

            }
        }

        public async Task<ObservableCollection<TaskItem>> LoadTasksAsync()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return new ObservableCollection<TaskItem>();
                }

                string json = await File.ReadAllTextAsync(_filePath);
                return JsonSerializer.Deserialize<ObservableCollection<TaskItem>>(json) ?? new ObservableCollection<TaskItem>();
            }
            catch (Exception ex)
            {
                // Handle or log the exception as necessary
                return new ObservableCollection<TaskItem>();
            }
        }
    }
    
}
