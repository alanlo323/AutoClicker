using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static AutoClicker.Runtime.Helper.ScreenHelper;
using Point = System.Drawing.Point;

namespace AutoClicker.Runtime.Core
{
    public class MarcoEvent
    {
        private const int DefaultDelayBefore = 0;
        private const int DefaultDelayAfter = 100;

        public enum MarcoEventType
        {
            EmptyEvent,
            MouseMoveEvent,
            MouseKeyEvent,
            KeyboardEvent,
            FocusWindow,
            FindImage,
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
            LeftKey,
            MiddleKey,
            RightKey
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

        public List<MarcoEvent> SubEvents { get; set; } = [];

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
        public int DelayAfter { get; set; } = DefaultDelayAfter;
        public int Repeat { get; set; } = 1;
        public string WindowName { get; set; }
        public string ImageFilePath { get; set; }
        public double ImageMinSimilarity { get; set; } = 0.85;
        public Rectangle? ImageSearchingArea { get; set; }
        public string LoadFromVariable { get; set; }
        public string SaveToVariable { get; set; }
        public bool SkipIfVariableAlreadyExist { get; set; } = false;
        public bool ShowInLogger { get; set; } = false;

        public int Layer { get; set; } = 0;
        public bool IsRunning { get; set; } = false;

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern nint FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(nint hWnd);

        public static void BringToFront(string title)
        {
            // Get a handle to the Calculator application.
            nint handle = FindWindow(null, title);

            // Verify that Calculator is a running process.
            if (handle == nint.Zero)
            {
                return;
            }

            // Make Calculator the foreground application
            SetForegroundWindow(handle);
        }

        public void LogWriteLine(string message)
        {
            Console.WriteLine($"{DateTime.Now:MM/dd/yyyy hh:mm:ss.fff} | {message}");
            Debug.WriteLine($"{DateTime.Now:MM/dd/yyyy hh:mm:ss.fff} | {message}");
        }

        public async Task Excute(Dictionary<object, object> runtimeDatabase, MarcoEventStatusChangedEventHandler handler)
        {
            IsRunning = true;
            handler(this);
            try
            {
                runtimeDatabase ??= RuntimeDatabase.Default;
                MarcoEvent marcoEvent = this;
                InputSimulator inputSimulator = new();

                var resolution = ScreenHelper.GetDisplayResolution();
                double screenWidth = resolution.Width;
                double screenHeight = resolution.Height;

                for (int i = 0; i < marcoEvent.Repeat; i++)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(marcoEvent.Name))
                        {
                            string msg = $"{marcoEvent.Name}";
                            if (marcoEvent.Repeat > 1)
                            {
                                msg += $" {i + 1}/{marcoEvent.Repeat}";
                            }
                            if (marcoEvent.ShowInLogger) LogWriteLine(msg);
                        }

                    }
                    catch (Exception)
                    {
                    }

