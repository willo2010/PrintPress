using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PrintPress.Data.Enum;
using System.Data;
using System.Text;

namespace PrintPress.Controller.Data
{
    internal class DataTools
    {
        public static string SchemaString(string[][] columns)
        {
            if (columns == null || columns.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder($"({columns[0][0]} {columns[0][1]}");
            for (int i = 1; i < columns.Length; i++)
            {
                sb.Append($",{columns[i][0]} {columns[i][1]}");
            }
            sb.Append(")");
            return sb.ToString();
        }

        public static string SchemaValuesString(string[] dataLabel)
        {
            if (dataLabel == null || dataLabel.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder($"({dataLabel[0]}");
            for (int i = 1; i < dataLabel.Length; i++)
            {
                sb.Append($", {dataLabel[i]}");
            }
            sb.Append(")");
            return sb.ToString();
        }

        public static string SchemaConditionString(string[][] columns, string[] dataLabel)
        {
            if (columns == null || columns.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder($"{columns[0][0]}={dataLabel[0]}");
            for (int i = 1; i < columns.Length; i++)
            {
                sb.Append($" AND {columns[i][0]}={dataLabel[i]}");
            }
            return sb.ToString();
        }

        public static string[] GenerateParameterPlaceholders(int length)
        {
            List<string> placeholders = new List<string>();
            for (int i = 0; i < length; i++)
            {
                placeholders.Add($"@item{i.ToString()}");
            }
            return placeholders.ToArray();
        }

        public static bool DepartmentString(Department department, out string text)
        {
            switch (department)
            {
                case Department.Accounts:
                    text = "Accounts";
                    return true;
                case Department.Admin:
                    text = "Admin";
                    return true;
                case Department.Editing:
                    text = "Editing";
                    return true;
                case Department.Journalism:
                    text = "Journalism";
                    return true;
                case Department.Marketing:
                    text = "Marketing";
                    return true;
                case Department.Processing:
                    text = "Processing";
                    return true;
                default:
                    text = string.Empty;
                    return false;
            }
        }

        public static bool DepartmentString(string text, out Department department)
        {
            switch (text.ToLower())
            {
                case "accounts":
                    department = Department.Accounts;
                    return true;
                case "admin":
                    department = Department.Admin;
                    return true;
                case "editing":
                    department = Department.Editing;
                    return true;
                case "journalism":
                    department = Department.Journalism;
                    return true;
                case "marketing":
                    department = Department.Marketing;
                    return true;
                case "processing":
                    department = Department.Processing;
                    return true;
                default:
                    department = Department.Journalism;
                    return false;
            }
        }

        public static byte[] ImageToByteArray(Image? image)
        {
            if (image == null) return [];
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private static Dictionary<int, Department> departmentIDs = new Dictionary<int, Department>()
        {
            { 0, Department.Admin },
            { 1, Department.Accounts },
            { 2, Department.Editing },
            { 3, Department.Journalism },
            { 4, Department.Marketing },
            { 5, Department.Processing }
        };

        public static Department DepartmentFromID(int id)
        {
            return departmentIDs[id];
        }

        public static int DepartmentFromID(Department department)
        {
            return departmentIDs.FirstOrDefault(x => x.Value == department).Key;
        }
    }
}
