using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Ukol_DatabaseWPF;

public partial class DisplayStudentsBySubjectPage : Page
{
    private DatabaseManager databaseManager;

    public ObservableCollection<Subject> Subjects { get; set; }
    public ObservableCollection<Student> Students { get; set; }

    public DisplayStudentsBySubjectPage()
    {
        InitializeComponent();
        databaseManager = new DatabaseManager();
        LoadData();
    }

    private void LoadData()
    {
        try
        {
            Subjects = new ObservableCollection<Subject>(databaseManager.GetSubjects());
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading subjects: " + ex.Message);
        }

        DataContext = this;
    }

    private void ShowStudents_Click(object sender, RoutedEventArgs e)
    {
        Subject selectedSubject = cmbSubjects.SelectedItem as Subject;

        if (selectedSubject == null)
        {
            MessageBox.Show("Please select a subject.");
            return;
        }

        try
        {
            Students = new ObservableCollection<Student>(
            databaseManager.GetStudentsForSubject(selectedSubject.Id)
            );
            countLabel.Content = "Count: " + Students.Count;
            lvStudents.ItemsSource = Students;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading students: " + ex.Message);
        }
    }

    private void RemoveStudentFromSubject_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Student selectedStudent = lvStudents.SelectedItem as Student;
            Subject selectedSubject = cmbSubjects.SelectedItem as Subject;

            if (selectedStudent == null || selectedSubject == null)
            {
                MessageBox.Show("Please select both student and subject.");
                return;
            }

            databaseManager.RemoveStudentFromSubject(selectedStudent.Id, selectedSubject.Id);

            ShowStudents_Click(null, null);

            MessageBox.Show("Student removed from the subject successfully.");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error removing student from subject: " + ex.Message);
        }
    }
}