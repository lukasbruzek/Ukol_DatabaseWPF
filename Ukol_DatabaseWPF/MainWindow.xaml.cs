using System.Windows;

namespace Ukol_DatabaseWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new StudentListPage();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NavigateToStudents_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new StudentListPage());
        }

        private void NavigateToSubjects_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SubjectListPage());
        }

        private void NavigateToAssignSubject_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AssignSubjectPage());
        }

        private void NavigateToDisplayStudentsBySubjectPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DisplayStudentsBySubjectPage());
        }
    }
}
