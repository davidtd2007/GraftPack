using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;
using GraftPack;

namespace GraftPack
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GrafPack());
        }
    }

}
public partial class GrafPack : Form
{


    private bool selectSquareStatus = false;
    private bool selectTriangleStatus = false;
    private bool selectRectangleStatus = false;
    private bool moveBool = false;
    private bool scaleBool = false;
    private bool selectCircleStatus = false;


    Shape newShape;
    private int selectedItem = -1;
    private Color selectedColor = Color.Red;
    private Color previousSelectedColor;
    List<Shape> ShapeList = new List<Shape>();
    private Point one;
    private Point two;

    static Color color = Color.Black;
    Pen pen = new Pen(color);


    //Controls of the rotate shape form
    int angle_To_Rotate = 0;
    Form rotator = new Form();
    Label title = new Label();
    Label angle_label = new Label();
    Button rotate_Buttom = new Button();
    TextBox angle = new TextBox();
    CheckBox clockwise = new CheckBox();
    CheckBox anticlockwise = new CheckBox();







    public GrafPack()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        this.SetStyle(ControlStyles.ResizeRedraw, true);
        this.WindowState = FormWindowState.Maximized;
        this.BackColor = Color.White;
        draw();

        // The following approach uses menu items coupled with mouse clicks
        MainMenu mainMenu = new MainMenu();
        MenuItem createItem = new MenuItem();
        MenuItem selectItem = new MenuItem();
        MenuItem squareItem = new MenuItem();
        MenuItem triangleItem = new MenuItem();
        MenuItem quadrangleItem = new MenuItem();
        MenuItem rectangleItem = new MenuItem();
        MenuItem circleItem = new MenuItem();
        MenuItem color = new MenuItem();
        MenuItem Blue = new MenuItem();
        MenuItem Green = new MenuItem();
        MenuItem Yellow = new MenuItem();
        MenuItem Purple = new MenuItem();
        MenuItem Black = new MenuItem();
        MenuItem pink = new MenuItem();
        MenuItem next = new MenuItem();
        MenuItem previous = new MenuItem();
        MenuItem deselect = new MenuItem();
        MenuItem edit = new MenuItem();
        MenuItem delete = new MenuItem();
        MenuItem move = new MenuItem();
        MenuItem rotate = new MenuItem();
        MenuItem scale = new MenuItem();
        MenuItem mirrorX = new MenuItem();
        MenuItem mirrorY = new MenuItem();
        MenuItem Exit = new MenuItem();



        Black.Text = "&Black";
        Blue.Text = "&Blue";
        Green.Text = "&Green";
        Yellow.Text = "&Yellow";
        Purple.Text = "&Purple";
        pink.Text = "&Pink";
        color.Text = "&Color";
        createItem.Text = "&Create";
        squareItem.Text = "&Square";
        triangleItem.Text = "&Triangle";
        circleItem.Text = "&Circle";
        selectItem.Text = "&Select";
        quadrangleItem.Text = "&Quadrangle";
        rectangleItem.Text = "&Rectangle";
        next.Text = "&Next Shape";
        previous.Text = "&Previous Shape";
        deselect.Text = "&Deselect Shape";
        edit.Text = "&Edit Shape";
        delete.Text = "&Delete Shape";
        move.Text = "&Move Shape";
        rotate.Text = "&Rotate Shape";
        scale.Text = "&Scale Shape";
        mirrorX.Text = "&Mirror Shape by the X axis";
        mirrorY.Text = "&Mirror Shape by the Y axis";
        Exit.Text = "&Exit Program";




