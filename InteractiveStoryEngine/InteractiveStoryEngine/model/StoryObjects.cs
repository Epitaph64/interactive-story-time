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
        public string textOpen { get; set; }
        public string textClose { get; set; }
        public string textReturn { get; set; }
        public bool visited { get; set; }
        public List<StoryButton> storyButtons { get; set; }
    }

    public class StoryButton
    {
        public int num { get; set; }
        public int go { get; set; }
        public string text { get; set; }
        public string textClick { get; set; }
    }

    public class StoryArt
    {
        public int artNo { get; set; }
        public string text { get; set; }

        public string GetDisplay(string layout, string content = "")
        {
            string[] tokens = layout.Split('-');
            string output = "";
            foreach (string t in tokens)
            {
                switch (t)
                {
                    // add associated art
                    case "a":
                        output += text;
                        break;
                    // add passed content
                    case "c":
                        output += content;
                        break;
                }
                output += Environment.NewLine;
            }
            return output;
        }
    }
}
