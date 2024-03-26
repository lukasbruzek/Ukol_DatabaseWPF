using System.Windows.Controls;
using System.Windows;

public partial class CreateStudentPage : Page
{
    public event EventHandler Return; 

    private DatabaseManager databaseManager;
    private StudentListPage studentListPage; 

    public CreateStudentPage(StudentListPage studentListPage)
    {
        InitializeComponent();
        this.studentListPage = studentListPage; 
        databaseManager = new DatabaseManager();
    }

    private void CreateStudent_Click(object sender, RoutedEventArgs e)
    {
        if(txtFirstName.Text == "" || txtLastName.Text == "" || txtClass.Text == "" || txtAge.Text == 0.ToString())
        {
            MessageBox.Show("Please enter a valid parameters.");
            return;
        }
        string firstName = txtFirstName.Text;
        string lastName = txtLastName.Text;
        int age;
        if (int.TryParse(txtAge.Text, out age))
        {
            string className = txtClass.Text;
            try
            {
                databaseManager.AddStudent(firstName, lastName, age, className);
                MessageBox.Show("Student added successfully.");

                Return?.Invoke(this, EventArgs.Empty);

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding student: " + ex.Message);
            }
        }
        else
        {
            MessageBox.Show("Please enter a valid age.");
        }
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }
}