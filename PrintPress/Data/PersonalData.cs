using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace PrintPress.Data
{
    public class PersonalData : Data
    {
        private Address? _address;
        public Address? HomeAddress
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                OnDataChanged();
            }
        }
        private string _firstNames;
        public string FirstNames
        {
            get
            {
                return _firstNames;
            }
            set
            {
                _firstNames = value;
                OnDataChanged();
            }
        }
        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnDataChanged();
            }
        }
        private MailAddress _mailAddress;
        public MailAddress MailAddress
        {
            get
            {
                return _mailAddress;
            }
            set
            {
                _mailAddress = value;
                OnDataChanged();
            }
        }
        private string _phone;
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                OnDataChanged();
            }
        }

        public PersonalData()
        {
            HomeAddress = new Address();
            FirstNames = string.Empty;
            LastName = string.Empty;
            MailAddress = new MailAddress("null@null");
            Phone = string.Empty;
        }

        public PersonalData(
            Address? address,
            string firstNames,
            string lastName,
            MailAddress mailAddress,
            string phone)
        {
            HomeAddress = address;
            FirstNames = firstNames;
            LastName = lastName;
            MailAddress = mailAddress;
            Phone = phone;
        }
    }
}
