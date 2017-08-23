using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MineCase.Formats;
using Newtonsoft.Json.Linq;

namespace MineCase.UnitTest
{
    public class ChatTest
    {
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
            Assert.Equal("hello case!", o.GetValue("text").Value<string>());
            Assert.True(o.GetValue("bold").Value<bool>());
            Assert.Equal("#FFFFFF", o.GetValue("color").Value<string>());
            Assert.Equal(JToken.Parse(chatClickEvent.ToJObject().ToString()), o.GetValue("clickEvent"));
        }
    }
}