                    if (marcoEvent.SkipIfVariableAlreadyExist == true && !string.IsNullOrEmpty(marcoEvent.SaveToVariable) && runtimeDatabase.ContainsKey(marcoEvent.SaveToVariable)) break;

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
                                if (!string.IsNullOrEmpty(marcoEvent.LoadFromVariable))
                                {
                                    marcoEvent.MouseMoveX = ((Point)runtimeDatabase[marcoEvent.LoadFromVariable]).X;
                                    marcoEvent.MouseMoveY = ((Point)runtimeDatabase[marcoEvent.LoadFromVariable]).Y;
                                }
                                inputSimulator.Mouse.MoveMouseTo(marcoEvent.MouseMoveX / screenWidth * 65535, marcoEvent.MouseMoveY / screenHeight * 65535);
                                if (marcoEvent.ShowInLogger) LogWriteLine($"Move mouse to {marcoEvent.MouseMoveX} {marcoEvent.MouseMoveY}");
                                break;
                            case MarcoEventType.MouseKeyEvent:
                                switch (marcoEvent.MouseKey)
                                {
                                    case MouseKeyType.LeftKey:
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

                                    case MouseKeyType.MiddleKey:
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

                                    case MouseKeyType.RightKey:
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
                            case MarcoEventType.FindImage:
                                Rectangle rectangle;
                                if (string.IsNullOrEmpty(marcoEvent.WindowName))
                                {
                                    rectangle = new(0, 0, (int)screenWidth, (int)screenHeight);
                                }
                                else
                                {
                                    var hwnd = HwndInterface.GetHwndFromTitle(marcoEvent.WindowName);
                                    if (hwnd == 0x0000000000000000)
                                    {
                                        LogWriteLine($"Window {marcoEvent.WindowName} not found"); break;
                                    }
                                    var pos = HwndInterface.GetHwndPos(hwnd);
                                    var size = HwndInterface.GetHwndSize(hwnd);
                                    var factor = ScreenHelper.GetWindowsScreenScalingFactor(false);
                                    rectangle = new((int)(pos.X * factor), (int)(pos.Y * factor), size.Width, size.Height);
                                    BringToFront(marcoEvent.WindowName);
                                    Thread.Sleep(50);
                                }
                                if (marcoEvent.ImageSearchingArea.HasValue)
                                {
                                    if (marcoEvent.ImageSearchingArea?.X != 0) rectangle.X += Math.Min((int)resolution.Width, (int)marcoEvent.ImageSearchingArea?.X);
                                    if (marcoEvent.ImageSearchingArea?.Y != 0) rectangle.Y += Math.Min((int)resolution.Width, (int)marcoEvent.ImageSearchingArea?.Y);
                                    if (marcoEvent.ImageSearchingArea?.Width != 0) rectangle.Width = (int)marcoEvent.ImageSearchingArea?.Width;
                                    if (marcoEvent.ImageSearchingArea?.Height != 0) rectangle.Height = (int)marcoEvent.ImageSearchingArea?.Height;
                                    rectangle.Width = Math.Min((int)resolution.Width - rectangle.X, rectangle.Width);
                                    rectangle.Height = Math.Min((int)resolution.Height - rectangle.Y, rectangle.Height);
                                }

                                try
                                {
                                    var searchResult = ScreenHelper.FindImageInRectangle(rectangle, marcoEvent.ImageFilePath, marcoEvent.ImageMinSimilarity);
                                    if (!string.IsNullOrEmpty(marcoEvent.SaveToVariable)) runtimeDatabase[marcoEvent.SaveToVariable] = searchResult;
                                    if (marcoEvent.ShowInLogger) LogWriteLine($"Image found at  {searchResult.X} {searchResult.Y}");
                                }
                                catch (ImageNotFoundException ex)
                                {
                                    LogWriteLine(ex.Message);
                                    throw;
                                }
                                break;

                            default:
                                break;
                        }
                        #endregion

                        if (marcoEvent.SubEvents != null)
                        {
                            foreach (MarcoEvent subEvent in marcoEvent.SubEvents)
                            {
                                await subEvent.Excute(runtimeDatabase, handler);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }

                    Thread.Sleep(marcoEvent.DelayAfter);
                }
            }
            catch (Exception ex)
            {
                LogWriteLine(ex.Message);
            }
            finally
            {
                IsRunning = false;
                handler(this);
            }
        }

        public List<MarcoEvent> GetAllSubEvents(int layer = 0)
        {
            this.Layer = layer;
            List<MarcoEvent> result = [this];
            if (SubEvents != null)
            {
                foreach (MarcoEvent subEvent in SubEvents)
                {
                    result.AddRange(subEvent.GetAllSubEvents(layer + 1));
                }
            }
            return result;
        }

        public delegate void MarcoEventStatusChangedEventHandler(MarcoEvent sender);

        public string _ToString { get => ToString(); }
        public override string ToString()
        {
            string result = string.Empty;
            if (DelayBefore != DefaultDelayBefore)
            {
                for (int i = 0; i < Layer; i++) result += "\t";
                result += $"DelayBefore: {DelayBefore} ms \n";
            }
            for (int i = 0; i < Layer; i++) result += "\t";
            if (!string.IsNullOrWhiteSpace(Name)) result += $"[{Name}] ";
            result += $"{EventType}";
            if (Repeat != 1) result += $" - Repeat:{Repeat}";

            switch (EventType)
            {
                case MarcoEventType.EmptyEvent:
                    break;
                case MarcoEventType.MouseMoveEvent:
                    result += $" - X:{MouseMoveX} Y:{MouseMoveY}";
                    break;
                case MarcoEventType.MouseKeyEvent:
                    result += $" - {MouseKey} {KeyEvent}";
                    break;
                case MarcoEventType.KeyboardEvent:
                    result += $" - {KeyboardKey} {KeyEvent}";
                    break;
                case MarcoEventType.FocusWindow:
                    result += $" - {WindowName}";
                    break;
                case MarcoEventType.FindImage:
                    result += $" - {ImageFilePath}";
                    break;
                default:
                    break;
            }

            //if (!string.IsNullOrWhiteSpace(LoadFromVariable)) result += $" - RefKey:{LoadFromVariable}";
            //if (!string.IsNullOrWhiteSpace(SaveToVariable)) result += $" - ResultKey:{SaveToVariable}";

            if (DelayAfter != DefaultDelayAfter)
            {
                result += $"\n";
                for (int i = 0; i < Layer; i++) result += "\t";
                result += $"DelayAfter: {DelayAfter} ms";
            }
            return result;
        }
    }
}