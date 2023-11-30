using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;
using GraftPack;



namespace GraftPack
{
    public class Shape
    {
        // This is the base class for Shapes in the application. It should allow an array or LL
        // to be created containing different kinds of shapes.
        string shapeType;
        Color color;
        Point one, two;
        Graphics g;
        public Shape(Color color,string shapeType, Point point1, Point point2,Graphics g)   // constructor
        {
            this.one = point1;
            this.two = point2;
            this.shapeType = shapeType;
            this.color = color;
            this.g = g;
        }

        

        public void setGraphics(Graphics g)
        {
            this.g = g;
        }


        public Graphics getGraphics()
        {
            return this.g;
        }

        public string getShapeType()
        {
            return shapeType;
        }
        public Point getOne()
        {
            return one;
        }
        public Point getTwo()
        {
            return two;
        }

        public void setOne(Point one)
        {
            this.one = one;
        }
        public void setTwo(Point two)
        {
            this.two = two;
        }


        public Color getColor()
        {
            return color;
        }

        public void setColor(Color color)
        {
            this.color = color;
        }
       
        public void drawRectangle()
        {
         
            // This method draws the rectangle by moving the X and Y of the oposite corners
            Pen pen = new Pen(color);
            g.DrawLine(pen, one.X, one.Y, two.X, one.Y);
            g.DrawLine(pen, two.X, two.Y, two.X, one.Y);
            g.DrawLine(pen, two.X, two.Y, one.X, two.Y);
            g.DrawLine(pen, one.X, one.Y, one.X, two.Y);

        }
        public void drawSquare()
        {
            Pen pen = new Pen(color);
            // This method draws the square by calculating the positions of the other 2 corners
            double xDiff, yDiff, xMid, yMid;   // range and mid points of x & y  

            // calculate ranges and mid points
            xDiff = two.X - one.X;
            yDiff = two.Y - one.Y;
            xMid = (two.X + one.X) / 2;
            yMid = (two.Y + one.Y) / 2;

            // draw square
            g.DrawLine(pen, one.X, (int)one.Y, (int)(xMid + yDiff / 2), (int)(yMid - xDiff / 2));
            g.DrawLine(pen, (int)(xMid + yDiff / 2), (int)(yMid - xDiff / 2), (int)two.X, (int)two.Y);
            g.DrawLine(pen, (int)two.X, (int)two.Y, (int)(xMid - yDiff / 2), (int)(yMid + xDiff / 2));
            g.DrawLine(pen, (int)(xMid - yDiff / 2), (int)(yMid + xDiff / 2), (int)one.X, (int)one.Y);
        }


        public void drawCircle()
        {
            Pen pen = new Pen(color);
            var radius = Math.Sqrt(Math.Pow((one.X - two.X), 2) + Math.Pow((one.Y - two.Y), 2));

            var angle = 2 * Math.PI / 360;
            for (int i = 0; i < 360; i++)
            {
                Point P1 = new Point((int)(radius * Math.Sin(i * angle) + one.X), (int)(radius * Math.Cos(i * angle) + one.Y));
                Point P2 = new Point((int)(radius * Math.Sin((i + 1) * angle) + one.X), (int)(radius * Math.Cos((i + 1) * angle) + one.Y));
                g.DrawLine(pen, P1, P2);
            }

        }

        public void drawTriangle()
        {
            Pen pen = new Pen(color);

            float DifferenceX = one.X - two.X;
            float DifferenceY = one.Y - two.Y;
            float MidpointX = (one.X + two.X) / 2;
            float MidpointY = (one.Y + two.Y) / 2;


            // draw triangle
            g.DrawLine(pen,  two.X, two.Y, (MidpointX + DifferenceY / 2), (MidpointY - DifferenceX / 2));
            g.DrawLine(pen, (MidpointX + DifferenceY / 2), (MidpointY - DifferenceX / 2), one.X, one.Y);
            g.DrawLine(pen,  two.X, two.Y, one.X, one.Y);

        }

        

        


    }





       
    
}








