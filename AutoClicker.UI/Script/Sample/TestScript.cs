using System;
using System.Collections.Generic;
using System.Text;
using AutoClicker.Runtime.Core;
using AutoClicker.UI.Script;

namespace AutoClicker.UI.Script.Sample
{
    internal class TestScript : BaseScipt
    {
        public override string Name { get; set; } = nameof(TestScript);
        public override List<MarcoEvent> MarcoEvents { get; } =
        [
            new()
            {
                DelayAfter = 1000,
                EventType = MarcoEvent.MarcoEventType.FindImage,
                WindowName = "1.jpg - 小畫家",
                ImageFilePath = "App_Data\\1.jpg",
                ResultKey = "buttonLocation1",
            },
            new()
            {
                EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                RefKey = "buttonLocation1",
            },
            new()
            {
                Name = "Mouse click",
                EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                RefKey = "buttonLocation1",
                MouseKey = MarcoEvent.MouseKeyType.LeftKey,
                KeyEvent = MarcoEvent.KeyEventType.Press
            },
        ];

    }
}
