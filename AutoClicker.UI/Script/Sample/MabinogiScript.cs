using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using AutoClicker.Runtime.Core;
using AutoClicker.UI.Script;

namespace AutoClicker.UI.Script.Sample
{
    internal class MabinogiScript : BaseScipt
    {
        private List<MarcoEvent> _marcoEvents;
        public override string Name { get; set; } = nameof(MabinogiScript);
        public override List<MarcoEvent> MarcoEvents
        {
            get
            {
                if (_marcoEvents != null) return _marcoEvents;

                string windowName = "新瑪奇 mabinogi";
                var maxMoneyInBag = 200 * 10_000;
                var stickPrice = 927;
                var stickExp = stickPrice / 25;
                var stickCount = 99;
                var stickTotalPrice = 927 * stickCount;
                var stickLoopCount = 6;
                var maxExpPerWeek = 40 * 10_000;

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
                        Name = "主循環",
                        ShowInLogger = true,
                        Repeat = Math.Min(Math.Max(maxMoneyInBag / (stickTotalPrice * stickLoopCount), 1), maxExpPerWeek / (stickExp * stickCount)),
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
                                        Name = "尋找圖片 - 棍",
                                        EventType = MarcoEvent.MarcoEventType.FindImage,
                                        WindowName = windowName,
                                        ImageFilePath = "App_Data\\Stick.png",
                                        ImageSearchingArea = new System.Drawing.Rectangle(3374, 0, 467, 447),
                                        SaveToVariable = "StickLocation",
                                        SkipIfVariableAlreadyExist = true,
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                                        LoadFromVariable = "StickLocation",
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
                                        MouseMoveX = 0,
                                        MouseMoveY = 0,
                                        DelayAfter = 100,
                                    },
                                    new()
                                    {
                                        Name = "尋找圖片 - 右箭嘴",
                                        EventType = MarcoEvent.MarcoEventType.FindImage,
                                        WindowName = windowName,
                                        ImageFilePath = "App_Data\\DoubleRightArrow.png",
                                        ImageSearchingArea = new System.Drawing.Rectangle(3321, 0, 344, 508),
                                        SaveToVariable = "DoubleRightArrowLocation",
                                        SkipIfVariableAlreadyExist = true,
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                                        LoadFromVariable = "DoubleRightArrowLocation",
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
                                        Name = "尋找圖片 - 購買",
                                        EventType = MarcoEvent.MarcoEventType.FindImage,
                                        WindowName = windowName,
                                        ImageFilePath = "App_Data\\Buy.png",
                                        ImageSearchingArea = new System.Drawing.Rectangle(3321, 0, 344, 508),
                                        SaveToVariable = "BuyButtonLocation",
                                        SkipIfVariableAlreadyExist = true,
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                                        LoadFromVariable = "BuyButtonLocation",
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
                                        Name = "尋找圖片 - 成長1",
                                        EventType = MarcoEvent.MarcoEventType.FindImage,
                                        WindowName = windowName,
                                        ImageFilePath = "App_Data\\成長1.png",
                                        ImageSearchingArea = new System.Drawing.Rectangle(1156, 508, 1520, 1072),
                                        ImageMinSimilarity = 0.9,
                                        SaveToVariable = "GrowthLocation",
                                        SkipIfVariableAlreadyExist = true,
                                    },
                                    new()
                                    {
                                        Name = "尋找圖片 - 成長2",
                                        EventType = MarcoEvent.MarcoEventType.FindImage,
                                        WindowName = windowName,
                                        ImageFilePath = "App_Data\\成長2.png",
                                        ImageMinSimilarity = 0.9,
                                        ImageSearchingArea = new System.Drawing.Rectangle(1156, 508, 1520, 1072),
                                        SaveToVariable = "GrowthLocation",
                                        SkipIfVariableAlreadyExist = true,
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                                        LoadFromVariable = "GrowthLocation",
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                                        MouseKey = MarcoEvent.MouseKeyType.LeftKey,
                                        KeyEvent = MarcoEvent.KeyEventType.Press,
                                        DelayAfter = 500,
                                    },
                                    new()
                                    {
                                        Name = "尋找圖片 - 其他1",
                                        EventType = MarcoEvent.MarcoEventType.FindImage,
                                        WindowName = windowName,
                                        ImageFilePath = "App_Data\\其他1.png",
                                        ImageSearchingArea = new System.Drawing.Rectangle(1156, 508, 1520, 1072),
                                        ImageMinSimilarity = 0.9,
                                        SaveToVariable = "OtherLocation",
                                        SkipIfVariableAlreadyExist = true,
                                    },
                                    new()
                                    {
                                        Name = "尋找圖片 - 其他2",
                                        EventType = MarcoEvent.MarcoEventType.FindImage,
                                        WindowName = windowName,
                                        ImageFilePath = "App_Data\\其他2.png",
                                        ImageSearchingArea = new System.Drawing.Rectangle(1156, 508, 1520, 1072),
                                        ImageMinSimilarity = 0.9,
                                        SaveToVariable = "OtherLocation",
                                        SkipIfVariableAlreadyExist = true,
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                                        LoadFromVariable = "OtherLocation",
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                                        MouseKey = MarcoEvent.MouseKeyType.LeftKey,
                                        KeyEvent = MarcoEvent.KeyEventType.Press,
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
                                        Name = "尋找圖片 - 自動選擇",
                                        EventType = MarcoEvent.MarcoEventType.FindImage,
                                        WindowName = windowName,
                                        ImageFilePath = "App_Data\\AutoSelect.png",
                                        ImageSearchingArea = new System.Drawing.Rectangle(1156, 508, 1520, 1072),
                                        ImageMinSimilarity = 0.9,
                                        SaveToVariable = "AutoSelectLocation",
                                        SkipIfVariableAlreadyExist = true,
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                                        LoadFromVariable = "AutoSelectLocation",
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
                                        Name = "尋找圖片 - 給予道具",
                                        EventType = MarcoEvent.MarcoEventType.FindImage,
                                        WindowName = windowName,
                                        ImageFilePath = "App_Data\\GiveItem.png",
                                        ImageSearchingArea = new System.Drawing.Rectangle(1156, 508, 1520, 1072),
                                        ImageMinSimilarity = 0.9,
                                        SaveToVariable = "GiveItemLocation",
                                        SkipIfVariableAlreadyExist = true,
                                    },
                                    new()
                                    {
                                        DelayBefore = 100,
                                        EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                                        LoadFromVariable = "GiveItemLocation",
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                                        MouseKey = MarcoEvent.MouseKeyType.LeftKey,
                                        KeyEvent = MarcoEvent.KeyEventType.Press,
                                    },
                                    new()
                                    {
                                        Name = "尋找圖片 - 確認",
                                        EventType = MarcoEvent.MarcoEventType.FindImage,
                                        WindowName = windowName,
                                        ImageFilePath = "App_Data\\Comfirm.png",
                                        ImageSearchingArea = new System.Drawing.Rectangle(1156, 508, 1520, 1072),
                                        ImageMinSimilarity = 0.9,
                                        SaveToVariable = "ComfirmLocation",
                                        SkipIfVariableAlreadyExist = true,
                                    },
                                    new()
                                    {
                                        EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                                        LoadFromVariable = "ComfirmLocation",
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
                _marcoEvents = marcoEvents;
                return _marcoEvents;
            }
        }
    }
}
