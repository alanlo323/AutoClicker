using System;
using System.Collections.Generic;
using System.Text;
using AutoClicker;

namespace AutoClicker.Runtime.Script
{
    internal class TestScript
    {
        public static List<MarcoEvent> GetEvents()
        {
            List<MarcoEvent> marcoEvents = [
                new()
                {
                    Name = "Find Image",
                    DelayAfter = 1000,
                    EventType = MarcoEvent.MarcoEventType.FindImageLocation,
                    WindowName = "1.jpg - 小畫家",
                    ImageFilePath = "D:\\Git\\AutoClicker\\AutoClicker.Runtime\\App_Data\\1.jpg",
                    ResultKey = "buttonLocation1",
                },
                new()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                    RefKey = "buttonLocation1",
                },
                new()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                    RefKey = "buttonLocation1",
                    MouseKey = MarcoEvent.MouseKeyType.LeftClick,
                    KeyEvent = MarcoEvent.KeyEventType.Press
                },
            ];

            return marcoEvents;
        }
    }
}
