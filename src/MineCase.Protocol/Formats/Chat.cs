using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MineCase.Formats
{
    /// <summary>
    /// Available types for Action of ClickEvent.
    /// </summary>
    public enum ClickEventType
    {
        OpenUrl = 0,
        RunCommand = 1,
        SuggestCommand = 2,
        ChangePage = 3
    }

    /// <summary>
    /// Available types for Action of HoverEvent.
    /// </summary>
    public enum HoverEventType
    {
        ShowText = 0,
        ShowItem = 1,
        ShowEntity = 2,
    }

    /// <summary>
    /// One of the fields of the component.
    /// </summary>
    public class ChatClickEvent
    {
        private static string[] _map = new string[4]
        { "open_rul", "run_command", "suggest_command", "change_page" };

        public ClickEventType Action { get; set; }

        public JToken Value { get; set; }

        public JObject ToJObject()
        {
            JObject jObject = new JObject();
            jObject.Add("action", _map[Action.GetHashCode()]);
            jObject.Add("value", Value);
            return jObject;
        }

        public override string ToString()
        {
            JObject jObject = new JObject();
            jObject.Add("action", _map[Action.GetHashCode()]);
            jObject.Add("value", Value);
            return jObject.ToString();
        }
    }

    /// <summary>
    /// One of the fields of the component.
    /// </summary>
    public class ChatHoverEvent
    {
        private static string[] _map = new string[3]
        { "show_text", "show_item", "show_entity" };

        public string Action { get; set; }

        public JToken Value { get; set; }

        public JObject ToJObject()
        {
            JObject jObject = new JObject();
            jObject.Add("action", _map[Action.GetHashCode()]);
            jObject.Add("value", Value);
            return jObject;
        }

        public override string ToString()
        {
            JObject jObject = new JObject();
            jObject.Add("action", _map[Action.GetHashCode()]);
            jObject.Add("value", Value);
            return jObject.ToString();
        }
    }

    /// <summary>
    /// An abstract base class that contains the fields common to all components.
    /// </summary>
    public abstract class ChatComponent
    {
        public bool Bold { get; set; }

        public bool Itatic { get; set; }

        public bool Underlined { get; set; }

        public bool Strikethrough { get; set; }

        public bool Obfuscated { get; set; }

        public string Color { get; set; }

        public string Insertion { get; set; }

        public ChatClickEvent ClickEvent { get; set; }

        public ChatHoverEvent HoverEvent { get; set; }

        public List<ChatComponent> Extra { get; set; }

        public virtual JObject ToJObject()
        {
            JObject jObject = new JObject();
            AddBoolValue(jObject, "bold", Bold);
            AddBoolValue(jObject, "itatic", Itatic);
            AddBoolValue(jObject, "underlined", Underlined);
            AddBoolValue(jObject, "shrikethrough", Strikethrough);
            AddBoolValue(jObject, "obfuscated", Obfuscated);
            AddStringValue(jObject, "color", Color);
            AddStringValue(jObject, "insertion", Insertion);

            if (ClickEvent != null)
            {
                jObject.Add("clickEvent", ClickEvent.ToJObject());
            }

            if (HoverEvent != null)
            {
                jObject.Add("hoverEvent", HoverEvent.ToJObject());
            }

            if (Extra != null && Extra.Count != 0)
            {
                JArray jArray = new JArray();
                foreach (ChatComponent comp in Extra)
                {
                    jArray.Add(comp.ToJObject());
                }
            }

            return jObject;
        }

        private void AddBoolValue(JObject jObject, string key, bool value)
        {
            if (value)
            {
                jObject.Add(key, value);
            }
        }

        private void AddStringValue(JObject jObject, string key, string value)
        {
            if (value != null && value.Length != 0)
            {
                jObject.Add(key, value);
            }
        }
    }

    /// <summary>
    /// String component, which  contains only text.
    /// </summary>
    public class StringComponent : ChatComponent
    {
        public string Text { get; set; }

        public override JObject ToJObject()
        {
            JObject jObject = base.ToJObject();

            if (Text != null && Text.Length != 0)
            {
                jObject.Add("text", Text);
            }

            return jObject;
        }
    }

    /// <summary>
    /// Translation component. Translates text into the current language
    /// </summary>
    public class TranslationComponent : ChatComponent
    {
        public string Translate { get; set; }

        public List<ChatComponent> With { get; set; }

        public override JObject ToJObject()
        {
            JObject jObject = base.ToJObject();

            if (Translate != null && Translate.Length != 0)
            {
                jObject.Add("translate", Translate);
            }

            if (With != null && With.Count != 0)
            {
                JArray jArray = new JArray();
                foreach (ChatComponent comp in With)
                {
                    jArray.Add(comp.ToJObject());
                }
            }

            return jObject;
        }
    }

    /// <summary>
    /// Keybind component. Displays the client's current keybind for the specified key.
    /// </summary>
    public class KeybindComponent : ChatComponent
    {
        public override JObject ToJObject()
        {
            JObject jObject = base.ToJObject();
            return jObject;
        }
    }

    /// <summary>
    /// Score component. Displays a score.
    /// </summary>
    public class ScoreComponent : ChatComponent
    {
        public JObject Score { get; set; }

        public override JObject ToJObject()
        {
            JObject jObject = base.ToJObject();
            return jObject;
        }
    }

    /// <summary>
    /// Selector component. Displays the results of an entity selector.
    /// </summary>
    public class SelectorComponent : ChatComponent
    {
        public override JObject ToJObject()
        {
            JObject jObject = base.ToJObject();
            return jObject;
        }
    }

    /// <summary>
    /// Chat data, which has a top-level component, can convert to a JSON string conveniently.
    /// </summary>
    public class Chat
    {
        public ChatComponent Component { get; set; }

        public JObject ToJObject()
        {
            return Component.ToJObject();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(Component.ToJObject(), Formatting.None);
        }
    }
}
