using System;
using System.Windows.Controls;
using System.Windows;

public partial class CreateSubjectPage : Page
{
    public event EventHandler Return;

    private DatabaseManager databaseManager;
    private SubjectListPage subjectListPage;

    public CreateSubjectPage(SubjectListPage subjectListPage)
    {
        InitializeComponent();
        this.subjectListPage = subjectListPage;
        databaseManager = new DatabaseManager();
    }

    private void CreateSubject_Click(object sender, RoutedEventArgs e)
    {
        string subjectName = txtSubjectName.Text;
        string subjectClass = txtSubjectClass.Text;

        if (subjectName == "" || subjectClass == "")
        {
            MessageBox.Show("Please enter a valid parameters.");
            return;
        }

        try
        {
            databaseManager.AddSubject(subjectName, subjectClass);
            MessageBox.Show("Subject added successfully.");

            Return?.Invoke(this, EventArgs.Empty);

            NavigationService.GoBack();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error adding subject: " + ex.Message);
        }
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }
}
