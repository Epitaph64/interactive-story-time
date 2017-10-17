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
        // view-model objects/references
        public GameConsole gc;
        private Button[] buttons;
        private Grid buttonGrid;

        // story state
        private StoryState state;
        
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

            loadStory();
        }

        private void loadStory()
        {
            // create story initial state
            state = new StoryState();

            // for now, write the opening text of first page after loading story
            // TODO: title page format perhaps or special field in metadata object
            var openingText = state.GetOpeningText();
            if (openingText != null)
            {
                gc.Write(openingText);
            }
            updateButtonDisplays(state.GetButtonDisplays());
        }

        private void updateView()
        {
            updateButtonDisplays(state.GetButtonDisplays());
        }

        // if a left-panel button is clicked, handle the event
        private void buttonClick(object sender, RoutedEventArgs e)
        {
            // determine Button which triggered the event
            Button button = (Button)sender;
            int buttonNumber = int.Parse("" + button.Name.ToCharArray()[1]);

            // pass the number of the button to the state
            // if the button has any associated text it will be returned
            string clickedText = state.buttonClicked(buttonNumber);

            // write button click text
            if (clickedText != null)
            {
                gc.Write(clickedText);
            }

            updateView();
        }

        private void updateButtonDisplays(string[] displayTexts)
        {
            for (int i = 0; i < 8; i++)
            {
                var text = displayTexts[i];
                if (text == null)
                {
                    buttons[i].Visibility = Visibility.Hidden;
                } else
                {
                    buttons[i].Content = text;
                    buttons[i].Visibility = Visibility.Visible;
                }
            }
        }
    }
}
