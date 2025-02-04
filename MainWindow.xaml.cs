
using System.Reflection;
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
            // Add
           

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