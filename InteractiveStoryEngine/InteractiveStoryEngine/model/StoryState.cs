using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InteractiveStoryEngine.model
{
    class StoryState
    {
        private Story story;

        private Dictionary<int, StoryPage> pages;
        private Dictionary<int, bool> visited;

        private int currentPage;

        internal StoryState()
        {
            // load story from file (TODO: browser)
            story = StoryLoader.LoadStory("story.yaml");

            pages = new Dictionary<int, StoryPage>();
            visited = new Dictionary<int, bool>();

            foreach (var page in story.pages)
            {
                pages.Add(page.pageNo, page);
                visited.Add(page.pageNo, false);
            }

            // set currentPage to smallest pageNo in story
            currentPage = pages.Keys.Min();
        }

        // page accessor methods
        internal string GetOpeningText()
        {
            if (visited[currentPage])
            {
                return pages[currentPage].returningText;
            }
            else
            {
                visited[currentPage] = true;
                return pages[currentPage].openingText;
            }
        }

        // currentPage accessor methods
        internal string SetCurrentPage(int pageNo)
        {
            if (pages.ContainsKey(pageNo))
            {
                string closingText = pages[currentPage].closingText;
                currentPage = pageNo;
                // return the closingText of last page concatenated
                // with the opening text of the new page
                return closingText + GetOpeningText();
            }
            else
            {
                return $"Error: page does not exist in story: {pageNo}";
            }
        }
        internal int GetCurrentPage()
        {
            return currentPage;
        }

        internal string GetClosingText()
        {
            return (pages[currentPage].closingText == null) ?
                "" : pages[currentPage].closingText;
        }

        internal string[] GetButtonDisplays()
        {
            string[] buttonDisplays = new string[8];

            foreach (StoryButton sb in pages[currentPage].storyButtons)
            {
                buttonDisplays[sb.num] = sb.display;
            }

            return buttonDisplays;
        }

        internal string buttonClicked(int buttonNumber)
        {
            var button = pages[currentPage].storyButtons[buttonNumber];
            return SetCurrentPage(button.go);
        }
    }
}