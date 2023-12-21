﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using AutoClicker.Runtime.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WindowScrape.Static;
using WindowsInput;
using WindowsInput.Native;
using Point = System.Drawing.Point;

namespace AutoClicker.Runtime
{
    internal class MarcoEvent
    {
        public enum MarcoEventType
        {
            EmptyEvent,
            MouseMoveEvent,
            MouseKeyEvent,
            KeyboardEvent,
            FocusWindow,
            FindImageLocation,
        }

        public enum KeyboardKeyType
        {
            K_0,
            K_1,
            K_2,
            K_3,
            K_4,
            K_5,
            K_6,
            K_7,
            K_8,
            K_9,
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H,
            I,
            J,
            K,
            L,
            M,
            N,
            O,
            P,
            Q,
            R,
            S,
            T,
            U,
            V,
            W,
            X,
            Y,
            Z
        }

        public enum MouseKeyType
        {
            LeftClick,
            MiddleClick,
            RightClick
        }

        public enum KeyEventType
        {
            Down,
            Up,
            Press
        }

        public enum RepeatType
        {
            OneTime,
            RepeatNTimes
        }

        public List<MarcoEvent> SubEvents { get; set; } = new List<MarcoEvent>();

        [JsonConverter(typeof(StringEnumConverter))]
        public MarcoEventType EventType { get; set; } = MarcoEventType.EmptyEvent;
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public VirtualKeyCode KeyboardKey { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MouseKeyType MouseKey { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public KeyEventType KeyEvent { get; set; }
        public int MouseMoveX { get; set; }
        public int MouseMoveY { get; set; }
        public int DelayBefore { get; set; } = 0;
        public int DelayAfter { get; set; } = 100;
        public int RepeatCount { get; set; } = 1;
        public string WindowName { get; set; }
        public string ImageFilePath { get; set; }
        public string RefKey { get; set; }
        public string ResultKey { get; set; }
        public bool ShowInLogger { get; set; } = false;

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void BringToFront(string title)
        {
            // Get a handle to the Calculator application.
            IntPtr handle = FindWindow(null, title);

            // Verify that Calculator is a running process.
            if (handle == IntPtr.Zero)
            {
                return;
            }

            // Make Calculator the foreground application
            SetForegroundWindow(handle);
        }

        public void LogWriteLine(string message)
        {
            Console.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff")} | {message}");
        }

        public void Excute()
        {
            MarcoEvent marcoEvent = this;
            InputSimulator inputSimulator = new();

            var resolution = ScreenHelper.GetDisplayResolution();
            double screenWidth = resolution.Width;
            double screenHeight = resolution.Height;

            for (int i = 0; i < marcoEvent.RepeatCount; i++)
            {
                try
                {
                    if (!string.IsNullOrEmpty(marcoEvent.Name))
                    {
                        string msg = $"{marcoEvent.Name}";
                        if (marcoEvent.RepeatCount > 1)
                        {
                            msg += $" {i + 1}/{marcoEvent.RepeatCount}";
                        }
                        if (marcoEvent.ShowInLogger) LogWriteLine(msg);
                    }

                }
                catch (Exception)
                {
                }
                Thread.Sleep(marcoEvent.DelayBefore);

                try
                {
                    #region Event logit
                    switch (marcoEvent.EventType)
                    {
                        case MarcoEventType.FocusWindow:
                            if (marcoEvent.ShowInLogger) LogWriteLine($"Focus window {marcoEvent.WindowName}");
                            BringToFront(marcoEvent.WindowName);
                            break;
                        case MarcoEventType.MouseMoveEvent:
                            if (!string.IsNullOrEmpty(marcoEvent.RefKey))
                            {
                                marcoEvent.MouseMoveX = ((Point)RuntimeDatabase.Default[marcoEvent.RefKey]).X;
                                marcoEvent.MouseMoveY = ((Point)RuntimeDatabase.Default[marcoEvent.RefKey]).Y;
                            }
                            inputSimulator.Mouse.MoveMouseTo(marcoEvent.MouseMoveX / screenWidth * 65535, marcoEvent.MouseMoveY / screenHeight * 65535);
                            if (marcoEvent.ShowInLogger) LogWriteLine($"Move mouse to {marcoEvent.MouseMoveX} {marcoEvent.MouseMoveY}");
                            break;
                        case MarcoEventType.MouseKeyEvent:
                            switch (marcoEvent.MouseKey)
                            {
                                case MouseKeyType.LeftClick:
                                    switch (marcoEvent.KeyEvent)
                                    {
                                        case KeyEventType.Down:
                                            inputSimulator.Mouse.LeftButtonDown();
                                            if (marcoEvent.ShowInLogger) LogWriteLine($"Left mouse down");
                                            break;

                                        case KeyEventType.Up:
                                            inputSimulator.Mouse.LeftButtonUp();
                                            if (marcoEvent.ShowInLogger) LogWriteLine($"Left mouse up");
                                            break;

                                        case KeyEventType.Press:
                                            inputSimulator.Mouse.LeftButtonClick();
                                            if (marcoEvent.ShowInLogger) LogWriteLine($"Left mouse click");
                                            break;

                                        default:
                                            break;
                                    }
                                    break;

                                case MouseKeyType.MiddleClick:
                                    switch (marcoEvent.KeyEvent)
                                    {
                                        case KeyEventType.Down:
                                            if (marcoEvent.ShowInLogger) LogWriteLine($"Middle mouse up");
                                            break;

                                        case KeyEventType.Up:
                                            if (marcoEvent.ShowInLogger) LogWriteLine($"Middle mouse down");
                                            break;

                                        case KeyEventType.Press:
                                            if (marcoEvent.ShowInLogger) LogWriteLine($"Middle mouse click");
                                            break;

                                        default:
                                            break;
                                    }
                                    break;

                                case MouseKeyType.RightClick:
                                    switch (marcoEvent.KeyEvent)
                                    {
                                        case KeyEventType.Down:
                                            inputSimulator.Mouse.RightButtonDown();
                                            if (marcoEvent.ShowInLogger) LogWriteLine($"Right mouse up");
                                            break;

                                        case KeyEventType.Up:
                                            inputSimulator.Mouse.RightButtonUp();
                                            if (marcoEvent.ShowInLogger) LogWriteLine($"Right mouse down");
                                            break;

                                        case KeyEventType.Press:
                                            inputSimulator.Mouse.RightButtonClick();
                                            if (marcoEvent.ShowInLogger) LogWriteLine($"Right mouse click");
                                            break;

                                        default:
                                            break;
                                    }
                                    break;

                                default:
                                    break;
                            }
                            break;
                        case MarcoEventType.KeyboardEvent:
                            switch (marcoEvent.KeyEvent)
                            {
                                case KeyEventType.Down:
                                    inputSimulator.Keyboard.KeyDown(marcoEvent.KeyboardKey);
                                    if (marcoEvent.ShowInLogger) LogWriteLine($"Key {marcoEvent.KeyboardKey} down");
                                    break;

                                case KeyEventType.Up:
                                    inputSimulator.Keyboard.KeyUp(marcoEvent.KeyboardKey);
                                    if (marcoEvent.ShowInLogger) LogWriteLine($"Key {marcoEvent.KeyboardKey} up");
                                    break;

                                case KeyEventType.Press:
                                    inputSimulator.Keyboard.KeyPress(marcoEvent.KeyboardKey);
                                    if (marcoEvent.ShowInLogger) LogWriteLine($"Key {marcoEvent.KeyboardKey} press");
                                    break;

                                default:
                                    break;
                            }
                            break;
                        case MarcoEventType.FindImageLocation:
                            Rectangle rectangle;
                            if (string.IsNullOrEmpty(marcoEvent.WindowName))
                            {
                                rectangle = new(0, 0, (int)screenWidth, (int)screenHeight);
                            }
                            else
                            {
                                var hwnd = HwndInterface.GetHwndFromTitle(marcoEvent.WindowName);
                                var pos = HwndInterface.GetHwndPos(hwnd);
                                var size = HwndInterface.GetHwndSize(hwnd);
                                var factor = ScreenHelper.GetWindowsScreenScalingFactor(false);
                                rectangle = new((int)(pos.X * factor), (int)(pos.Y * factor), size.Width, size.Height);
                                BringToFront(marcoEvent.WindowName);
                                Thread.Sleep(50);
                            }
                            var searchResult = ScreenHelper.FindImageInRectangele(rectangle, marcoEvent.ImageFilePath);
                            if (!string.IsNullOrEmpty(marcoEvent.ResultKey)) RuntimeDatabase.Default[marcoEvent.ResultKey] = searchResult;
                            if (marcoEvent.ShowInLogger) LogWriteLine($"Image found at  {searchResult.X} {searchResult.Y}");
                            break;

                        default:
                            break;
                    }
                    #endregion

                    if (marcoEvent.SubEvents != null)
                    {
                        foreach (MarcoEvent subEvent in marcoEvent.SubEvents)
                        {
                            subEvent.Excute();
                        }
                    }
                }
                catch (Exception)
                {
                }

                Thread.Sleep(marcoEvent.DelayAfter);
            }

        }

    }
}