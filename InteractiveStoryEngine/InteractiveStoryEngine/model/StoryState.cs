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
            if (pages[currentPage].visited && pages[currentPage].textReturn != null)
            {
                return pages[currentPage].textReturn;
            }
            else
            {
                pages[currentPage].visited = true;
                return pages[currentPage].textOpen;
            }
        }

        internal string GetClosingText()
        {
            return (pages[currentPage].textClose == null) ?
                "" : pages[currentPage].textClose;
        }

        // currentPage accessor methods
        internal string SetCurrentPage(int pageNo)
        {
            if (pageNo == 0) return null;
            if (pages.ContainsKey(pageNo))
            {
                string closingText = GetClosingText();
                currentPage = pageNo;
                return closingText + GetOpeningText();
            }
            else
            {
                return $"Error: page does not exist in story: {pageNo}";
            }
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

        internal string ButtonClicked(int buttonNumber)
        {
            var button = pages[currentPage].storyButtons.Find(i => i.num == buttonNumber);
            if (button != null)
            {
                // set initial content to button click text if available
                string content = button.textClick == null ?
                    "" :
                    button.textClick;

                return content + SetCurrentPage(button.go);
            }
            else
            {
                return $"Error: Button does not exist with num: {buttonNumber}";
            }
        }
    }
}