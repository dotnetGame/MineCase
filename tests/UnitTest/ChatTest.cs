using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Formats;
using Newtonsoft.Json.Linq;
using Xunit;
using Newtonsoft.Json;

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
            stringComponent.Color = "blue";
            stringComponent.ClickEvent = new ChatClickEvent(ClickEventType.OpenUrl, @"http://case.orz");
            stringComponent.Extra = new JArray(JObject.Parse(@"{""text"":""foo""}"), JObject.Parse(@"{""text"":""bar""}"));
            chat.Component = stringComponent;

            JObject o = chat.ToJObject();
            JObject o2 = JObject.Parse(chat.ToString());
            Assert.Equal("hello case!", o.GetValue("text").Value<string>());
            Assert.True(o.GetValue("bold").Value<bool>());
            Assert.Equal("blue", o.GetValue("color").Value<string>());
            Assert.Equal("open_url", (string)o.SelectToken("clickEvent.action"));
            Assert.Equal(@"http://case.orz", (string)o.SelectToken("clickEvent.value"));
            Assert.Equal("foo", (string)o.SelectToken("extra[0].text"));
            Assert.Equal("bar", (string)o.SelectToken("extra[1].text"));
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
            stringComponent.Color = "green";
            ChatClickEvent chatClickEvent = new ChatClickEvent();
            chatClickEvent.Action = ClickEventType.ChangePage;
            chatClickEvent.Value = 1;
            stringComponent.ClickEvent = chatClickEvent;

            Chat chat = new Chat(stringComponent);
            Assert.Equal("{\"bold\":true,\"color\":\"green\",\"clickEvent\":{\"action\":\"change_page\",\"value\":1},\"text\":\"hello\"}", chat.ToString());
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
                    { ""text"":""foo"" },
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
            Assert.Equal("red", sc.Color);
            Assert.Equal(ClickEventType.ChangePage, sc.ClickEvent.Action);
            Assert.Equal(1, sc.ClickEvent.Value.Value<int>());
            Assert.Equal("foo", jObject.SelectToken("extra[0].text"));
            Assert.Equal("bar", jObject.SelectToken("extra[1].text"));
        }
    }
}
