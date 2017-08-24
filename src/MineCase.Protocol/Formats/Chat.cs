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
        ShowEntity = 2
    }

    /// <summary>
    /// Available types for keybind.
    /// </summary>
    public enum KeyBindType
    {
        Attack = 0,
        Use = 1,
        Forward = 2,
        Left = 3,
        Back = 4,
        Right = 5,
        Jump = 6,
        Sneak = 7,
        Sprint = 8,
        Drop = 9,
        Inventory = 10,
        Chat = 11,
        PlayerList = 12,
        PickItem = 13,
        Command = 14,
        ScreenShot = 15,
        TogglePerspective = 16,
        SmoothCamera = 17,
        Fullscreen = 18,
        SpectatorOutlines = 19,
        SwapHands = 20,
        SaveToolbarActivator = 21,
        LoadToolbarActivator = 22,
        Advancements = 23,
        Hotbar1 = 24,
        Hotbar2 = 25,
        Hotbar3 = 26,
        Hotbar4 = 27,
        Hotbar5 = 28,
        Hotbar6 = 29,
        Hotbar7 = 30,
        Hotbar8 = 31,
        Hotbar9 = 32
    }

    /// <summary>
    /// One of the fields of the component.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ChatClickEvent
    {
        private static readonly string[] _map = new string[4]
        { "open_url", "run_command", "suggest_command", "change_page" };

        public ClickEventType Action { get; set; }

        public JToken Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatClickEvent"/> class.
        /// </summary>
        public ChatClickEvent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatClickEvent"/> class with the specified parameters.
        /// </summary>
        /// <param name="action">Action type</param>
        /// <param name="value">The value of action</param>
        public ChatClickEvent(ClickEventType action, JToken value)
        {
            Action = action;
            Value = value;
        }

        public JObject ToJObject()
        {
            JObject jObject = new JObject();
            jObject.Add("action", _map[(int)Action]);
            jObject.Add("value", Value);
            return jObject;
        }
    }

    /// <summary>
    /// One of the fields of the component.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ChatHoverEvent
    {
        private static readonly string[] _map = new string[3]
        { "show_text", "show_item", "show_entity" };

        public HoverEventType Action { get; set; }

        public JToken Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatHoverEvent"/> class.
        /// </summary>
        public ChatHoverEvent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatHoverEvent"/> class with the specified parameters.
        /// </summary>
        /// <param name="action">Action type</param>
        /// <param name="value">The value of action</param>
        public ChatHoverEvent(HoverEventType action, JToken value)
        {
            Action = action;
            Value = value;
        }

        public JObject ToJObject()
        {
            JObject jObject = new JObject();
            jObject.Add("action", _map[(int)Action]);
            jObject.Add("value", Value);
            return jObject;
        }
    }

    /// <summary>
    /// A object in the ScoreComponent.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ChatScore
    {
        public string Name { get; set; }

        public string Objective { get; set; }

        public int? Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatScore"/> class.
        /// </summary>
        public ChatScore()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatScore"/> class with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the player</param>
        /// <param name="objective">The scoreboard target to display the score</param>
        public ChatScore(string name, string objective)
        {
            Name = name;
            Objective = objective;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatScore"/> class with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the player</param>
        /// <param name="objective">The scoreboard target to display the score</param>
        /// <param name="value">The score to be displayed</param>
        public ChatScore(string name, string objective, int value)
        {
            Name = name;
            Objective = objective;
            Value = value;
        }
    }

    /// <summary>
    /// An abstract base class that contains the fields common to all components.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
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

        public JArray Extra { get; set; }

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
                    jArray.Add((JObject)comp);
                }

                jObject.Add("extra", jArray);
            }

            return jObject;
        }

        private void AddBoolValue(JObject jObject, string key, bool? value)
        {
            if (value != null)
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
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class StringComponent : ChatComponent
    {
        public string Text { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringComponent"/> class.
        /// </summary>
        public StringComponent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringComponent"/> class with a string.
        /// </summary>
        /// <param name="text">The text of StringComponent</param>
        public StringComponent(string text)
        {
            Text = text;
        }

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
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class TranslationComponent : ChatComponent
    {
        public string Translate { get; set; }

        public JArray With { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationComponent"/> class.
        /// </summary>
        public TranslationComponent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationComponent"/> class with a string and a JArray.
        /// </summary>
        /// <param name="translate">Translates text</param>
        /// <param name="with">Optional tag</param>
        public TranslationComponent(string translate, JArray with)
        {
            Translate = translate;
            With = with;
        }

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
                    jArray.Add((JObject)comp);
                }

                jObject.Add("with", jArray);
            }

            return jObject;
        }
    }

    /// <summary>
    /// Keybind component. Displays the client's current keybind for the specified key.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class KeybindComponent : ChatComponent
    {
        private static readonly string[] _map = new string[33]
        {
            "key.attack", "key.use", "key.forward", "key.left", "key.back", "key.right",
            "key.jump", "key.sneak", "key.sprint", "key.drop", "key.inventory", "key.chat",
            "key.playerlist", "key.pickItem", "key.command", "key.screenshot",
            "key.togglePerspective", "key.smoothCamera", "key.fullscreen", "key.spectatorOutlines",
            "key.swapHands", "key.saveToolbarActivator", "key.loadToolbarActivator",
            "key.advancements", "key.hotbar.1", "key.hotbar.2", "key.hotbar.3", "key.hotbar.4",
            "key.hotbar.5", "key.hotbar.6", "key.hotbar.7", "key.hotbar.8", "key.hotbar.9"
        };

        public KeyBindType Keybind { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeybindComponent"/> class.
        /// </summary>
        public KeybindComponent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeybindComponent"/> class with the specified type.
        /// </summary>
        /// <param name="type">The type of keybind</param>
        public KeybindComponent(KeyBindType type)
        {
            Keybind = type;
        }

        public override JObject ToJObject()
        {
            JObject jObject = base.ToJObject();
            jObject.Add("keybind", _map[(int)Keybind]);
            return jObject;
        }
    }

    /// <summary>
    /// Score component. Displays a score.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ScoreComponent : ChatComponent
    {
        public ChatScore Score { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreComponent"/> class.
        /// </summary>
        public ScoreComponent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreComponent"/> class ChatScore class.
        /// </summary>
        /// <param name="score">A ChatScore class</param>
        public ScoreComponent(ChatScore score)
        {
            Score = score;
        }

        public override JObject ToJObject()
        {
            JObject jObject = base.ToJObject();
            jObject.Add("name", Score.Name);
            jObject.Add("objective", Score.Objective);
            if (Score.Value != null)
            {
                jObject.Add("value", Score.Value);
            }

            return jObject;
        }
    }

    /// <summary>
    /// Selector component. Displays the results of an entity selector.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class SelectorComponent : ChatComponent
    {
        public string Selector { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorComponent"/> class.
        /// </summary>
        public SelectorComponent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorComponent"/> class with a string.
        /// </summary>
        /// <param name="selector">Selector string</param>
        public SelectorComponent(string selector)
        {
            Selector = selector;
        }

        public override JObject ToJObject()
        {
            JObject jObject = base.ToJObject();
            jObject.Add("selector", Selector);
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
            { "show_text", 0 }, { "show_item", 1 }, { "show_entity", 2 }, { "key.attack", 0 },
            { "key.use", 1 }, { "key.forward", 2 }, { "key.left", 3 }, { "key.back", 4 },
            { "key.right", 5 }, { "key.jump", 6 }, { "key.sneak", 7 }, { "key.sprint", 8 },
            { "key.drop", 9 }, { "key.inventory", 10 }, { "key.chat", 11 }, { "key.playerlist", 12 },
            { "key.pickItem", 13 }, { "key.command", 14 }, { "key.screenshot", 15 },
            { "key.togglePerspective", 16 }, { "key.smoothCamera", 17 }, { "key.fullscreen", 18 },
            { "key.spectatorOutlines", 19 }, { "key.swapHands", 20 }, { "key.saveToolbarActivator", 21 },
            { "key.loadToolbarActivator", 22 }, { "key.advancements", 23 }, { "key.hotbar.1", 24 },
            { "key.hotbar.2", 25 }, { "key.hotbar.3", 26 }, { "key.hotbar.4", 27 }, { "key.hotbar.5", 28 },
            { "key.hotbar.6", 29 }, { "key.hotbar.7", 30 }, { "key.hotbar.8", 31 }, { "key.hotbar.9", 32 }
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

            JObject jsonObject;
            try
            {
                jsonObject = JObject.Parse(json);
            }
            catch (JsonReaderException)
            {
                throw new JsonException("Invalid JSON string.");
            }

            Handle(jsonObject);
            json = jsonObject.ToString();
            ChatComponent component = null;

            if (jsonObject.SelectToken("text") != null)
            {
                component = JsonConvert.DeserializeObject<StringComponent>(json);
            }
            else if (jsonObject.SelectToken("translate") != null)
            {
                component = JsonConvert.DeserializeObject<TranslationComponent>(json);
            }
            else if (jsonObject.SelectToken("keybind") != null)
            {
                component = JsonConvert.DeserializeObject<KeybindComponent>(json);
            }
            else if (jsonObject.SelectToken("score") != null)
            {
                component = JsonConvert.DeserializeObject<ScoreComponent>(json);
            }
            else if (jsonObject.SelectToken("selector") != null)
            {
                component = JsonConvert.DeserializeObject<SelectorComponent>(json);
            }

            if (component != null)
            {
                return new Chat(component);
            }

            throw new JsonException("Invalid JSON string.");
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

            if (!string.IsNullOrEmpty((string)jObject["Keybind"]))
            {
                jObject["Keybind"] = _dict[(string)jObject["Keybind"]];
            }

            var extra = (JArray)jObject["extra"];
            if (extra != null && extra.Count != 0)
            {
                foreach (var comp in extra)
                {
                    Handle((JObject)comp);
                }
            }
        }
    }
}
