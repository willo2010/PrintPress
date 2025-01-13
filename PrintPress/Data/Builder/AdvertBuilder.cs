using PrintPress.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintPress.Data.Builder
{
    public class AdvertBuilder : ContentBuilder
    {
        public PersonalData Contact { get; set; } = new PersonalData();

        public AdvertData ToAdvertData()
        {
            return new AdvertData
                (ContentID, Assigned, Text, Title, Image, Notes, Comments, State, LastSaved, Contact);
        }
    }
}
