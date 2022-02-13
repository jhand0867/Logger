using System.Drawing;
using System.Windows.Forms;

namespace Logger
{
    class CustomProfessionalRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected)
            {
                using (Brush b = new SolidBrush(ProfessionalColors.ButtonCheckedHighlight))
                {
                    e.Graphics.FillRectangle(b, e.Item.ContentRectangle);
                    e.Item.Font = new Font("Arial", 10);
                    // e.Item.Font = new Font("Helvetica", 10); // FontStyle.Bold);
                    //e.Item.ForeColor = Color.White;
                }
            }
            else
            {
                using (Pen p = new Pen(ProfessionalColors.SeparatorLight))
                {
                    //e.Graphics.DrawRectangle(p, e.Item.ContentRectangle);
                }
            }
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            Rectangle r = Rectangle.Inflate(e.Item.ContentRectangle, -2, -2);

            if (e.Item.Selected)
            {
                using (Brush b = new SolidBrush(ProfessionalColors.ButtonCheckedHighlight))
                {
                    e.Graphics.FillRectangle(b, r);
                }
            }
            else
            {
                using (Pen p = new Pen(ProfessionalColors.SeparatorLight))
                {
                    e.Graphics.DrawRectangle(p, r);
                }
            }
        }
    }

    class RedTextRenderer : ToolStripRenderer
    {
        protected override void
        OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            base.OnRenderItemText(e);
            e.TextColor = Color.White;
            e.TextFont = new Font("Helvetica", 7, FontStyle.Bold);

        }

        protected override void OnRenderToolStripBackground(System.Windows.Forms.ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);
            e.ToolStrip.BackColor = Color.SteelBlue;


        }

        // OnRenderToolStripBorder(ToolStripRenderEventArgs)

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBorder(e);
            ControlPaint.DrawFocusRectangle(
                e.Graphics,
                e.AffectedBounds,
                Color.White,
                Color.White);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderMenuItemBackground(e);
            e.Item.BackColor = Color.Aqua;
        }

        protected override void OnRenderLabelBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            e.Item.BackColor = Color.LightGray;
            base.OnRenderLabelBackground(e);
        }

    }
}
