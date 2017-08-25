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
    /// Available types for color of Compoent.
    /// </summary>
    public enum ColorType
    {
        Black = 0,
        DarkBlue = 1,
        DarkGreen = 2,
        DarkAqua = 3,
        DarkRed = 4,
        DarkPurple = 5,
        Gold = 6,
        Gray = 7,
        DarkGray = 8,
        Blue = 9,
        Green = 10,
        Aqua = 11,
        Red = 12,
        LightPurple = 13,
        Yellow = 14,
        White = 15,
        Reset = 16
    }

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
    public class ChatComponent
    {
        private static readonly string[] _map = new string[17]
        {
            "black", "dark_blue", "dark_green", "dark_aqua", "dark_red",
            "dark_purple", "gold", "gray", "dark_gray", "blue", "green",
            "aqua", "red", "light_purple", "yellow", "white", "reset"
        };

        public bool? Bold { get; set; }

        public bool? Itatic { get; set; }

        public bool? Underlined { get; set; }

        public bool? Strikethrough { get; set; }

        public bool? Obfuscated { get; set; }

        public string Insertion { get; set; }

        public ColorType? Color { get; set; }

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
            AddStringValue(jObject, "insertion", Insertion);

            if (Color != null)
            {
                jObject.Add("color", _map[(int)Color]);
            }

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

                jObject.Add("extra", jArray);
            }

            return jObject;
        }

        private void AddBoolValue(JObject jObject, string key, bool? value)
        {
            if (value != null)
            {
                jObject.Add(key, (bool)value);
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

        public List<ChatComponent> With { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationComponent"/> class.
        /// </summary>
        public TranslationComponent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationComponent"/> class with a string.
        /// </summary>
        /// <param name="translate">Translates text</param>
        public TranslationComponent(string translate)
        {
            Translate = translate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationComponent"/> class with a string and a List.
        /// </summary>
        /// <param name="translate">Translates text</param>
        /// <param name="with">Optional tag</param>
        public TranslationComponent(string translate, List<ChatComponent> with)
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
            JObject score = new JObject();
            score.Add("name", Score.Name);
            score.Add("objective", Score.Objective);
            if (Score.Value != null)
            {
                score.Add("value", Score.Value);
            }

            jObject.Add("score", score);
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
            { "key.hotbar.6", 29 }, { "key.hotbar.7", 30 }, { "key.hotbar.8", 31 }, { "key.hotbar.9", 32 },
            { "black", 0 }, { "dark_blue", 1 }, { "dark_green", 2 }, { "dark_aqua", 3 }, { "dark_red", 4 },
            { "dark_purple", 5 }, { "gold", 6 }, { "gray", 7 }, { "dark_gray", 8 }, { "blue", 9 },
            { "green", 10 }, { "aqua", 11 }, { "red", 12 }, { "light_purple", 13 }, { "yellow", 14 },
            { "white", 15 }, { "reset", 16 }
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
        /// Initializes a new instance of the <see cref="Chat"/> class with a string.
        /// It is convenient to initialize a Chat class with a StringComponent.
        /// </summary>
        /// <param name="text">The text of the StringComponent</param>
        public Chat(string text)
        {
            Component = new StringComponent(text);
        }

        /// <summary>
        /// Parses Chat from a JSON string.
        /// </summary>
        /// <exception>
        /// Newtonsoft.Json.JsonException
        /// </exception>
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
            return new Chat(ParseCompoent(jsonObject));
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

        private static ChatComponent ParseCompoent(JObject jsonObject)
        {
            ChatComponent component = null;

            // Construct the special part
            if (jsonObject.SelectToken("selector") != null)
            {
                component = new SelectorComponent(jsonObject.SelectToken("selector").Value<string>());
            }
            else if (jsonObject.SelectToken("score") != null)
            {
                ChatScore chatScore = new ChatScore(
                    jsonObject.SelectToken("score.name").Value<string>(),
                    jsonObject.SelectToken("score.objective").Value<string>());
                if (jsonObject.SelectToken("score.value") != null)
                {
                    chatScore.Value = jsonObject.SelectToken("score.value").Value<int>();
                }

                component = new ScoreComponent(chatScore);
            }
            else if (jsonObject.SelectToken("text") != null)
            {
                component = new StringComponent(jsonObject.SelectToken("text").Value<string>());
            }
            else if (jsonObject.SelectToken("translate") != null)
            {
                component = new TranslationComponent(jsonObject.SelectToken("translate").Value<string>());
                if (jsonObject.SelectToken("with") != null)
                {
                    // There must be elements when the `with` is not null
                    ((TranslationComponent)component).With = new List<ChatComponent>();
                    var with = (JArray)jsonObject.SelectToken("with");
                    foreach (var comp in with)
                    {
                        ((TranslationComponent)component).With.Add(ParseCompoent((JObject)comp));
                    }
                }
            }
            else if (jsonObject.SelectToken("keybind") != null)
            {
                component = new KeybindComponent((KeyBindType)jsonObject.SelectToken("keybind").Value<int>());
            }

            if (component == null)
            {
                throw new JsonException("Invalid JSON string.");
            }

            // Construct the common part
            ConstructCommonPart(jsonObject, component);

            return component;
        }

        private static void ConstructCommonPart(JObject jsonObject, ChatComponent component)
        {
            if (jsonObject.SelectToken("bold") != null)
            {
                component.Bold = jsonObject.SelectToken("bold").Value<bool>();
            }

            if (jsonObject.SelectToken("itatic") != null)
            {
                component.Itatic = jsonObject.SelectToken("itatic").Value<bool>();
            }

            if (jsonObject.SelectToken("underlined") != null)
            {
                component.Underlined = jsonObject.SelectToken("underlined").Value<bool>();
            }

            if (jsonObject.SelectToken("strikethrough") != null)
            {
                component.Strikethrough = jsonObject.SelectToken("strikethrough").Value<bool>();
            }

            if (jsonObject.SelectToken("obfuscated") != null)
            {
                component.Obfuscated = jsonObject.SelectToken("obfuscated").Value<bool>();
            }

            if (jsonObject.SelectToken("insertion") != null)
            {
                component.Insertion = jsonObject.SelectToken("insertion").Value<string>();
            }

            if (jsonObject.SelectToken("color") != null)
            {
                component.Color = (ColorType)jsonObject.SelectToken("color").Value<int>();
            }

            if (jsonObject.SelectToken("clickEvent") != null)
            {
                component.ClickEvent = new ChatClickEvent(
                    (ClickEventType)jsonObject.SelectToken("clickEvent.action").Value<int>(),
                    jsonObject.SelectToken("clickEvent.value"));
            }

            if (jsonObject.SelectToken("hoverEvent") != null)
            {
                component.HoverEvent = new ChatHoverEvent(
                    (HoverEventType)jsonObject.SelectToken("hoverEvent.action").Value<int>(),
                    jsonObject.SelectToken("hoverEvent.value"));
            }

            if (jsonObject.SelectToken("extra") != null)
            {
                component.Extra = new List<ChatComponent>();
                var extra = (JArray)jsonObject.SelectToken("extra");
                foreach (var comp in extra)
                {
                    component.Extra.Add(ParseCompoent((JObject)comp));
                }
            }
        }

        private static void Handle(JObject jObject)
        {
            if (jObject.SelectToken("color") != null)
            {
                jObject["color"] = _dict[(string)jObject["color"]];
            }

            if (jObject.SelectToken("clickEvent.action") != null)
            {
                jObject["clickEvent"]["action"] = _dict[(string)jObject["clickEvent"]["action"]];
            }

            if (jObject.SelectToken("hoverEvent.action") != null)
            {
                jObject["hoverEvent"]["action"] = _dict[(string)jObject["hoverEvent"]["action"]];
            }

            if (jObject.SelectToken("keybind") != null)
            {
                jObject["keybind"] = _dict[(string)jObject["keybind"]];
            }

            var with = (JArray)jObject["with"];
            if (with != null && with.Count != 0)
            {
                foreach (var comp in with)
                {
                    Handle((JObject)comp);
                }
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
