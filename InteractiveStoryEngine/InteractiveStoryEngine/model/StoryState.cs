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

        private int currentPage;

        internal StoryState()
        {
            // load story from file (TODO: browser)
            story = StoryLoader.LoadStory("story.yaml");

            pages = new Dictionary<int, StoryPage>();
            foreach (var page in story.pages)
            {
                pages.Add(page.pageNo, page);
                pages[page.pageNo].visited = false;
            }

            // set currentPage to smallest pageNo in story
            currentPage = pages.Keys.Min();
        }

        // page accessor methods
        internal string GetOpeningText()
        {
            if (pages[currentPage].visited)
            {
                return pages[currentPage].textReturn;
            }
            else
            {
                pages[currentPage].visited = true;
                return pages[currentPage].textOpen;
            }
        }

        // currentPage accessor methods
        internal string SetCurrentPage(int pageNo)
        {
            if (pages.ContainsKey(pageNo))
            {
                string closingText = pages[currentPage].textClose;
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
            return (pages[currentPage].textClose == null) ?
                "" : pages[currentPage].textClose;
        }

        internal string[] GetButtonDisplays()
        {
            string[] buttonDisplays = new string[8];

            foreach (StoryButton sb in pages[currentPage].storyButtons)
            {
                buttonDisplays[sb.num] = sb.text;
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