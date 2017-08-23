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
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public abstract class ChatComponent
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Bold { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Itatic { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Underlined { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Strikethrough { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Obfuscated { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Insertion { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ChatClickEvent ClickEvent { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ChatHoverEvent HoverEvent { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
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
            jObject.Add("score", Score);
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

    public class ChatJsonConverter : JsonConverter
    {
        private readonly Type[] _types;

        private static readonly string[] _clickMap = new string[4]
        { "open_url", "run_command", "suggest_command", "change_page" };

        private static readonly string[] _hoverMap = new string[3]
        { "show_text", "show_item", "show_entity" };

        public ChatJsonConverter(params Type[] types)
        {
            _types = types;
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.GetType() == typeof(bool))
            {
                if ((bool)value)
                {
                    writer.WriteValue((bool)value);
                }
            }
            else if (value.GetType() == typeof(ClickEventType))
            {
                ClickEventType type = (ClickEventType)value;
                writer.WriteValue(_clickMap[type.GetHashCode()]);
            }
            else if (value.GetType() == typeof(HoverEventType))
            {
                HoverEventType type = (HoverEventType)value;
                writer.WriteValue(_hoverMap[type.GetHashCode()]);
            }
        }
    }

    /// <summary>
    /// Chat data, which has a top-level component, can convert to a JSON string conveniently.
    /// </summary>
    public class Chat
    {
        public ChatComponent Component { get; set; }

        public Chat()
        {
        }

        public Chat(ChatComponent component)
        {
            Component = component;
        }

        public static Chat Parse(string json)
        {
            if (string.IsNullOrEmpty(json))
                return null;
            ChatComponent component = null;
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            json = json.Replace("change_page", "3");
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

        public JObject ToJObject()
        {
            return Component.ToJObject();
        }

        public override string ToString()
        {
            // return JsonConvert.SerializeObject(Component.ToJObject(), Formatting.None);
            return JsonConvert.SerializeObject(Component, Formatting.None, new ChatJsonConverter(typeof(bool), typeof(ClickEventType), typeof(HoverEventType)));
        }
    }
}
