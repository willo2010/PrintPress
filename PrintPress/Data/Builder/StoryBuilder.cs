using PrintPress.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintPress.Data.Builder
{
    public class StoryBuilder : ContentBuilder
    {
        public string Source { get; set; } = string.Empty;

        public StoryData ToStoryData()
        {
            return new StoryData
                (ContentID, Assigned, Text, Title, Image, Notes, Comments, State, LastSaved, Source);
        }
    }
}
