using System;
using System.Collections.Generic;
using System.Text;
using AutoClicker.Runtime.Core;
using AutoClicker.UI.Script;

namespace AutoClicker.UI.Script.Sample
{
    internal class TestScript2 : BaseScipt
    {
        public override string Name { get; set; } = nameof(TestScript2);
        public override List<MarcoEvent> MarcoEvents { get; } =
        [
            new()
            {
                Repeat = 5,
                SubEvents = [
                    new()
                    {
                        Name = "1",
                        ShowInLogger = true,
                    },
                    new()
                    {
                        Name = "2",
                        ShowInLogger = true,
                    },
                    new()
                    {
                        Name = "3",
                        ShowInLogger = true,
                    },
                    new()
                    {
                        Name = "4",
                        ShowInLogger = true,
                    },
                    new()
                    {
                        Name = "5",
                        ShowInLogger = true,
                    },
                    new()
                    {
                        Name = "6",
                        ShowInLogger = true,
                    },
                    new()
                    {
                        Name = "7",
                        ShowInLogger = true,
                    },
                    new()
                    {
                        Name = "8",
                        ShowInLogger = true,
                    },
                    new()
                    {
                        Name = "9",
                        ShowInLogger = true,
                    },
                    new()
                    {
                        Name = "10",
                        ShowInLogger = true,
                    },
                ],
            },
        ];

    }
}