        mainMenu.MenuItems.Add(createItem);
        mainMenu.MenuItems.Add(edit);
        mainMenu.MenuItems.Add(selectItem);
        mainMenu.MenuItems.Add(color);
        mainMenu.MenuItems.Add(Exit);
        color.MenuItems.Add(Black);
        color.MenuItems.Add(Blue);
        color.MenuItems.Add(Yellow);
        color.MenuItems.Add(Purple);
        color.MenuItems.Add(pink);
        color.MenuItems.Add(Green);
        createItem.MenuItems.Add(quadrangleItem);
        createItem.MenuItems.Add(triangleItem);
        createItem.MenuItems.Add(circleItem);
        quadrangleItem.MenuItems.Add(squareItem);
        quadrangleItem.MenuItems.Add(rectangleItem);
        selectItem.MenuItems.Add(next);
        selectItem.MenuItems.Add(previous);
        selectItem.MenuItems.Add(deselect);
        edit.MenuItems.Add(delete);
        edit.MenuItems.Add(move);
        edit.MenuItems.Add(rotate);
        edit.MenuItems.Add(mirrorX);
        edit.MenuItems.Add(mirrorY);
        edit.MenuItems.Add(scale);



        squareItem.Click += new System.EventHandler(this.selectSquare);
        triangleItem.Click += new System.EventHandler(this.selectTriangle);
        rectangleItem.Click += new System.EventHandler(this.selectRectangle);
        circleItem.Click += new System.EventHandler(this.selectCircle);
        Blue.Click += new System.EventHandler(this.selectBlue);
        Green.Click += new System.EventHandler(this.selectGreen);
        Black.Click += new System.EventHandler(this.selectBlack);
        Yellow.Click += new System.EventHandler(this.selectYellow);
        Purple.Click += new System.EventHandler(this.selectPurple);
        pink.Click += new System.EventHandler(this.selectPink);
        next.Click += new System.EventHandler(this.nextSelection);
        previous.Click += new System.EventHandler(this.previousSelection);
        deselect.Click += new System.EventHandler(this.deselectSelection);
        delete.Click += new System.EventHandler(this.selectDelete);
        move.Click += new System.EventHandler(this.moveShape);
        rotate.Click += new System.EventHandler(this.rotateShape);
        scale.Click += new System.EventHandler(this.scaleShape);
        mirrorX.Click += new System.EventHandler(this.mirror_ShapeX);
        mirrorY.Click += new System.EventHandler(this.mirror_ShapeY);
        rotator.FormClosed += new FormClosedEventHandler(rotator_FromClosed);
        Exit.Click += new System.EventHandler(this.exit);

