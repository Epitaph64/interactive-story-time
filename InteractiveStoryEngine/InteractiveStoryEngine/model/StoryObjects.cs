using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveStoryEngine.model
{
    /* These classes define the hierarchy built by the StoryLoader which
     * represents the story structure. */
    public class Story
    {
        public List<StoryPage> pages { get; set; }
        public List<StoryArt> art { get; set; }
    }

    public class StoryPage
    {
        public int pageNo { get; set; }
        public int artNo { get; set; }
        public string artLayout { get; set; }
        public string openingText { get; set; }
        public string closingText { get; set; }
        public string returningText { get; set; }
        public List<StoryButton> storyButtons { get; set; }
    }

    public class StoryButton
    {
        public int num { get; set; }
        public int go { get; set; }
        public string display { get; set; }
        public string clickText { get; set; }
    }

    public class StoryArt
    {
        public int artNo { get; set; }
        public string display { get; set; }
    }
}
