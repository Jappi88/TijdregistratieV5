using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf.parser;

namespace ProductieManager.Rpm.Various
{
    public class RectAndText : IComparable
    {
        public iTextSharp.text.Rectangle Rect;
        public String Text;
        public RectAndText(iTextSharp.text.Rectangle rect, String text)
        {
            this.Rect = rect;
            this.Text = text;
        }

        public int CompareTo(object obj)
        {
            if (obj is RectAndText comp)
            {
                var a = (int)Math.Round(Math.Floor(this.Rect.Left), 0, MidpointRounding.ToEven);
                var b = (int)Math.Round(Math.Floor(comp.Rect.Left), 0, MidpointRounding.ToEven);
                if (Enumerable.Range(a -1, 2).Contains(b))
                {
                    a = (int)Math.Round(Math.Floor(this.Rect.Bottom), 0, MidpointRounding.ToEven);
                    b = (int)Math.Round(Math.Floor(comp.Rect.Bottom), 0, MidpointRounding.ToEven);
                    if (Enumerable.Range(a -1, 2).Contains(b))
                    {
                        return 0;
                    }

                    if (b > a) return 1;
                    return -1;
                }
                if (b > a) return 1;
                return -1;
            }
            else
            {
                throw new ArgumentException("Object is not a MyObject ");
            }
        }
    }

    public class MyLocationTextExtractionStrategy : LocationTextExtractionStrategy
    {
        public MyLocationTextExtractionStrategy()
        {
        }
        //Hold each coordinate
        public List<RectAndText> myPoints = new List<RectAndText>();

        //Automatically called for each chunk of text in the PDF
        public override void RenderText(TextRenderInfo renderInfo)
        {
            base.RenderText(renderInfo);

            //Get the bounding box for the chunk of text
            var bottomLeft = renderInfo.GetDescentLine().GetStartPoint();
            var topRight = renderInfo.GetAscentLine().GetEndPoint();

            //Create a rectangle from it
            var rect = new iTextSharp.text.Rectangle(
                bottomLeft[Vector.I1],
                bottomLeft[Vector.I2],
                topRight[Vector.I1],
                topRight[Vector.I2]
            );
            //Add this to our main collection
            this.myPoints.Add(new RectAndText(rect, renderInfo.GetText()));
        }
    }
}
