using System;
using System.Formats.Asn1;
using System.Windows;
using System.Windows.Controls;
using Ukol_DatabaseWPF;

    public partial class UpdateStudentPage : Page
    {
        private Student studentToUpdate;
        private StudentListPage parentPage;

        public event EventHandler Return;

        public UpdateStudentPage(Student student, StudentListPage parent)
        {
            InitializeComponent();
            studentToUpdate = student;
            parentPage = parent;

            txtFirstName.Text = studentToUpdate.FirstName;
            txtLastName.Text = studentToUpdate.LastName;
            txtAge.Text = studentToUpdate.Age.ToString();
            txtClass.Text = studentToUpdate.Class;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                    string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtAge.Text) ||
                    string.IsNullOrWhiteSpace(txtClass.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                studentToUpdate.FirstName = txtFirstName.Text;
                studentToUpdate.LastName = txtLastName.Text;
                studentToUpdate.Age = int.Parse(txtAge.Text);
                studentToUpdate.Class = txtClass.Text;

                DatabaseManager databaseManager = new DatabaseManager();
                databaseManager.UpdateStudent(studentToUpdate);

                OnReturn();

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating student: " + ex.Message);
            }
        }

        protected virtual void OnReturn()
        {
            Return?.Invoke(this, EventArgs.Empty);
        }
    }