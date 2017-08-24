﻿using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Formats;
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
            Chat chat = new Chat();
            StringComponent stringComponent = new StringComponent();
            stringComponent.Text = "hello case!";
            stringComponent.Bold = true;
            stringComponent.Itatic = false;
            stringComponent.Color = ColorType.Blue;
            stringComponent.ClickEvent = new ChatClickEvent(ClickEventType.OpenUrl, @"http://case.orz");
            chat.Component = stringComponent;

            JObject o = chat.ToJObject();
            JObject o2 = JObject.Parse(chat.ToString());
            Assert.Equal("hello case!", o.GetValue("text").Value<string>());
            Assert.True(o.GetValue("bold").Value<bool>());
            Assert.Equal("blue", o.GetValue("color").Value<string>());
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
            StringComponent stringComponent = new StringComponent();
            stringComponent.Text = "hello";
            stringComponent.Bold = true;
            stringComponent.Itatic = false;
            stringComponent.Color = ColorType.Green;
            ChatClickEvent chatClickEvent = new ChatClickEvent();
            chatClickEvent.Action = ClickEventType.ChangePage;
            chatClickEvent.Value = 1;
            stringComponent.ClickEvent = chatClickEvent;

            Chat chat = new Chat(stringComponent);
            Assert.Equal("{\"bold\":true,\"itatic\":false,\"color\":\"green\",\"clickEvent\":{\"action\":\"change_page\",\"value\":1},\"text\":\"hello\"}", chat.ToString());
        }

        /// <summary>
        /// Tests Chat.Parse.
        /// </summary>
        [Fact]
        public void Test3()
        {
            string json = @"{
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
            string json2 = @"{""text"":}";

            Assert.Throws<JsonException>(() => Chat.Parse(json2));

            Chat chat = Chat.Parse(json);
            JObject jObject = JObject.Parse(json);

            StringComponent sc = (StringComponent)chat.Component;
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
            Chat chat = new Chat("text");
            chat.Component.Bold = true;
            chat.Component.Itatic = false;
            chat.Component.Insertion = "insert";
            chat.Component.Color = ColorType.Red;
            chat.Component.ClickEvent = new ChatClickEvent(ClickEventType.ChangePage, 1);
            chat.Component.HoverEvent = new ChatHoverEvent(HoverEventType.ShowText, "show");
            KeybindComponent keybind = new KeybindComponent();
            keybind.Bold = false;
            keybind.Itatic = true;
            keybind.Color = ColorType.Green;
            chat.Component.Extra = new List<ChatComponent>() { keybind };

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
            Assert.Equal("insert", json.SelectToken("insertion"));
            Assert.Equal("change_page", json.SelectToken("clickEvent.action"));
            Assert.True((int)json.SelectToken("clickEvent.value") == 1);
            Assert.Equal("show_text", json.SelectToken("hoverEvent.action"));
            Assert.Equal("show", json.SelectToken("hoverEvent.value"));
        }

        /// <summary>
        /// Tests TranslationComponent.
        /// </summary>
        [Fact]
        public void Test5()
        {
            StringComponent sc = new StringComponent("text");
            sc.Bold = true;
            sc.ClickEvent = new ChatClickEvent(ClickEventType.RunCommand, "/msg a");
            List<ChatComponent> list = new List<ChatComponent>();
            list.Add(sc);
            list.Add(new StringComponent("nothing"));

            Chat chat = new Chat(new TranslationComponent("chat.type.text", list));
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
            var jObject2 = chat2.ToJObject();
            Assert.True(JToken.DeepEquals(jObject, jObject2));
        }
    }
}