        this.Menu = mainMenu;
        this.MouseDown += GrafPack_MouseDown;
        this.MouseMove += GrafPack_MouseMove;
        this.MouseUp += GrafPack_MouseUp;
    }

    private void exit(object sender, EventArgs e)
    {
        this.Close();
    }

    private void mirror_ShapeY(object sender, EventArgs e)
    {
        if (selectedItem != -1)
        {
            Point mirrorPointOne = ShapeList[selectedItem].getOne();
            Point mirrorPointTwo = ShapeList[selectedItem].getTwo();

            if (mirrorPointOne.Y > mirrorPointTwo.Y)
            {
                int yDiff = mirrorPointOne.Y - mirrorPointTwo.Y;

                mirrorPointOne.Y = mirrorPointTwo.Y - yDiff;
                string shapeType = ShapeList[this.selectedItem].getShapeType();
                switch (shapeType)
                {
                    case "Square":
                        ShapeList[selectedItem] = new Shape(ShapeList[selectedItem].getColor(), "Square", mirrorPointOne, mirrorPointTwo, ShapeList[selectedItem].getGraphics());
                        break;
                    case "FreeTriangle":
                        ShapeList[selectedItem] = new Shape(ShapeList[selectedItem].getColor(), "FreeTriangle", mirrorPointTwo, mirrorPointOne, ShapeList[selectedItem].getGraphics());

                        break;
                    case "Rectangle":
                        ShapeList[selectedItem] = new Shape(ShapeList[selectedItem].getColor(), "Rectangle", mirrorPointOne, mirrorPointTwo, ShapeList[selectedItem].getGraphics());

                        break;
                    case "Circle":
                        ShapeList[selectedItem] = new Shape(ShapeList[selectedItem].getColor(), "Circle", mirrorPointOne, mirrorPointTwo, ShapeList[selectedItem].getGraphics());

                        break;
                }
                Refresh();
                draw();
            }
            else
            {
                int yDiff = mirrorPointTwo.Y - mirrorPointOne.Y;

                mirrorPointOne.Y = mirrorPointTwo.Y + yDiff;
                string shapeType = ShapeList[this.selectedItem].getShapeType();
                switch (shapeType)
                {
                    case "Square":
                        ShapeList[selectedItem] = new Shape(ShapeList[selectedItem].getColor(), "Square", mirrorPointOne, mirrorPointTwo, ShapeList[selectedItem].getGraphics());
                        break;
                    case "FreeTriangle":
                        ShapeList[selectedItem] = new Shape(ShapeList[selectedItem].getColor(), "FreeTriangle", mirrorPointTwo, mirrorPointOne, ShapeList[selectedItem].getGraphics());

                        break;
                    case "Rectangle":
                        ShapeList[selectedItem] = new Shape(ShapeList[selectedItem].getColor(), "Rectangle", mirrorPointOne, mirrorPointTwo, ShapeList[selectedItem].getGraphics());

                        break;
                    case "Circle":
                        ShapeList[selectedItem] = new Shape(ShapeList[selectedItem].getColor(), "Circle", mirrorPointOne, mirrorPointTwo, ShapeList[selectedItem].getGraphics());

                        break;
                }
                Refresh();
                draw();
            }
        }
        else
        {
            MessageBox.Show("There is nothing selected");
        }
    }

    private void mirror_ShapeX(object sender, EventArgs e)
    {
        if (selectedItem != -1)
        {
            Point mirrorPointOne = ShapeList[selectedItem].getOne();
            Point mirrorPointTwo = ShapeList[selectedItem].getTwo();

            if (mirrorPointOne.X > mirrorPointTwo.X)
            {
                int xDiff = mirrorPointOne.X - mirrorPointTwo.X;

                mirrorPointOne.X = mirrorPointTwo.X - xDiff;
                string shapeType = ShapeList[this.selectedItem].getShapeType();
                switch (shapeType)
                {
                    case "Square":
                        ShapeList[selectedItem] = new Shape(ShapeList[selectedItem].getColor(), "Square", mirrorPointOne, mirrorPointTwo, ShapeList[selectedItem].getGraphics());
                        break;
                    case "FreeTriangle":
                        ShapeList[selectedItem] = new Shape(ShapeList[selectedItem].getColor(), "FreeTriangle", mirrorPointTwo, mirrorPointOne, ShapeList[selectedItem].getGraphics());

                        break;
                    case "Rectangle":
                        ShapeList[selectedItem] = new Shape(ShapeList[selectedItem].getColor(), "Rectangle", mirrorPointOne, mirrorPointTwo, ShapeList[selectedItem].getGraphics());

                        break;
                    case "Circle":
                        ShapeList[selectedItem] = new Shape(ShapeList[selectedItem].getColor(), "Circle", mirrorPointOne, mirrorPointTwo, ShapeList[selectedItem].getGraphics());

                        break;
                }
                Refresh();
                draw();
            }
            else
            {
                int xDiff = mirrorPointTwo.X - mirrorPointOne.X;

                mirrorPointOne.X = mirrorPointTwo.X + xDiff;
                string shapeType = ShapeList[this.selectedItem].getShapeType();
                switch (shapeType)
                {
                    case "Square":
                        ShapeList[selectedItem] = new Shape(ShapeList[selectedItem].getColor(), "Square", mirrorPointOne, mirrorPointTwo, ShapeList[selectedItem].getGraphics());
                        break;
                    case "FreeTriangle":
                        ShapeList[selectedItem] = new Shape(ShapeList[selectedItem].getColor(), "FreeTriangle", mirrorPointTwo, mirrorPointOne, ShapeList[selectedItem].getGraphics());

                        break;
                    case "Rectangle":
                        ShapeList[selectedItem] = new Shape(ShapeList[selectedItem].getColor(), "Rectangle", mirrorPointOne, mirrorPointTwo, ShapeList[selectedItem].getGraphics());

                        break;
                    case "Circle":
                        ShapeList[selectedItem] = new Shape(ShapeList[selectedItem].getColor(), "Circle", mirrorPointOne, mirrorPointTwo, ShapeList[selectedItem].getGraphics());

                        break;
                }
                Refresh();
                draw();
            }
        }
        else
        {
            MessageBox.Show("There is nothing selected");
        }
    }

    private void scaleShape(object sender, EventArgs e)
    {
        if (selectedItem != -1)
        {
            scaleBool = true;
            selectSquareStatus = false;
            selectTriangleStatus = false;
            selectRectangleStatus = false;
            moveBool = false;
            selectCircleStatus = false;
        }
        else
        {
            MessageBox.Show("There is nothing selected");
        }
    }

    private void rotateShape(object sender, EventArgs e)
    {
        if (selectedItem != -1)
        {
            rotator.Size = new Size(300, 300);
            rotator.FormBorderStyle = FormBorderStyle.FixedDialog;


            clockwise.Text = "Clockwise Rotation";
            clockwise.Checked = true;
            clockwise.Enabled = false;
            clockwise.Location = new Point(100, 100);
            clockwise.CheckedChanged += new EventHandler(clockwise_CheckedChanged);



            anticlockwise.Text = "Anticlockwise Rotation";
            anticlockwise.Checked = false;
            anticlockwise.Location = new Point(100, 125);
            anticlockwise.CheckedChanged += new EventHandler(anticlockwise_CheckedChanged);
            //Title of the form properties

            Font title_font = new Font("Arial", 15);
            title.Size = new Size(300, 50);
            title.Text = "Rotate selected Shape";
            title.Location = new Point(50, 10);
            title.Font = title_font;


            angle.Location = new Point(100, 175);
            angle_label.Text = "Angle:";
            angle_label.Location = new Point(55, 175);


            angle.KeyPress += new KeyPressEventHandler(angle_Key_Press);



            rotate_Buttom.Location = new Point(100, 225);
            rotate_Buttom.Text = "Rotate";
            rotate_Buttom.Click += new EventHandler(rotate_Buttom_Click);

            rotator.Controls.Add(rotate_Buttom);
            rotator.Controls.Add(title);
            rotator.Controls.Add(angle);
            rotator.Controls.Add(clockwise);
            rotator.Controls.Add(anticlockwise);
            rotator.Controls.Add(angle_label);


            rotator.ShowDialog();



        }
        else
        {
            MessageBox.Show("There is nothing selected");
        }


    }

    private void rotator_FromClosed(object sender, FormClosedEventArgs e)
    {
     
        Graphics g = this.CreateGraphics();
        Point origin = new Point();
        origin.X = (ShapeList[selectedItem].getOne().X + ShapeList[selectedItem].getTwo().X) / 2;
        origin.Y = (ShapeList[selectedItem].getOne().Y + ShapeList[selectedItem].getTwo().Y) / 2;
        g.TranslateTransform(origin.X, origin.Y);
        g.RotateTransform(angle_To_Rotate);
        g.TranslateTransform(-origin.X, -origin.Y);
        ShapeList[selectedItem].setGraphics(g);

        Refresh();
        draw();
    }

    private void anticlockwise_CheckedChanged(object sender, EventArgs e)
    {
        if (anticlockwise.Checked)
        {
            clockwise.Checked = false;
            anticlockwise.Enabled = false;
            clockwise.Enabled = true;
        }

    }

    private void clockwise_CheckedChanged(object sender, EventArgs e)
    {
        //Force a check to always be checked
        if (clockwise.Checked)
        {
            anticlockwise.Checked = false;
            clockwise.Enabled = false;
            anticlockwise.Enabled = true;
        }



    }

    private void angle_Key_Press(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }

    }


    private void rotate_Buttom_Click(object sender, EventArgs e)
    {
        if (angle.Text != "")
        {

            if (Convert.ToInt32(angle.Text) > 360)
            {

                angle.Text = "360";
            }
            if (Convert.ToInt32(angle.Text) < 0)
            {

                angle.Text = "0";
            }

            if (anticlockwise.Checked)
            {

                angle_To_Rotate = -Convert.ToInt32(angle.Text);
            }
            else
            {
                angle_To_Rotate = Convert.ToInt32(angle.Text);
            }

            rotator.Close();
        }
        else
        {
            MessageBox.Show("There is no angle written");
        }
    }

    private void moveShape(object sender, EventArgs e)
    {
        if (selectedItem != -1)
        {
            moveBool = true;

            selectSquareStatus = false;
            selectTriangleStatus = false;
            selectRectangleStatus = false;
            scaleBool = false;
            selectCircleStatus = false;
        }
        else
        {
            MessageBox.Show("There is nothing selected");
        }

    }

    private void GrafPack_MouseUp(object sender, MouseEventArgs e)
    {
        Graphics g = this.CreateGraphics();
        if (selectRectangleStatus == true)
        {
            Shape aShape = new Shape(color, "Rectangle", one, two, g);
            ShapeList.Add(aShape);
            draw();
            selectRectangleStatus = false;
        }

        if (selectSquareStatus == true)
        {
            Shape aShape = new Shape(color, "Square", one, two, g);
            ShapeList.Add(aShape);
            draw();
            selectSquareStatus = false;
        }
        if (selectTriangleStatus == true)
        {
            Shape aShape = new Shape(color, "FreeTriangle", one, two, g);
            ShapeList.Add(aShape);
            draw();
            selectTriangleStatus = false;
        }

        if (selectCircleStatus)
        {
            Shape aShape = new Shape(color, "Circle", one, two, g);
            ShapeList.Add(aShape);
            draw();
            selectCircleStatus = false;
        }

        if (moveBool)
        {
            ShapeList[selectedItem] = newShape;
            moveBool = false;
            this.Refresh();
            draw();
        }

        if (scaleBool)
        {
            ShapeList[selectedItem] = newShape;
            scaleBool = false;
            this.Refresh();
            draw();
        }





    }

    private void GrafPack_MouseMove(object sender, MouseEventArgs e)
    {
        //https://stackoverflow.com/questions/32019439/c-sharp-how-to-draw-a-rubber-band-selection-rectangle-on-panel-like-one-used-in
        if (e.Button == System.Windows.Forms.MouseButtons.Left)
        {

            Graphics g = this.CreateGraphics();

            one = e.Location;
            if (selectRectangleStatus)
            {
                this.Refresh();

                new Shape(pen.Color, "", one, two, g).drawRectangle();
                draw();

            }

            if (selectSquareStatus)
            {
                this.Refresh();
                // draw square
                new Shape(pen.Color, "", one, two, g).drawSquare();
                draw();

            }

            if (selectTriangleStatus)
            {
                this.Refresh();
                new Shape(pen.Color, "", one, two, g).drawTriangle();
                draw();


            }

            if (selectCircleStatus)
            {
                this.Refresh();
                new Shape(pen.Color, "", one, two, g).drawCircle();
                draw();
            }

            if (moveBool)
            {
                if (selectedItem != -1)
                {
                    int xDiff = two.X - one.X;
                    int yDiff = two.Y - one.Y;
                    Point newOne = new Point(ShapeList[selectedItem].getOne().X - xDiff, ShapeList[selectedItem].getOne().Y - yDiff);
                    Point newTwo = new Point(ShapeList[selectedItem].getTwo().X - xDiff, ShapeList[selectedItem].getTwo().Y - yDiff);
                    this.Refresh();
                    string shapeType = ShapeList[this.selectedItem].getShapeType();
                    switch (shapeType)
                    {
                        case "Square":
                            newShape = new Shape(ShapeList[selectedItem].getColor(), "Square", newOne, newTwo, ShapeList[selectedItem].getGraphics());
                            newShape.drawSquare();
                            break;
                        case "FreeTriangle":
                            newShape = new Shape(ShapeList[selectedItem].getColor(), "FreeTriangle", newOne, newTwo, ShapeList[selectedItem].getGraphics());
                            newShape.drawTriangle();
                            break;
                        case "Rectangle":
                            newShape = new Shape(ShapeList[selectedItem].getColor(), "Rectangle", newOne, newTwo, ShapeList[selectedItem].getGraphics());
                            newShape.drawRectangle();
                            break;
                        case "Circle":
                            newShape = new Shape(ShapeList[selectedItem].getColor(), "Circle", newOne, newTwo, ShapeList[selectedItem].getGraphics());
                            newShape.drawCircle();
                            break;
                    }

                    draw();
                }

            }

            if (scaleBool)
            {
                if (selectedItem != -1)
                {
                    Point shapeTwo = ShapeList[selectedItem].getTwo();

                    this.Refresh();
                    string shapeType = ShapeList[this.selectedItem].getShapeType();
                    switch (shapeType)
                    {
                        case "Square":
                            newShape = new Shape(ShapeList[selectedItem].getColor(), "Square", one, shapeTwo, ShapeList[selectedItem].getGraphics());
                            newShape.drawSquare();
                            break;
                        case "FreeTriangle":
                            newShape = new Shape(ShapeList[selectedItem].getColor(), "FreeTriangle", one, shapeTwo, ShapeList[selectedItem].getGraphics());
                            newShape.drawTriangle();
                            break;
                        case "Rectangle":
                            newShape = new Shape(ShapeList[selectedItem].getColor(), "Rectangle", one, shapeTwo, ShapeList[selectedItem].getGraphics());
                            newShape.drawRectangle();
                            break;
                        case "Circle":
                            newShape = new Shape(ShapeList[selectedItem].getColor(), "Circle", one, shapeTwo, ShapeList[selectedItem].getGraphics());
                            newShape.drawCircle();
                            break;
                    }

                    draw();
                }

            }
        }



    }


    private void GrafPack_MouseDown(object sender, MouseEventArgs e)
    {
        two = e.Location;
    }

    private void selectDelete(object sender, EventArgs e)
    {
        if (selectedItem != -1)
        {
            previousSelectedColor = Color.Empty;
            ShapeList.RemoveAt(selectedItem);
            selectedItem = -1;
            this.Refresh();
            draw();
        }
        else
        {
            MessageBox.Show("There is nothing selected");
        }


    }

    private void deselectSelection(object sender, EventArgs e)
    {
        ShapeList[selectedItem].setColor(previousSelectedColor);
        selectedItem = -1;
        draw();
    }

    private void previousSelection(object sender, EventArgs e)
    {
        if (selectedItem == -1)
        {
            selectedItem = 0;
            previousSelectedColor = ShapeList[selectedItem].getColor();
            ShapeList[selectedItem].setColor(selectedColor);
            draw();
        }
        else
        {
            if (selectedItem == 0)
            {
                MessageBox.Show("You are already in the first element of the draw");
            }
            else
            {
                ShapeList[selectedItem].setColor(previousSelectedColor);
                selectedItem--;
                previousSelectedColor = ShapeList[selectedItem].getColor();
                ShapeList[selectedItem].setColor(selectedColor);
                draw();

            }
        }


    }

    private void nextSelection(object sender, EventArgs e)
    {
        if (selectedItem == -1)
        {
            selectedItem = 0;
            previousSelectedColor = ShapeList[selectedItem].getColor();
            ShapeList[selectedItem].setColor(selectedColor);
            this.draw();
        }
        else
        {
            if (selectedItem == ShapeList.Count - 1)
            {
                MessageBox.Show("You are already in the last element of the draw");
            }
            else
            {
                ShapeList[selectedItem].setColor(previousSelectedColor);
                selectedItem++;
                previousSelectedColor = ShapeList[selectedItem].getColor();
                ShapeList[selectedItem].setColor(selectedColor);
                draw();
            }
        }
    }



    private void draw()
    {

        for (int i = 0; i < ShapeList.Count; i++)
        {

            string shapeType = ShapeList[i].getShapeType();
            if (i == selectedItem && moveBool)
            {
                continue;
            }

            switch (shapeType)
            {
                case "Square":
                    new Shape(ShapeList[i].getColor(), "", ShapeList[i].getOne(), ShapeList[i].getTwo(), ShapeList[i].getGraphics()).drawSquare();
                    break;
                case "FreeTriangle":
                    new Shape(ShapeList[i].getColor(), "", ShapeList[i].getOne(), ShapeList[i].getTwo(), ShapeList[i].getGraphics()).drawTriangle();
                    break;
                case "Rectangle":
                    new Shape(ShapeList[i].getColor(), "", ShapeList[i].getOne(), ShapeList[i].getTwo(), ShapeList[i].getGraphics()).drawRectangle();
                    break;
                case "Circle":
                    new Shape(ShapeList[i].getColor(), "", ShapeList[i].getOne(), ShapeList[i].getTwo(), ShapeList[i].getGraphics()).drawCircle();
                    break;
            }


        }

    }
    // Generally, all methods of the form are usually private
    private void selectSquare(object sender, EventArgs e)
    {

        selectSquareStatus = true;
        MessageBox.Show("Click and drag to create a square");
        selectTriangleStatus = false;
        selectRectangleStatus = false;
        moveBool = false;
        scaleBool = false;
        selectCircleStatus = false;


    }
    private void selectRectangle(object sender, EventArgs e)
    {

        selectRectangleStatus = true;
        MessageBox.Show("Click and drag to create a rectangle");
        selectSquareStatus = false;
        selectTriangleStatus = false;
        moveBool = false;
        scaleBool = false;
        selectCircleStatus = false;

    }

    private void selectTriangle(object sender, EventArgs e)
    {

        selectTriangleStatus = true;
        MessageBox.Show("Click and drag to create a triangle");
        selectSquareStatus = false;
        selectRectangleStatus = false;
        moveBool = false;
        scaleBool = false;
        selectCircleStatus = false;

    }

    private void selectCircle(object sender, EventArgs e)
    {

        selectCircleStatus = true;
        MessageBox.Show("Click and drag to create a circle");
        selectSquareStatus = false;
        selectTriangleStatus = false;
        selectRectangleStatus = false;
        moveBool = false;
        scaleBool = false;

    }

    private void selectPink(object sender, EventArgs e)
    {
        MessageBox.Show("You selected the color pink");
        color = Color.Pink;
        pen.Color = color;
    }
    private void selectBlue(object sender, EventArgs e)
    {

        MessageBox.Show("You selected the color blue");
        color = Color.Blue;
        pen.Color = color;
    }
    private void selectGreen(object sender, EventArgs e)
    {

        MessageBox.Show("You selected the color green");
        color = Color.Green;
        pen.Color = color;
    }
    private void selectYellow(object sender, EventArgs e)
    {

        MessageBox.Show("You selected the color yellow");
        color = Color.Yellow;
        pen.Color = color;
    }
    private void selectBlack(object sender, EventArgs e)
    {

        MessageBox.Show("You selected the color black");
        color = Color.Black;
        pen.Color = color;
    }

    private void selectPurple(object sender, EventArgs e)
    {
        MessageBox.Show("You selected the color purple");
        color = Color.Purple;
        pen.Color = color;
    }


}



