using PrintPress.Data.Enum;

namespace PrintPress.Data
{
    public class EmployeeData
    {
        public int Id { get; init; }
        public PersonalData personalData;
        public string jobTitle;
        public Department[] clearance;

        public EmployeeData()
        {
            Id = -1;
            personalData = new PersonalData();
            jobTitle = string.Empty;
            clearance = Array.Empty<Department>();
        }

        public EmployeeData(int id)
        {
            Id = id;
            personalData = new PersonalData();
            jobTitle = string.Empty;
            clearance = Array.Empty<Department>();
        }

        public EmployeeData(int id, PersonalData personalData,string jobTitle, Department[] clearance) 
        {
            Id = id;
            this.personalData = personalData;
            this.jobTitle = jobTitle;
            this.clearance = clearance;
        }

        public bool HasClearance(Department department)
        {
            foreach (Department dept in clearance)
            {
                if (dept == department) return true;
            }
            return false;
        }
    }
}
