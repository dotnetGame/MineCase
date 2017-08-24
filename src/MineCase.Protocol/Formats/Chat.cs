using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace MineCase.Formats
{
    /// <summary>
    /// Available types for Action of ClickEvent.
    /// </summary>
    public enum ClickEventType
    {
        [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
        OpenUrl = 0,
        [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
        RunCommand = 1,
        [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
        SuggestCommand = 2,
        [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
        ChangePage = 3
    }

    /// <summary>
    /// Available types for Action of HoverEvent.
    /// </summary>
    public enum HoverEventType
    {
        ShowText = 0,
        ShowItem = 1,
        ShowEntity = 2
    }

    /// <summary>
    /// One of the fields of the component.
    /// </summary>
    public class ChatClickEvent
    {
        private static readonly string[] _map = new string[4]
        { "open_url", "run_command", "suggest_command", "change_page" };

        [JsonProperty("action")]
        public ClickEventType Action { get; set; }

        [JsonProperty("value")]
        public JToken Value { get; set; }

        public JObject ToJObject()
        {
            JObject jObject = new JObject();
            jObject.Add("action", _map[Action.GetHashCode()]);
            jObject.Add("value", Value);
            return jObject;
        }
    }

    /// <summary>
    /// One of the fields of the component.
    /// </summary>
    public class ChatHoverEvent
    {
        private static readonly string[] _map = new string[3]
        { "show_text", "show_item", "show_entity" };

        [JsonProperty("action")]
        public HoverEventType Action { get; set; }

        [JsonProperty("value")]
        public JToken Value { get; set; }

        public JObject ToJObject()
        {
            JObject jObject = new JObject();
            jObject.Add("action", _map[Action.GetHashCode()]);
            jObject.Add("value", Value);
            return jObject;
        }
    }

    /// <summary>
    /// An abstract base class that contains the fields common to all components.
    /// </summary>
    public abstract class ChatComponent
    {
        public bool? Bold { get; set; }

        public bool? Itatic { get; set; }

        public bool? Underlined { get; set; }

        public bool? Strikethrough { get; set; }

        public bool? Obfuscated { get; set; }

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
                foreach (var comp in Extra)
                {
                    jArray.Add(comp.ToJObject());
                }
            }

            return jObject;
        }

        private void AddBoolValue(JObject jObject, string key, bool? value)
        {
            if (value.HasValue)
            {
                if ((bool)value)
                {
                    jObject.Add(key, (bool)value);
                }
            }
        }

        private void AddStringValue(JObject jObject, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
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
        [JsonProperty("text")]
        public string Text { get; set; }

        public override JObject ToJObject()
        {
            JObject jObject = base.ToJObject();

            if (!string.IsNullOrEmpty(Text))
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

            if (!string.IsNullOrEmpty(Translate))
            {
                jObject.Add("translate", Translate);
            }

            if (With != null && With.Count != 0)
            {
                JArray jArray = new JArray();
                foreach (var comp in With)
                {
                    jArray.Add(comp.ToJObject());
                }

                jObject.Add("with", jArray);
            }

            return jObject;
        }
    }

    /// <summary>
    /// Keybind component. Displays the client's current keybind for the specified key.
    /// </summary>
    public class KeybindComponent : ChatComponent
    {
        // TODO: Implements this component.
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
        // TODO: Implements this component.
        public JObject Score { get; set; }

        public override JObject ToJObject()
        {
            JObject jObject = base.ToJObject();
            jObject.Add("score", Score);
            return jObject;
        }
    }

    /// <summary>
    /// Selector component. Displays the results of an entity selector.
    /// </summary>
    public class SelectorComponent : ChatComponent
    {
        // TODO: Implements this component.
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
        private static readonly Dictionary<string, int> _dict = new Dictionary<string, int>
        {
            { "open_url", 0 }, { "run_command", 1 }, { "suggest_command", 2 }, { "change_page", 3 },
            { "show_text", 0 }, { "show_item", 1 }, { "show_entity", 2 }
        };

        /// <summary>
        /// Gets or sets the top-level component of this object.
        /// </summary>
        public ChatComponent Component { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chat"/> class.
        /// </summary>
        public Chat()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chat"/> class with the specified component.
        /// </summary>
        /// <param name="component">The top-level component of this object.</param>
        public Chat(ChatComponent component)
        {
            Component = component;
        }

        /// <summary>
        /// Parses Chat from a JSON string.
        /// </summary>
        /// <param name="json">The JSON string</param>
        /// <returns>A Chat object</returns>
        public static Chat Parse(string json)
        {
            if (string.IsNullOrEmpty(json))
                return null;
            ChatComponent component = null;
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            var jsonObject = JObject.Parse(json);
            Handle(jsonObject);
            json = jsonObject.ToString();
            if (json.Contains("text"))
            {
                component = JsonConvert.DeserializeObject<StringComponent>(json, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver
                });
            }
            else if (json.Contains("translate"))
            {
                component = JsonConvert.DeserializeObject<TranslationComponent>(json, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver
                });
            }
            else if (json.Contains("keybind"))
            {
                component = JsonConvert.DeserializeObject<KeybindComponent>(json, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver
                });
            }
            else if (json.Contains("score"))
            {
                component = JsonConvert.DeserializeObject<ScoreComponent>(json, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver
                });
            }
            else if (json.Contains("selector"))
            {
                component = JsonConvert.DeserializeObject<SelectorComponent>(json, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver
                });
            }

            if (component != null)
            {
                return new Chat(component);
            }

            return null;
        }

        /// <summary>
        /// Serializes this object to a JObject.
        /// </summary>
        /// <returns>A JObject</returns>
        public JObject ToJObject()
        {
            return Component.ToJObject();
        }

        /// <summary>
        /// Serializes this object to a string.
        /// </summary>
        /// <returns>A JSON string</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(Component.ToJObject(), Formatting.None);
        }

        private static void Handle(JObject jObject)
        {
            var clickEvent = (JObject)jObject["clickEvent"];
            if (clickEvent != null && clickEvent.HasValues)
            {
                jObject["clickEvent"]["action"] = _dict[(string)jObject["clickEvent"]["action"]];
            }

            var hoverEvent = (JObject)jObject["hoverEvent"];
            if (hoverEvent != null && hoverEvent.HasValues)
            {
                jObject["hoverEvent"]["action"] = _dict[(string)jObject["hoverEvent"]["action"]];
            }

            var extra = (JObject)jObject["extra"];
            if (extra != null && extra.HasValues)
            {
                Handle((JObject)jObject["extra"]);
            }
        }
    }
}
