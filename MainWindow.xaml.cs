
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace ToDoListApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Set the image source for app icon
            Uri iconUri = new Uri("pack://application:,,,/Assets/app-icon.png");
            Icon = BitmapFrame.Create(iconUri);
            this.Closing += MainWindow_Closing;

        }

        private void MainWindow_Closing(object? sender, CancelEventArgs e)
        {
            SaveTasks("automatic");
        }

        private void SaveTasksButton_Click(object sender, RoutedEventArgs e)
        {
          SaveTasks("manual");
        }

        private void SaveTasks(string type)
        {
            List<string> tasks = new List<string>();
            foreach (var item in TaskList.Items)
            {
                tasks.Add(item.ToString());
            }

            string json = JsonSerializer.Serialize(tasks);

            string path = "tasks.json";
            File.WriteAllText(path, json);
            if(type == "manual")
                MessageBox.Show("Tasks saved successfully!", "Save Tasks", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoadTasks()
        {
            string path = "tasks.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<string> tasks = JsonSerializer.Deserialize<List<string>>(json); 
                File.Delete(path);
                TaskList.Items.Clear();
                foreach (var task in tasks)
                {
                    TaskList.Items.Add(task);
                }
            }
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string task = TaskInput.Text;
            if (!string.IsNullOrEmpty(task))
            {
                TaskList.Items.Add(task);
                TaskInput.Clear();
                TaskInput.Text = string.Empty;

                TaskInput.Focus();
            }
        }

        private void RemoveTaskTextBlock_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock && textBlock.DataContext is string task)
            {
                TaskList.Items.Remove(task);
            }

        }

        private void LoadTasksButton_Click(object sender, RoutedEventArgs e)
        {
            LoadTasks();
        }

        private void TaskInput_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                AddTaskButton_Click(sender, e);
            } else if (e.Key == Key.Back)
            {
                var selectedItem = TaskList.SelectedItem;
                if (selectedItem != null)
                {
                    TaskList.Items.Remove(selectedItem);
                }
            }
        }

      

    }
}