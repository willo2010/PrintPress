using PrintPress.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PrintPress.Data.Builder
{
    public class PersonalDataBuilder
    {
        public Address Address { get; set; } = new Address();
        public string FirstNames { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public PersonalData ToPersonalData()
        {
            if (!MailAddress.TryCreate(Email, out MailAddress? mail))
            {
                return new PersonalData();
            }

            return new PersonalData(
                Address,
                FirstNames,
                LastName,
                mail,
                Phone);
        }
    }
}
