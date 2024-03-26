using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Ukol_DatabaseWPF;

public partial class AssignSubjectPage : Page
{
    private DatabaseManager databaseManager;

    public ObservableCollection<Student> Students { get; set; }
    public ObservableCollection<Subject> Subjects { get; set; }

    public AssignSubjectPage()
    {
        InitializeComponent();
        databaseManager = new DatabaseManager();
        LoadData();
    }

    private void LoadData()
    {
        try
        {
            Students = new ObservableCollection<Student>(databaseManager.GetStudents());
            Subjects = new ObservableCollection<Subject>(databaseManager.GetSubjects());
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading data: " + ex.Message);
        }

        DataContext = this;
    }

    private void AssignSubject_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Student selectedStudent = cmbStudents.SelectedItem as Student;
            Subject selectedSubject = cmbSubjects.SelectedItem as Subject;

            if (selectedStudent == null || selectedSubject == null)
            {
                MessageBox.Show("Please select both student and subject.");
                return;
            }

            if (selectedStudent.Class != selectedSubject.Class)
            {
                MessageBox.Show("Student cannot be assigned to the selected subject because they are in different classes.");
                return;
            }

            bool assignmentExists = databaseManager.CheckAssignmentExists(selectedStudent.Id, selectedSubject.Id);
            if (assignmentExists)
            {
                MessageBox.Show("This student is already assigned to the selected subject.");
                return;
            }

            databaseManager.AssignSubjectToStudent(selectedStudent.Id, selectedSubject.Id);
            MessageBox.Show("Subject assigned successfully.");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error assigning subject: " + ex.Message);
        }
    }

}
