using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Formats;
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
            stringComponent.Color = "#FFFFFF";
            ChatClickEvent chatClickEvent = new ChatClickEvent();
            chatClickEvent.Action = ClickEventType.OpenUrl;
            chatClickEvent.Value = @"http://case.orz";
            stringComponent.ClickEvent = chatClickEvent;

            chat.Component = stringComponent;
            JObject o = chat.ToJObject();
            JObject o2 = JObject.Parse(chat.ToString());
            Assert.Equal("hello case!", o.GetValue("text").Value<string>());
            Assert.True(o.GetValue("bold").Value<bool>());
            Assert.Equal("#FFFFFF", o.GetValue("color").Value<string>());
            Assert.Equal(JToken.Parse(chatClickEvent.ToJObject().ToString()), o.GetValue("clickEvent"));
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
            stringComponent.Color = "#FFFFFF";
            ChatClickEvent chatClickEvent = new ChatClickEvent();
            chatClickEvent.Action = ClickEventType.ChangePage;
            chatClickEvent.Value = 1;
            stringComponent.ClickEvent = chatClickEvent;

            Chat chat = new Chat(stringComponent);
            Assert.Equal("{\"bold\":true,\"color\":\"#FFFFFF\",\"clickEvent\":{\"action\":\"change_page\",\"value\":1},\"text\":\"hello\"}", chat.ToString());
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
                ""color"":""#FFFFFF"",
                ""clickEvent"":{
                    ""action"":""change_page"",
                    ""value"":1
                }
            }";
            Chat chat = Chat.Parse(json);
            StringComponent sc = (StringComponent)chat.Component;
            Assert.Equal("case", sc.Text);
            Assert.True(sc.Bold);
            Assert.True(sc.Itatic);
            Assert.Equal("#FFFFFF", sc.Color);
            Assert.Equal(ClickEventType.ChangePage, sc.ClickEvent.Action);
            Assert.Equal(1, sc.ClickEvent.Value.Value<int>());
        }
    }
}
