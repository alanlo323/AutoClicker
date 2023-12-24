using System;
using System.Collections.Generic;
using System.Text;
using AutoClicker.Runtime.Core;
using AutoClicker.UI.Script;

namespace AutoClicker.UI.Script.Sample
{
    internal class MabinogiScriptOri : BaseScipt
    {
        public override string Name { get; set; } = nameof(MabinogiScriptOri);
        public override List<MarcoEvent> MarcoEvents
        {
            get
            {
                string windowName = "新瑪奇 mabinogi";
                var maxMoneyInBag = 1130 * 10000;
                var stickPrice = 927 * 99;
                var stickLoopCount = 6;

                List<MarcoEvent> marcoEvents = [
                    new()
                    {
                        Name = "置頂瑪奇",
                        ShowInLogger = true,
                        EventType = MarcoEvent.MarcoEventType.FocusWindow,
                        WindowName = windowName,
                    },
                    new()
                    {
                        Name = "尋找圖片 - 棍",
                        DelayAfter = 1000,
                        EventType = MarcoEvent.MarcoEventType.FindImage,
                        WindowName = windowName,
                        ImageFilePath = "App_Data\\stick.png",
                        ImageSearchingArea = new System.Drawing.Rectangle(3374, 0, 467, 447),
                        SaveToVariable = "StickLocation",
                    },
                    new()
                    {
                        Name = "主循環",
                        ShowInLogger = true,
                        Repeat = maxMoneyInBag / (stickPrice * stickLoopCount),
                        SubEvents = [
                            new()
                            {
                                Name = "買棍",
                                ShowInLogger = true,
                                DelayBefore = 500,
                                Repeat = stickLoopCount,
                                SubEvents = [
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                                        MouseMoveX = 2322,
                                        MouseMoveY = 104,
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                                        MouseKey = MarcoEvent.MouseKeyType.LeftKey,
                                        KeyEvent = MarcoEvent.KeyEventType.Press
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                                        MouseMoveX = 2372,
                                        MouseMoveY = 250,
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                                        MouseKey = MarcoEvent.MouseKeyType.LeftKey,
                                        KeyEvent = MarcoEvent.KeyEventType.Press,
                                        DelayAfter = 10,
                                        Repeat = 10,
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                                        MouseMoveX = 2330,
                                        MouseMoveY = 300,
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                                        MouseKey = MarcoEvent.MouseKeyType.LeftKey,
                                        KeyEvent = MarcoEvent.KeyEventType.Press
                                    },
                                ],
                            },
                            new()
                            {
                                Name = "切換至其他",
                                ShowInLogger = true,
                                SubEvents = [
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                                        MouseMoveX = 1130,
                                        MouseMoveY = 450,
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                                        MouseKey = MarcoEvent.MouseKeyType.LeftKey,
                                        KeyEvent = MarcoEvent.KeyEventType.Press
                                    },
                                    new()
                                    {
                                        DelayBefore = 500,
                                        EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                                        MouseMoveX = 1512,
                                        MouseMoveY = 600,
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                                        MouseKey = MarcoEvent.MouseKeyType.LeftKey,
                                        KeyEvent = MarcoEvent.KeyEventType.Press
                                    },
                                ],
                            },
                            new()
                            {
                                DelayBefore = 250,
                                Name = "喂精武",
                                ShowInLogger = true,
                                Repeat = (int)Math.Round((decimal)stickLoopCount * 99 / 30, MidpointRounding.ToPositiveInfinity),
                                DelayAfter = 0,
                                SubEvents = [
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                                        MouseMoveX = 1412,
                                        MouseMoveY = 980,
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                                        MouseKey = MarcoEvent.MouseKeyType.LeftKey,
                                        KeyEvent = MarcoEvent.KeyEventType.Press,
                                        Repeat = 3,
                                        DelayAfter = 0,
                                    },
                                    new()
                                    {
                                        DelayBefore = 100,
                                        EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                                        MouseMoveX = 1617,
                                        MouseMoveY = 980,
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                                        MouseKey = MarcoEvent.MouseKeyType.LeftKey,
                                        KeyEvent = MarcoEvent.KeyEventType.Press,
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                                        MouseMoveX = 1236,
                                        MouseMoveY = 680,
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                                        MouseKey = MarcoEvent.MouseKeyType.LeftKey,
                                        KeyEvent = MarcoEvent.KeyEventType.Press,
                                    },
                                ],
                            }
                            ],
                    }
                    ];

                return marcoEvents;
            }
        }
    }
}
