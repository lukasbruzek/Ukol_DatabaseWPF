using System;
using System.Formats.Asn1;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Ukol_DatabaseWPF;

    public partial class UpdateSubjectPage : Page
    {
        private Subject subjectToUpdate;
        private SubjectListPage parentPage;

        public event EventHandler Return;

        public UpdateSubjectPage(Subject subject, SubjectListPage parent)
        {
            InitializeComponent();
            subjectToUpdate = subject;
            parentPage = parent;

            txtName.Text = subjectToUpdate.Name;
            txtClass.Text = subjectToUpdate.Class;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtClass.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                subjectToUpdate.Name = txtName.Text;
                subjectToUpdate.Class = txtClass.Text;

                DatabaseManager databaseManager = new DatabaseManager();
                databaseManager.UpdateSubject(subjectToUpdate);

                OnReturn();

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating subject: " + ex.Message);
            }
        }

        protected virtual void OnReturn()
        {
            Return?.Invoke(this, EventArgs.Empty);
        }
    }