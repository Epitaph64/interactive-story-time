using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using InteractiveStoryEngine.model;
using static InteractiveStoryEngine.model.StoryLoader;

namespace InteractiveStoryEngine.view
{
    internal class GameView
    {
        public GameConsole gc;
        private Button[] buttons;
        private Grid buttonGrid;

        private Dictionary<int, StoryPage> pageDict;
        private Dictionary<int, bool> pageVisitation;
        private int currentPage;

        private Dictionary<int, StoryButton> storyButtonDict;
        // to avoid magic number usage, but cannot be changed
        private const int buttonCount = 8;

        public GameView(GameConsole gc, Grid buttonGrid)
        {
            // store GameConsole instance
            this.gc = gc;
            this.buttonGrid = buttonGrid;

            // initialize buttons
            buttons = new Button[buttonCount];
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button();
                buttons[i].Name = $"b{i}";
                buttons[i].Click += buttonClick;

                // add button to buttonGrid
                this.buttonGrid.Children.Add(buttons[i]);

                // layout buttons in 2 column layout
                if (i % 2 == 1)
                {
                    Grid.SetColumn(buttons[i], 1);
                }
                if (i > 1)
                {
                    Grid.SetRow(buttons[i], i / 2);
                }
            }

            Story story = LoadStory("basic.yaml");

            // initialize page dictionary
            pageDict = new Dictionary<int, StoryPage>();
            pageVisitation = new Dictionary<int, bool>();
            foreach (var page in story.pages)
            {
                pageDict.Add(page.pageNo, page);
                pageVisitation.Add(page.pageNo, false);
            }

            loadButtons(1);
        }

        // load buttons from page
        private void loadButtons(int loadPageNum)
        {
            currentPage = loadPageNum;
            storyButtonDict = new Dictionary<int, StoryButton>();
            for (int i = 0; i < buttonCount; i++)
            {
                buttons[i].Content = "";
                buttons[i].Visibility = Visibility.Hidden;
            }

            // if currentPage exists and has storyButtons
            if (pageDict.ContainsKey(currentPage) && pageDict[currentPage].storyButtons != null)
            {
                // if the page was already visisted and has returningText specified
                if (pageVisitation[currentPage] && pageDict[currentPage].returningText != null)
                {
                    gc.Write(pageDict[currentPage].returningText);
                } else
                {
                    gc.Write(pageDict[currentPage].openingText);
                }
                pageVisitation[currentPage] = true;
                
                foreach (var btn in pageDict[currentPage].storyButtons)
                {
                    buttons[btn.num].Content = btn.display;
                    storyButtonDict.Add(btn.num, btn);
                    buttons[btn.num].Visibility = Visibility.Visible;
                }
            }
        }

        // navigate to different page in story
        private void goToPage(int go)
        {
            gc.Write(pageDict[currentPage].closingText);
            loadButtons(go);
        }

        // if a left-panel button is clicked, handle the event
        private void buttonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            // determine button number from string "bX" where X is the number of the button
            Int32 buttonNumber = Int32.Parse("" + button.Name.ToCharArray()[1]);
            if (storyButtonDict.ContainsKey(buttonNumber))
            {
                if (storyButtonDict[buttonNumber].clickText != null)
                {
                    gc.Write(storyButtonDict[buttonNumber].clickText);
                }
                goToPage(storyButtonDict[buttonNumber].go);
            }
        }
    }
}
