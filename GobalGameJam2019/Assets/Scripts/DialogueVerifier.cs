using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

public class DialogueVerifier
{
    public static string Verify(string filename)
    {
        XElement tree = XElement.Load(filename);
        
        //Get prompts
        IEnumerable<XElement> prompts = tree.Descendants().Where(x => x.Name == "prompt");
        if (prompts.Count() <= 0)
            return "No prompts found! At least one prompt is necessary!";

        //Make sure prompts all have id and text
        foreach (XElement prompt in prompts)
        {
            if (prompt.Attribute("id") == null)
                return "Prompt found without an id. All prompts must have an id!";
            if (prompt.Attribute("text") == null)
                return "Prompt with id \"" + prompt.Attribute("id").Value + "\" does not have a text attribute. All prompts must have a text attribute.";
            string id = prompt.Attribute("id").Value;
            if (prompts.Where(x => x.Attribute("id") != null && x.Attribute("id").Value == id).Count() > 1)
                return "Multiple prompts with id = " + id + "!";

        }

        //Check for root
        IEnumerable<XElement> roots = prompts.Where(x => x.Attribute("id").Value == "root");
        if (roots.Count() <= 0)
            return "Root prompt not found. Need one prompt with id=\"root\"!";
        if (roots.Count() > 1)
            return "Multiple roots found! Only one root is allowed!";

        foreach (XElement prompt in prompts)
        {
            if (prompt.Descendants().Count() <= 0)
                return "Prompt with id \"" + prompt.Attribute("id").Value + "\" has no children. Prompts must have at least one child option element.";
            foreach(XElement child in prompt.Descendants())
            {
                if (child.Name != "option")
                    return "Non-option child of prompt detected. All child elements of prompts should be options. Prompt id = \"" + prompt.Attribute("id").Value + "\"";
                if (child.Attribute("action") == null)
                    return "Option without action detected. Prompt id = \"" + prompt.Attribute("id").Value + "\"";
                string action = child.Attribute("action").Value;
                if (action == "exit")
                { }
                else if (action == "battle")
                { }
                else if (action == "prompt")
                {
                    if (child.Attribute("prompt") == null)
                        return "Child with prompt action has no prompt value! Prompt id = \"" + prompt.Attribute("id").Value + "\".";
                    string target = child.Attribute("prompt").Value;
                    if (prompts.Where(x => x.Attribute("id") != null && x.Attribute("id").Value == target).Count() <= 0)
                        return "Child of prompt \"" + prompt.Attribute("id").Value + "\" has target of \"" + target + "\", which is not defined.";
                }
                else
                    return "Unknown action provided. Prompt id = \"" + prompt.Attribute("id").Value + "\". Action = \"" + action + "\".";
            }
        }

        //Actually a valid file!
        return "";
    }
}
