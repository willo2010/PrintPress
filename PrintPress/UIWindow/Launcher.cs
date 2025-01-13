using PrintPress.Controller;
using PrintPress.Controller.Data;
using PrintPress.Data;
using PrintPress.Data.Enum;
using PrintPress.UI;
using PrintPress.UI.Enum;
using PrintPress.UIService;
using System.Net.Mail;

namespace PrintPress
{
    internal partial class Launcher : Form
    {  
        private EmployeeData? activeUser;
        private bool adminOverride = false;

        private List<ClientWindow> activeClients = new List<ClientWindow>();

        public Launcher()
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += keyDownEvent;
        }

        //// Event handelling

        private void LogInButton_Click(object sender, EventArgs e)
        {
            logInButton.Enabled = false;

            if (activeUser == null)
            {
                ProcessLogIn();
            }
            else
            {
                ProcessLogOut();
            }

            logInButton.Enabled = true;
        }

        private void LaunchButton_Click(object sender, EventArgs e)
        {
            LaunchClient();
        }

        private void adminLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if ((activeUser == null || !activeUser.HasClearance(Department.Admin)) && adminOverride == false)
            {
                MessageBox.Show("Present valid admin credentials.");
                return;
            }
            Admin adminForm = new Admin();
            adminForm.ShowDialog();
        }

        private void keyDownEvent(object? sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.KeyValue == 67) // c key
            {
                adminOverride = !adminOverride;
            }
        }

        //// 

        private void OnClientClosing(object? sender, FormClosingEventArgs fcArgs)
        {
            if (sender == null)
            {
                RemoveNullForms();
                return;
            }

            ClientWindow clientWindow = (ClientWindow)sender;
            clientWindow.FormClosing -= OnClientClosing;

            if (activeClients.Contains(clientWindow))
            {
                activeClients.Remove(clientWindow);
            }
        }

        //// Helper functions

        private void LaunchClient()
        {
            if (!DataTools.DepartmentString(clientComboBox.Text, out Department department) ||
                activeUser == null || !activeUser.HasClearance(department))
            {
                MessageBox.Show("Inavlid department selection");
                return;
            }

            if (IsClientOpen(department))
            {
                MessageBox.Show("Client window is already open.");
                return;
            }

            ClientWindow window;

            switch (department)
            {
                case Department.Journalism:
                    JournalismService js = new JournalismService(activeUser);
                    window = new Journalism(js);
                    break;
                case Department.Marketing:
                    MarketingService ms = new MarketingService(activeUser);
                    window = new Marketing(ms);
                    break;
                default:
                    return;
            }

            activeClients.Add(window);
            window.FormClosing += OnClientClosing;
            window.Show();
        }

        private void RemoveNullForms()
        {
            for (int i = 0; i < activeClients.Count; i++)
            {
                if (activeClients[i] == null)
                {
                    activeClients.RemoveAt(i--);
                }
            }
        }

        private bool IsClientOpen(Department department)
        {
            ClientWindow[] typeClients = activeClients.Where(x => x.Type == department).ToArray();
            if (typeClients.Length > 0)
            {
                return true;
            }
            return false;
        }

        private void ProcessLogIn()
        {
            bool validFormat = true;
            string statusMessage = string.Empty;

            MailAddress mailAddress;
            string password = passwordText.Text;

            if (!ValidEmailFormat(emailText.Text, out mailAddress))
            {
                validFormat = false;
                statusMessage = "invalid email";
            }
            else if (!ValidPasswordFormat(password))
            {
                validFormat = false;
                statusMessage = "invalid password";
            }

            if (!validFormat)
            {
                UpdateStatusLabel(loginStatusLabel, statusMessage, StatusLabelColour.Red);
                return;
            }

            ClassifiedDataController classifiedDC = ClassifiedDataController.Instance;
            bool valid = classifiedDC.ValidateCredentials(mailAddress, password, out string message, out EmployeeData employee);

            if (!valid)
            {
                UpdateStatusLabel(loginStatusLabel, message, StatusLabelColour.Red);
                return;
            }

            activeUser = employee;

            UpdateStatusLabel(loginStatusLabel, "Logged in as " + mailAddress.User, StatusLabelColour.Green);
            UpdateStatusLabel(clientStatusLabel, "select client", StatusLabelColour.Grey);
            emailText.Enabled = false;
            passwordText.Enabled = false;
            logInButton.Text = "Log Out";
            PopulateClientCombobox();
        }

        private void ProcessLogOut()
        {
            activeUser = null;

            UpdateStatusLabel(loginStatusLabel, "enter credentials", StatusLabelColour.Grey);
            UpdateStatusLabel(clientStatusLabel, "log in to launch client", StatusLabelColour.Grey);
            emailText.Enabled = true;
            passwordText.Enabled = true;
            logInButton.Text = "Log In";
            passwordText.Text = string.Empty;
            clientComboBox.Items.Clear();
            launchButton.Enabled = false;
        }

        private void UpdateStatusLabel(Label label, string message, StatusLabelColour colour)
        {
            label.Text = message;
            switch (colour)
            {
                default:
                case StatusLabelColour.Grey:
                    label.ForeColor = SystemColors.ControlDarkDark; break;
                case StatusLabelColour.Red:
                    label.ForeColor = Color.Firebrick; break;
                case StatusLabelColour.Green:
                    label.ForeColor = Color.Green; break;
            }
        }

        private void PopulateClientCombobox()
        {
            clientComboBox.Items.Clear();
            if (activeUser == null) return;

            foreach (Department department in activeUser.clearance)
            {
                if (department != Department.Admin &&
                    DataTools.DepartmentString(department, out string deptStr))
                {
                    clientComboBox.Items.Add(deptStr);
                }
            }

            if (clientComboBox.Items.Count > 0)
            {
                clientComboBox.SelectedIndex = 0;
                launchButton.Enabled = true;
            }
        }

        private bool ValidEmailFormat(string email, out MailAddress mailAddress)
        {
            return MailAddress.TryCreate(email, out mailAddress);
        }

        private bool ValidPasswordFormat(string passsword)
        {
            if (string.IsNullOrEmpty(passsword) ||
                passsword.Length < 3)
            {
                return false;
            }
            return true;
        }
    }
}
