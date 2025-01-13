using PrintPress.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintPress.Data.Builder
{
    public abstract class ContentBuilder
    {
        public int ContentID { get; set; } = -1;
        public EmployeeData Assigned { get; set; } = new EmployeeData();
        public string Text { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public Image? Image { get; set; } = null;
        public string Notes { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public ContentState State { get; set; } = ContentState.IN_PROGRESS;
        public DateTime LastSaved { get; set; } = DateTime.MinValue;
    }
}
