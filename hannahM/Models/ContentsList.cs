using System.Collections.Generic;

namespace hannahM.Models
{
    public class ContentsList
    {
        public List<Story> stories { get; set; }
        public List<Chapters> chapters { get; set; }
        public List<Blog> blogs { get; set; }
        public List<RandomThoughts> random { get; set; }

    }
}
