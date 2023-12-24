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
                WindowName = "新瑪奇 mabinogi",
                ImageFilePath = "App_Data\\1.png",
                ImageSearchingArea = new System.Drawing.Rectangle(3374, 0, 467, 447),
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
                MouseKey = MarcoEvent.MouseKeyType.LeftKey,
                KeyEvent = MarcoEvent.KeyEventType.Press
            },
        ];

    }
}
