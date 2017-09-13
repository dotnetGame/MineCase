using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MineCase.UnitTest
{
    public class ChatTest
    {
        /// <summary>
        /// Tests Chat.ToJObject.
        /// </summary>
        [Fact]
        public void Test1()
        {
            var chat = new Chat(new StringComponent
            {
                Text = "hello case!",
                Bold = true,
                Itatic = false,
                Color = ColorType.Blue,
                ClickEvent = new ChatClickEvent(ClickEventType.OpenUrl, @"http://case.orz")
            });

            var o = chat.ToJObject();
            var o2 = JObject.Parse(chat.ToString());
            Assert.Equal("hello case!", o.GetValue("text"));
            Assert.True(o.GetValue("bold").Value<bool>());
            Assert.Equal("blue", o.GetValue("color"));
            Assert.Equal("open_url", (string)o.SelectToken("clickEvent.action"));
            Assert.Equal(@"http://case.orz", (string)o.SelectToken("clickEvent.value"));
            Assert.True(JToken.DeepEquals(o, o2));
        }

        /// <summary>
        /// Tests Chat.ToString.
        /// </summary>
        [Fact]
        public void Test2()
        {
            var stringComponent = new StringComponent
            {
                Text = "hello",
                Bold = true,
                Itatic = false,
                Color = ColorType.Green,
                ClickEvent = new ChatClickEvent
                {
                    Action = ClickEventType.ChangePage,
                    Value = 1
                }
            };

            var chat = new Chat(stringComponent);
            Assert.Equal("{\"bold\":true,\"itatic\":false,\"color\":\"green\",\"clickEvent\":{\"action\":\"change_page\",\"value\":1},\"text\":\"hello\"}", chat.ToString());
        }

        /// <summary>
        /// Tests Chat.Parse.
        /// </summary>
        [Fact]
        public void Test3()
        {
            const string json = @"{
                ""text"":""case"",
                ""bold"":true,
                ""itatic"":true,
                ""color"":""red"",
                ""clickEvent"":{
                    ""action"":""change_page"",
                    ""value"":1
                },
                ""extra"":[
                    { ""text"":""foo"",""bold"":false },
                    { ""text"":""bar"" }
                ]
            }";

            var chat = Chat.Parse(json);
            var jObject = JObject.Parse(json);

            var sc = (StringComponent)chat.Component;
            Assert.Equal("case", sc.Text);
            Assert.True(sc.Bold);
            Assert.True(sc.Itatic);
            Assert.Equal(ColorType.Red, sc.Color);
            Assert.Equal(ClickEventType.ChangePage, sc.ClickEvent.Action);
            Assert.Equal(1, sc.ClickEvent.Value.Value<int>());
            Assert.False(jObject.SelectToken("extra[0].bold").Value<bool>());
            Assert.Equal("foo", jObject.SelectToken("extra[0].text"));
            Assert.Equal("bar", jObject.SelectToken("extra[1].text"));
        }

        /// <summary>
        /// More test cases.
        /// </summary>
        [Fact]
        public void Test4()
        {
            var chat = new Chat("text")
            {
                Component =
                {
                    Bold = true,
                    Itatic = false,
                    Insertion = "insert",
                    Color = ColorType.Red,
                    ClickEvent = new ChatClickEvent(ClickEventType.ChangePage, 1),
                    HoverEvent = new ChatHoverEvent(HoverEventType.ShowText, "show")
                }
            };
            var keybind = new KeybindComponent
            {
                Keybind = KeyBindType.Back,
                Bold = false,
                Itatic = true,
                Color = ColorType.Green
            };
            chat.Component.Extra = new List<ChatComponent> { keybind };

            var json = chat.ToJObject();
            Assert.Null(json.SelectToken("underlined"));
            Assert.Null(json.SelectToken("strikethrough"));
            Assert.Null(json.SelectToken("obfuscated"));
            Assert.True(json.SelectToken("bold").Value<bool>());
            Assert.False(json.SelectToken("extra[0].bold").Value<bool>());
            Assert.False(json.SelectToken("itatic").Value<bool>());
            Assert.True(json.SelectToken("extra[0].itatic").Value<bool>());
            Assert.Equal("red", json.SelectToken("color"));
            Assert.Equal("green", json.SelectToken("extra[0].color"));
            Assert.Equal("key.back", json.SelectToken("extra[0].keybind"));
            Assert.Equal("insert", json.SelectToken("insertion"));
            Assert.Equal("change_page", json.SelectToken("clickEvent.action"));
            Assert.Equal(1, json.SelectToken("clickEvent.value").Value<int>());
            Assert.Equal("show_text", json.SelectToken("hoverEvent.action"));
            Assert.Equal("show", json.SelectToken("hoverEvent.value"));
        }

        /// <summary>
        /// Tests TranslationComponent.
        /// </summary>
        [Fact]
        public void Test5()
        {
            var sc = new StringComponent("text")
            {
                Bold = true,
                ClickEvent = new ChatClickEvent(ClickEventType.RunCommand, "/msg a")
            };
            var list = new List<ChatComponent> { sc, new StringComponent("nothing") };

            var chat = new Chat(new TranslationComponent("chat.type.text", list));
            var jObject = chat.ToJObject();
            Assert.Equal("chat.type.text", jObject.SelectToken("translate"));
            Assert.True(jObject.SelectToken("with[0].bold").Value<bool>());
            Assert.Equal("run_command", jObject.SelectToken("with[0].clickEvent.action"));
            Assert.Equal("/msg a", jObject.SelectToken("with[0].clickEvent.value"));
            Assert.Equal("text", jObject.SelectToken("with[0].text"));
            Assert.Equal("nothing", jObject.SelectToken("with[1].text"));

            string json = @"{
                ""translate"":""chat.type.text"",
                ""with"":[
                    {
                        ""bold"":true,
                        ""text"":""text"",
                        ""clickEvent"":{""action"":""run_command"",""value"":""/msg a""}
                    },
                    {
                        ""text"":""nothing"" 
                    }
                ]
            }";
            var chat2 = Chat.Parse(json);
            Assert.True(JToken.DeepEquals(jObject, chat2.ToJObject()));
        }

        /// <summary>
        /// Tests KeybindComponent.
        /// </summary>
        [Fact]
        public void Test6()
        {
            var keybind = new KeybindComponent(KeyBindType.Attack)
            {
                Extra = new List<ChatComponent>
                {
                    new StringComponent("text1"),
                    new StringComponent("text2")
                }
            };

            var chat = new Chat(keybind);
            var j = chat.ToJObject();
            Assert.Equal("key.attack", j.SelectToken("keybind"));
            Assert.Equal("text1", j.SelectToken("extra[0].text"));
            Assert.Equal("text2", j.SelectToken("extra[1].text"));

            const string json = @"{
                ""keybind"":""key.attack"",
                ""extra"":[
                    { ""text"":""text1"" },
                    { ""text"":""text2"" }
                ]
            }";
            var chat2 = Chat.Parse(json);
            Assert.True(JToken.DeepEquals(j, chat2.ToJObject()));
        }

        /// <summary>
        /// Tests ScoreComponent.
        /// </summary>
        [Fact]
        public void Test7()
        {
            var score = new ScoreComponent(new ChatScore("case", "ball", 100))
            {
                Extra = new List<ChatComponent>
                {
                    new StringComponent("text1"),
                    new StringComponent("text2")
                }
            };

            var chat = new Chat(score);
            var j = chat.ToJObject();
            Assert.Equal("case", j.SelectToken("score.name"));
            Assert.Equal("ball", j.SelectToken("score.objective"));
            Assert.Equal(100, j.SelectToken("score.value").Value<int>());
            Assert.Equal("text1", j.SelectToken("extra[0].text"));
            Assert.Equal("text2", j.SelectToken("extra[1].text"));

            const string json = @"{
                ""score"":{
                    ""name"":""case"",
                    ""objective"":""ball"",
                    ""value"":100
                },
                ""extra"":[
                    { ""text"":""text1"" },
                    { ""text"":""text2"" }
                ]
            }";
            var chat2 = Chat.Parse(json);
            Assert.True(JToken.DeepEquals(j, chat2.ToJObject()));
        }

        /// <summary>
        /// Tests SelectorComponent.
        /// </summary>
        [Fact]
        public void Test8()
        {
            var chat = new Chat(new SelectorComponent("@p"))
            {
                Component =
                {
                    Extra = new List<ChatComponent>
                    {
                        new StringComponent("text1"),
                        new StringComponent("text2")
                    }
                }
            };

            var j = chat.ToJObject();
            Assert.Equal("@p", j.SelectToken("selector"));
            Assert.Equal("text1", j.SelectToken("extra[0].text"));
            Assert.Equal("text2", j.SelectToken("extra[1].text"));

            const string json = @"{
                ""selector"":""@p"",
                ""extra"":[
                    { ""text"":""text1"" },
                    { ""text"":""text2"" }
                ]
            }";
            var chat2 = Chat.Parse(json);
            Assert.True(JToken.DeepEquals(j, chat2.ToJObject()));
        }

        /// <summary>
        /// Tests exceptions.
        /// </summary>
        [Fact]
        public void Test9()
        {
            // parse exception
            const string json = @"{""text"":}";
            Assert.Throws<JsonException>(() => Chat.Parse(json));

            // no key exception
            Chat chat = new Chat();  // no compoent
            Assert.Throws<InvalidOperationException>(() => chat.ToJObject());

            // no key exception
            var comp = new StringComponent();
            Chat chat2 = new Chat(comp); // compoent no specified key
            Assert.Throws<InvalidOperationException>(() => chat2.ToJObject());

            // no key exception
            Chat chat3 = new Chat(
                new ScoreComponent(
                    new ChatScore
                    {
                        Name = "case" // no name key or objective key
                    }));
            Assert.Throws<InvalidOperationException>(() => chat3.ToJObject());
        }
    }
}
