using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.IO;
using InteractiveStoryEngine.view;

/* This class handles loading a YAML based document
 * in order to produce an interactive story in WPF (using an 8 button grid panel, and a simple textual console display). Uses https://github.com/aaubry/YamlDotNet to handle YAML processing.
*/

namespace InteractiveStoryEngine.model
{
    public class StoryLoader
    {
        // load a story given a file name
        static public Story LoadStory(String fileName)
        {
            // load in the story program (YAML document) to input string
            var doc = File.ReadAllText(fileName);
            var input = new StringReader(doc);

            // setup deserializer engine
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .Build();

            // deserialize input stream as page objects
            var story = deserializer.Deserialize<Story>(input);

            return story;
        }
    }
}
