# interactive-story-time
An interactive story-telling "console" designed to make programming your own stories easy using a YAML-based parser. Uses the WPF framework to render buttons/text in a pleasing way on Windows 10.

### Example Story Script
- Name the file story.yaml and place it in binary's stories/ path
- Copy and paste the following script to get started:

```
---
pages:
  - pageNo: 1
    openingText: "You read page 1's opening text."
    storyButtons:
      - num: 0
        go: 2
        display: "Turn to page 2"
      - num: 1
        go: 3
        display: "Skip to page 3"
  - pageNo: 2
    openingText: >-
      You reach page 2.
      Once you've fully read it, you must decide what to do next.
    closingText: "You close page 2."
    storyButtons:
      - num: 0
        go: 1
        display: |-
          Go to
          Page 1
      - num: 3
        go: 3
        display: |-
          Go to
          Page 3
  - pageNo: 3
    openingText: "This is the first time you've read page 3!"
    returningText: "You've read page 3 before..."
    storyButtons:
      - num: 0
        go: 1
        display: "Restart Story"
      - num: 1
        go: 4
        display: "End Story"
  - pageNo: 4
    openingText: "The story is over!"
    storyButtons: []
```

### Credits
- YamlDotNet (Deserialize YAML format for story loading):
  - https://github.com/aaubry/YamlDotNet

### Ideas for Future Improvement
- Way to go back to previous page
- Story load function / story browser
  - Store metadata in file
  - Allow customization of font style / colors on per-story basis
