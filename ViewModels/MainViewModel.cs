using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoListApp.Models;
using ToDoListApp.Commands;
namespace ToDoListApp.ViewModels
{
    class MainViewModel
    {
        public ObservableCollection<TaskModel> Tasks { get; set; } = new();

        public ICommand AddTaskCommand { get; }
        public ICommand RemoveTaskCommand { get; }

        public MainViewModel()
        {
            AddTaskCommand = new RelayCommand(AddTask);
            RemoveTaskCommand = new RelayCommand(RemoveTask);
        }

        private void AddTask(object parameter)
        {
            if (parameter is string taskName && !string.IsNullOrWhiteSpace(taskName))
            {
                Tasks.Add(new TaskModel { TaskName = taskName, IsCompleted = false });
            }
        }
        private void RemoveTask(object parameter) 
        { 
            if (parameter is TaskModel task)
                    Tasks.Remove(task);
        }
    }
}
