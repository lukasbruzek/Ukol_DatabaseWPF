using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Ukol_DatabaseWPF;

public partial class SubjectListPage : Page
{
    private DatabaseManager databaseManager;

    public ObservableCollection<Subject> Subjects { get; set; }

    public SubjectListPage()
    {
        InitializeComponent();
        databaseManager = new DatabaseManager();
        Subjects = new ObservableCollection<Subject>();
        LoadSubjects();
    }

    public void LoadSubjects()
    {
        try
        {
            Subjects.Clear();
            foreach (var subject in databaseManager.GetSubjects())
            {
                Subjects.Add(subject);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading subjects: " + ex.Message);
        }

        DataContext = this;
    }

    private void AddSubject_Click(object sender, RoutedEventArgs e)
    {
        CreateSubjectPage createSubjectPage = new CreateSubjectPage(this);
        createSubjectPage.Return += CreateSubjectPage_Return;
        NavigationService.Navigate(createSubjectPage);
    }

    private void CreateSubjectPage_Return(object sender, EventArgs e)
    {
        LoadSubjects();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        LoadSubjects();
    }

    private void UpdateSubject_Click(object sender, RoutedEventArgs e)
    {
        Subject selectedSubject = (Subject)dataGrid.SelectedItem;

        if (selectedSubject != null)
        {
            UpdateSubjectPage updateSubjectPage = new UpdateSubjectPage(selectedSubject, this);
            updateSubjectPage.Return += UpdateSubjectPage_Return;
            NavigationService.Navigate(updateSubjectPage);
        }
        else
        {
            MessageBox.Show("Please select a subject to update.");
        }
    }

    private void UpdateSubjectPage_Return(object sender, EventArgs e)
    {
        LoadSubjects(); 
    }
}
