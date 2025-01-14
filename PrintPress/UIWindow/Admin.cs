using PrintPress.Controller;
using PrintPress.Controller.Data;
using PrintPress.Controller.Enum;
using PrintPress.Data;
using PrintPress.Data.Enum;
using System.Net.Mail;

namespace PrintPress.UI
{
    internal partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        
        private string[] AddressFields
        {
            get
            {
                return [
                    houseNameNumText.Text,
                    roadNameText.Text,
                    cityText.Text,
                    countyText.Text,
                    countryText.Text,
                    postcodeText.Text];
            }
        }
        private string[] PersonalFields
        {
            get
            {
                return [
                    firstNamesText.Text,
                    lastNameText.Text,
                    emailText.Text.ToLower(),
                    phoneText.Text];
            }
        }

        private string[] EmployeeFields
        {
            get
            {
                return [
                    jobDescText.Text,
                    passwordText.Text];
            }
        }

        private void CreateCoTableButton_Click(object sender, EventArgs e)
        {
            //VerifyDatabase();
        }

        private void sendSqlButton_Click(object sender, EventArgs e)
        {
            CommandReturnState resultState = CommercialDataController.Instance.SendSql(sqlStringText.Text, out string result);
            if (resultState == CommandReturnState.FOUND)
            {
                MessageBox.Show(result);
            }
            else if (resultState == CommandReturnState.NOTFOUND)
            {
                MessageBox.Show("Request not found");
            }
        }

        private void sendClassifiedSqlButton_Click(object sender, EventArgs e)
        {
            if (ClassifiedDataController.Instance.SendSql(clasSqlStringText.Text, out string result) ==
                CommandReturnState.FOUND) MessageBox.Show(result);
        }

        private void addEmployeeButton_Click(object sender, EventArgs e)
        {
            string[] adrsText = AddressFields;

            bool oneAdrsFieldValid = false;
            bool oneAdrsFieldInvalid = false;

            foreach (string field in adrsText)
            {
                if (validStandardInput(field))
                {
                    oneAdrsFieldValid = true;
                }
                else
                {
                    oneAdrsFieldInvalid = true;
                }
            }

            Address? address = null;
            if (oneAdrsFieldValid && oneAdrsFieldInvalid)
            {
                MessageBox.Show("invalid address format - leave all fields blank for no address.");
            }
            else if (oneAdrsFieldValid) 
            {
                address = new Address(
                adrsText[0],
                adrsText[1],
                adrsText[2],
                adrsText[3],
                adrsText[4],
                adrsText[5]);
            }

            string[] psnlFields = PersonalFields;

            if (!validStandardInput(psnlFields))
            {
                MessageBox.Show("invalid name / personal details");
                return;
            }

            if (!MailAddress.TryCreate(psnlFields[2], out MailAddress email))
            {
                MessageBox.Show("invalid email format");
                return;
            }

            PersonalData personalData = new PersonalData(
                address,
                psnlFields[0],
                psnlFields[1],
                email,
                psnlFields[3]);

            string[] employeeFields = EmployeeFields;
            if (!validStandardInput(employeeFields)) 
            {
                MessageBox.Show("invalid job desc / password");
                return;
            }

            string[] clearances = clearanceText.Text.Split(',');
            List<Department> departmentList = new List<Department>();

            foreach (string clearance in clearances)
            {
                if (DataTools.DepartmentString(clearance, out Department d))
                {
                    departmentList.Add(d);
                }
            }

            EmployeeData employeeData = new EmployeeData(
                -1,
                personalData,
                employeeFields[0],
                departmentList.ToArray());

            ClassifiedDataController.Instance.AddCredentials(employeeData, employeeFields[1]);
        }
        private bool validStandardInput(string input, int maxLength = 20)
        {
            return validStandardInput([input]);
        }
        private bool validStandardInput(string[] input, int maxLength = 20)
        {
            foreach (string s in input)
            {
                string str = s.Trim();
                if (str.Length < 2 || str.Length > maxLength)
                {
                    return false;
                }
            }

            return true;
        }
    }
}