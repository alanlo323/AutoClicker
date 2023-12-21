using System;
using System.Collections.Generic;
using System.Text;
using AutoClicker.Runtime.Core;
using AutoClicker.UI.Script;

namespace AutoClicker.UI.Script.Sample
{
    internal class MabinogiScript : BaseScipt
    {
        public override string Name { get; set; } = nameof(MabinogiScript);
        public override List<MarcoEvent> MarcoEvents
        {
            get
            {
                var maxMoneyInBag = 1130 * 10000;
                var stickPrice = 927 * 99;
                var stickLoopCount = 6;

                List<MarcoEvent> marcoEvents = [];

                MarcoEvent focusEvent = new()
                {
                    Name = "置頂瑪奇",
                    ShowInLogger = true,
                    EventType = MarcoEvent.MarcoEventType.FocusWindow,
                    WindowName = "新瑪奇 mabinogi",
                };
                MarcoEvent mainEvent = new()
                {
                    Name = "主循環",
                    ShowInLogger = true,
                    RepeatCount = maxMoneyInBag / (stickPrice * stickLoopCount),
                };

                #region 買棍
                MarcoEvent stick = new()
                {
                    Name = "買棍",
                    ShowInLogger = true,
                    DelayBefore = 500,
                    RepeatCount = stickLoopCount,
                };
                List<MarcoEvent> stickContent =
                [
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
                        RepeatCount = 10,
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
                ];
                stick.SubEvents = stickContent;
                #endregion
                #region 切換至其他
                MarcoEvent switchToOthers = new()
                {
                    Name = "切換至其他",
                    ShowInLogger = true,
                };
                List<MarcoEvent> switchToOthersContent =
                [
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
                ];
                switchToOthers.SubEvents = switchToOthersContent;
                #endregion
                #region 喂精武
                MarcoEvent feedTheWeapon = new()
                {
                    DelayBefore = 250,
                    Name = "喂精武",
                    ShowInLogger = true,
                    RepeatCount = (int)Math.Round((decimal)stick.RepeatCount * 99 / 30, MidpointRounding.ToPositiveInfinity),
                    DelayAfter = 0,
                };
                List<MarcoEvent> feedTheWeaponContent =
                [
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
                        RepeatCount = 3,
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
                ];
                feedTheWeapon.SubEvents = feedTheWeaponContent;
                #endregion

                mainEvent.SubEvents.Add(stick);
                mainEvent.SubEvents.Add(switchToOthers);
                mainEvent.SubEvents.Add(feedTheWeapon);
                marcoEvents.Add(focusEvent);
                marcoEvents.Add(mainEvent);

                return marcoEvents;
            }
        }
    }
}
