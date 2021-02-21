using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickAndMove
{
    public partial class Form1 : Form
    {
        Rectangle[] items = new Rectangle[5];//number of items
        Rectangle[] slots = new Rectangle[10];//number of slots
        Point cursorLocation;//get cursor loaction
        bool[] carryItem;//check if item being carried 
        bool[] slotFilled;//check if slot has item

        public Form1()
        {
            InitializeComponent();

            carryItem = new bool[items.Length];//create parrallel arrey
            slotFilled = new bool[slots.Length];//create parrallel arrey

            int count = 100;//space between slots and items

            //create items
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new Rectangle(count, 100, 100, 100);

                count = count + 150;
            }

            count = 100;//reset count for slots 

            //create slots
            for (int i = 0; i < slots.Length; i++)
            {
                if (i == 5)//reset count for new row 
                {
                    count = 100;
                }

                if (i < 5)//goes from slots 1 to 5
                {
                    slots[i] = new Rectangle(count, 100, 100, 100);
                    count = count + 150;
                }
                else//start new row then creates slots 6 to 10
                {
                    slots[i] = new Rectangle(count, 250, 100, 100);
                    count = count + 150;
                }                            
            }
        }
       
        private void tmrMovement_Tick(object sender, EventArgs e)
        {
            //make sure the item follows 
            for (int i = 0; i < carryItem.Length; i++)//goes through each item
            {
                if (carryItem[i] == true)//if carrying item...
                {
                    //have item follow cursor
                    items[i].X = cursorLocation.X - (items[i].Width / 2);
                    items[i].Y = cursorLocation.Y - (items[i].Height / 2);
                }
            }

            //check if slot is filled
            for (int i = 0; i < slots.Length; i++)//goes through each slot
            {
                slotFilled[i] = false;//set all slots to false first
                for (int j = 0; j < items.Length; j++)//goes through each item
                {
                    if (items[j].IntersectsWith(slots[i]))//if item is touching slot...
                    {
                        slotFilled[i] = true;//slot becomes filled 
                    }                    
                }               
            }

            //sends filled results to label 
            lblText.Text = slotFilled[0].ToString() + " " + slotFilled[1].ToString() + " " + slotFilled[2].ToString() + " " + slotFilled[3].ToString() + " " + slotFilled[4].ToString() + "\n" + slotFilled[5].ToString() + " " + slotFilled[6].ToString() + " " + slotFilled[7].ToString() + " " + slotFilled[8].ToString() + " " + slotFilled[9].ToString();

            Refresh();//refresh 
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < items.Length; i++)//goes through each item 
            {
                //if cursor over item and not carrying it...
                if ((items[i].Left <= cursorLocation.X && cursorLocation.X <= items[i].Right) && (items[i].Top <= cursorLocation.Y && cursorLocation.Y <= items[i].Bottom) && carryItem[i] == false)
                {
                    carryItem[i] = true;//picks up item
                }
                else//otherwise...
                {
                    for (int j = 0; j < slots.Length; j++)//goes through each slot 
                    {
                        //if cursor over slot and carrying item
                        if ((slots[j].Left <= cursorLocation.X && cursorLocation.X <= slots[j].Right) && (slots[j].Top <= cursorLocation.Y && cursorLocation.Y <= slots[j].Bottom) && carryItem[i] == true)
                        {
                            //drops item 
                            carryItem[i] = false;
                            items[i].X = slots[j].X;
                            items[i].Y = slots[j].Y;
                        }                       
                    }
                }           
            }          
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            cursorLocation = e.Location;//have cursorLocation be mouse position 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            for (int i = 0; i < slots.Length; i++)
            {
                //have slots be black
                e.Graphics.FillRectangle(Brushes.Black, slots[i]);
            }

            for (int i = 0; i < carryItem.Length; i++)
            {
                //when not carrying items are green
                if (carryItem[i] == false)
                {
                    e.Graphics.FillRectangle(Brushes.Green, items[i]);
                }
                //when carrying items are red 
                else
                {
                    e.Graphics.FillRectangle(Brushes.Red, items[i]);
                }
            }                   
        }        
    }
}
