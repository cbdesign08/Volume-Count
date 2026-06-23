#region Using declarations
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using NinjaTrader.Gui;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

namespace NinjaTrader.NinjaScript.Indicators.Cooz
{
    public class BarVolumeLabel : Indicator
    {
        // ── Configurable Properties ────────────────────────────────────────

        [NinjaScriptProperty]
        [Display(Name = "Tick Offset Below Low", GroupName = "Layout", Order = 0)]
        public int TickOffset { get; set; }

        [NinjaScriptProperty]
        [Display(Name = "Text Color", GroupName = "Text", Order = 0)]
        public Brush TextColor { get; set; }

        [NinjaScriptProperty]
        [Range(0, 100)]
        [Display(Name = "Text Opacity %", GroupName = "Text", Order = 1)]
        public int TextOpacity { get; set; }

        [NinjaScriptProperty]
        [Display(Name = "Background Color", GroupName = "Background", Order = 0)]
        public Brush BgColor { get; set; }

        [NinjaScriptProperty]
        [Range(0, 100)]
        [Display(Name = "Background Opacity %", GroupName = "Background", Order = 1)]
        public int BgOpacity { get; set; }

        // ──────────────────────────────────────────────────────────────────

        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Name               = "BarVolumeLabel";
                Description        = "Draws abbreviated volume below each candle.";
                Calculate          = Calculate.OnBarClose;
                IsOverlay          = true;
                DisplayInDataBox   = false;
                DrawOnPricePanel   = true;

                TickOffset  = 10;
                TextColor   = Brushes.White;
                TextOpacity = 100;
                BgColor     = Brushes.DimGray;
                BgOpacity   = 70;
            }
        }

        protected override void OnBarUpdate()
        {
            if (CurrentBar < 1) return;

            string tag   = "Vol_" + CurrentBar;
            double price = Low[0] - (TickOffset * TickSize);
            string label = FormatVolume(Volume[0]);

            // Build brushes with requested opacity
            Brush textBrush = ApplyOpacity(TextColor, TextOpacity);
            Brush bgBrush   = ApplyOpacity(BgColor,   BgOpacity);

            Draw.Text(
    this, tag, true, label, 0, price, 0,
    textBrush,
    new Gui.Tools.SimpleFont("Arial", 11),
    System.Windows.TextAlignment.Center,
    Brushes.Transparent,   // outline brush
    bgBrush,               // fill brush
    25                      // outline opacity
);
        }

        // ── Helpers ───────────────────────────────────────────────────────

        private string FormatVolume(double vol)
        {
            if (vol >= 1_000_000) return (vol / 1_000_000.0).ToString("0.#") + "M";
            if (vol >= 1_000)     return (vol / 1_000.0).ToString("0.#") + "K";
            return vol.ToString("0");
        }

        private Brush ApplyOpacity(Brush source, int opacityPct)
{
    Color c = ((SolidColorBrush)source).Color;
    byte alpha = (byte)(255 * opacityPct / 100.0);
    SolidColorBrush b = new SolidColorBrush(Color.FromArgb(alpha, c.R, c.G, c.B));
    b.Freeze();
    return b;
}
    }
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private Cooz.BarVolumeLabel[] cacheBarVolumeLabel;
		public Cooz.BarVolumeLabel BarVolumeLabel(int tickOffset, Brush textColor, int textOpacity, Brush bgColor, int bgOpacity)
		{
			return BarVolumeLabel(Input, tickOffset, textColor, textOpacity, bgColor, bgOpacity);
		}

		public Cooz.BarVolumeLabel BarVolumeLabel(ISeries<double> input, int tickOffset, Brush textColor, int textOpacity, Brush bgColor, int bgOpacity)
		{
			if (cacheBarVolumeLabel != null)
				for (int idx = 0; idx < cacheBarVolumeLabel.Length; idx++)
					if (cacheBarVolumeLabel[idx] != null && cacheBarVolumeLabel[idx].TickOffset == tickOffset && cacheBarVolumeLabel[idx].TextColor == textColor && cacheBarVolumeLabel[idx].TextOpacity == textOpacity && cacheBarVolumeLabel[idx].BgColor == bgColor && cacheBarVolumeLabel[idx].BgOpacity == bgOpacity && cacheBarVolumeLabel[idx].EqualsInput(input))
						return cacheBarVolumeLabel[idx];
			return CacheIndicator<Cooz.BarVolumeLabel>(new Cooz.BarVolumeLabel(){ TickOffset = tickOffset, TextColor = textColor, TextOpacity = textOpacity, BgColor = bgColor, BgOpacity = bgOpacity }, input, ref cacheBarVolumeLabel);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.Cooz.BarVolumeLabel BarVolumeLabel(int tickOffset, Brush textColor, int textOpacity, Brush bgColor, int bgOpacity)
		{
			return indicator.BarVolumeLabel(Input, tickOffset, textColor, textOpacity, bgColor, bgOpacity);
		}

		public Indicators.Cooz.BarVolumeLabel BarVolumeLabel(ISeries<double> input , int tickOffset, Brush textColor, int textOpacity, Brush bgColor, int bgOpacity)
		{
			return indicator.BarVolumeLabel(input, tickOffset, textColor, textOpacity, bgColor, bgOpacity);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.Cooz.BarVolumeLabel BarVolumeLabel(int tickOffset, Brush textColor, int textOpacity, Brush bgColor, int bgOpacity)
		{
			return indicator.BarVolumeLabel(Input, tickOffset, textColor, textOpacity, bgColor, bgOpacity);
		}

		public Indicators.Cooz.BarVolumeLabel BarVolumeLabel(ISeries<double> input , int tickOffset, Brush textColor, int textOpacity, Brush bgColor, int bgOpacity)
		{
			return indicator.BarVolumeLabel(input, tickOffset, textColor, textOpacity, bgColor, bgOpacity);
		}
	}
}

#endregion
