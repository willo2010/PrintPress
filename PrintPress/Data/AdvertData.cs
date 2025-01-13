using PrintPress.Data.Enum;

namespace PrintPress.Data
{
    public class AdvertData : ContentData
    {
        public PersonalData Contact { get; init; }

        public AdvertData(int id,
            EmployeeData assigned,
            string text, string title,
            Image? image,
            string notes,
            string comments,
            ContentState state,
            DateTime lastSaved,
            PersonalData contact) :
            base(id, assigned, text, title, image, notes, comments, state, lastSaved)
        {
            Contact = contact;
            Contact.DataChanged += OnDataChanged;
        }
    }
}