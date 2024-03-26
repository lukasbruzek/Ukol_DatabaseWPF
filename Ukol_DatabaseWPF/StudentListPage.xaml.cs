using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Ukol_DatabaseWPF;

public partial class StudentListPage : Page
{
    private DatabaseManager databaseManager;

    public ObservableCollection<Student> Students { get; set; }

    public StudentListPage()
    {
        InitializeComponent();
        databaseManager = new DatabaseManager();
        Students = new ObservableCollection<Student>();
        LoadStudents();
    }

    public void LoadStudents()
    {
        try
        {
            Students.Clear();
            foreach (var student in databaseManager.GetStudents())
            {
                Students.Add(student);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading students: " + ex.Message);
        }

        DataContext = this;
    }

    private void AddStudent_Click(object sender, RoutedEventArgs e)
    {
        CreateStudentPage createStudentPage = new CreateStudentPage(this);
        createStudentPage.Return += CreateStudentPage_Return;
        NavigationService.Navigate(createStudentPage);
    }

    private void CreateStudentPage_Return(object sender, EventArgs e)
    {
        LoadStudents(); // Aktualizuje seznam studentů
    }

    private void UpdateStudent_Click(object sender, RoutedEventArgs e)
    {
        Student selectedStudent = (Student)dataGrid.SelectedItem;
        if (selectedStudent != null)
        {
            UpdateStudentPage updateStudentPage = new UpdateStudentPage(selectedStudent, this);
            updateStudentPage.Return += UpdateStudentPage_Return;
            NavigationService.Navigate(updateStudentPage);
        }
        else
        {
            MessageBox.Show("Please select a student to update.");
        }
    }

    private void UpdateStudentPage_Return(object sender, EventArgs e)
    {
        LoadStudents(); // Aktualizuje seznam studentů
    }
}
