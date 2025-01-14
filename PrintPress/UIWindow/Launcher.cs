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
    /// <summary>
    /// Represents the main Launcher form, providing user authentication and client window management.
    /// </summary>
    internal partial class Launcher : Form
    {
        #region Fields and Properties

        /// <summary>
        /// Stores the currently logged-in user.
        /// </summary>
        private EmployeeData? activeUser;

        /// <summary>
        /// Indicates whether admin override mode is active.
        /// </summary>
        private bool adminOverride = false;

        /// <summary>
        /// Tracks currently active client windows.
        /// </summary>
        private List<ClientWindow> activeClients = new List<ClientWindow>();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Launcher"/> class and sets up key event handling.
        /// </summary>
        public Launcher()
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += keyDownEvent;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the click event for the Log In button.
        /// </summary>
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

        /// <summary>
        /// Handles the click event for the Launch button.
        /// </summary>
        private void LaunchButton_Click(object sender, EventArgs e)
        {
            LaunchClient();
        }

        /// <summary>
        /// Handles the click event for the Admin label link.
        /// </summary>
        private void adminLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if ((activeUser == null || !activeUser.HasClearance(Department.Admin)) && !adminOverride)
            {
                MessageBox.Show("Present valid admin credentials.");
                return;
            }
            Admin adminForm = new Admin();
            adminForm.ShowDialog();
        }

        /// <summary>
        /// Handles keydown events to toggle admin override.
        /// </summary>
        private void keyDownEvent(object? sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.KeyValue == 67) // 'C' key
            {
                adminOverride = !adminOverride;
            }
        }

        /// <summary>
        /// Handles the closing event for client windows, removing them from the active client list.
        /// </summary>
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Launches a client window based on the selected department.
        /// </summary>
        private void LaunchClient()
        {
            if (!DataTools.DepartmentString(clientComboBox.Text, out Department department) ||
                activeUser == null || !activeUser.HasClearance(department))
            {
                MessageBox.Show("Invalid department selection.");
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

        /// <summary>
        /// Removes any null entries from the active client list.
        /// </summary>
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

        /// <summary>
        /// Checks if a client window for a specific department is already open.
        /// </summary>
        private bool IsClientOpen(Department department)
        {
            return activeClients.Any(x => x.Type == department);
        }

        /// <summary>
        /// Processes the log-in functionality, validating credentials and setting up the session.
        /// </summary>
        private void ProcessLogIn()
        {
            bool validFormat = true;
            string statusMessage = string.Empty;

            if (!ValidEmailFormat(emailText.Text, out MailAddress mailAddress))
            {
                validFormat = false;
                statusMessage = "Invalid email.";
            }
            else if (!ValidPasswordFormat(passwordText.Text))
            {
                validFormat = false;
                statusMessage = "Invalid password.";
            }

            if (!validFormat)
            {
                UpdateStatusLabel(loginStatusLabel, statusMessage, StatusLabelColour.Red);
                return;
            }

            ClassifiedDataController classifiedDC = ClassifiedDataController.Instance;
            if (!classifiedDC.ValidateCredentials(mailAddress, passwordText.Text, out string message, out EmployeeData employee))
            {
                UpdateStatusLabel(loginStatusLabel, message, StatusLabelColour.Red);
                return;
            }

            activeUser = employee;
            UpdateStatusLabel(loginStatusLabel, $"Logged in as {mailAddress.User}", StatusLabelColour.Green);
            UpdateStatusLabel(clientStatusLabel, "Select client", StatusLabelColour.Grey);
            emailText.Enabled = false;
            passwordText.Enabled = false;
            logInButton.Text = "Log Out";
            PopulateClientCombobox();
        }

        /// <summary>
        /// Processes the log-out functionality, resetting the session and UI.
        /// </summary>
        private void ProcessLogOut()
        {
            activeUser = null;
            UpdateStatusLabel(loginStatusLabel, "Enter credentials", StatusLabelColour.Grey);
            UpdateStatusLabel(clientStatusLabel, "Log in to launch client", StatusLabelColour.Grey);
            emailText.Enabled = true;
            passwordText.Enabled = true;
            logInButton.Text = "Log In";
            passwordText.Clear();
            clientComboBox.Items.Clear();
            launchButton.Enabled = false;
        }

        /// <summary>
        /// Updates the specified label with a new message and color.
        /// </summary>
        private void UpdateStatusLabel(Label label, string message, StatusLabelColour colour)
        {
            label.Text = message;
            label.ForeColor = colour switch
            {
                StatusLabelColour.Red => Color.Firebrick,
                StatusLabelColour.Green => Color.Green,
                _ => SystemColors.ControlDarkDark
            };
        }

        /// <summary>
        /// Populates the client selection combo box with available departments for the active user.
        /// </summary>
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

        /// <summary>
        /// Validates the format of an email address.
        /// </summary>
        private bool ValidEmailFormat(string email, out MailAddress mailAddress)
        {
            return MailAddress.TryCreate(email, out mailAddress);
        }

        /// <summary>
        /// Validates the format of a password.
        /// </summary>
        private bool ValidPasswordFormat(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 3;
        }

        #endregion
    }
}