using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace System.Windows.Forms
{
    public class IconListBox : Panel
    {
        private int y;
        public IconListBox()
        {
            this.Size=new Size(270, 245);
            this.Paint += new PaintEventHandler(Dibujar);
            imageSize = new Size(32, 32);
            itemHeight = 48;
            left = right = 13;
            top = bottom = 8;
            progressBarHeight = 13;
            Items = new List<IconListBoxItem>(5);
            this.ResizeRedraw = true;
            this.AutoScroll = true;
            y = 1;

            this.ControlAdded += new ControlEventHandler(ControlAñadido);
        }

        void ControlAñadido(object sender, ControlEventArgs e)
        {
            if (e.Control is IconListBoxItem)
            {
                this.Controls.Remove(e.Control);
                this.AddItem((IconListBoxItem)e.Control);
                return;
            }
            else
                throw new ArgumentException("No se pueden añadir controles a un control IconListBox");
        }

        void Dibujar(object sender, PaintEventArgs e)
        {
            if (this.BackColor == SystemColors.Control)
                e.Graphics.FillRectangle(new LinearGradientBrush(this.ClientRectangle, Color.FromArgb(252, 252, 254), Color.FromArgb(244, 243, 238), LinearGradientMode.Vertical), this.ClientRectangle);
          e.Graphics.DrawRectangle(new Pen(Color.FromArgb(127, 157, 185), 1),
                0, 0, this.Width - 1, this.Height - 1);
        }

        public void Clear()
        {
            this.Items.Clear();
            this.Actualizar();
        }
        private void AddItem(IconListBoxItem item, bool AddToItems)
        {
            if (item == null)
                return;
            item.Parent = this;
            item.Dock = DockStyle.Top;

            if (AddToItems)
                Items.Add(item);

            this.ControlAdded -= new ControlEventHandler(ControlAñadido);
            if(Handle!=null)
            this.Invoke(new DelegadoAñadirItem(añadirItem), new object[] { item });
            this.ControlAdded += new ControlEventHandler(ControlAñadido);

            y += this.itemHeight;
        }

        private void añadirItem(IconListBoxItem item)
        {
            this.Controls.Add(item);
            this.Controls.SetChildIndex(item, 0);
        }

        private delegate void DelegadoAñadirItem( IconListBoxItem item);

        public void AddItem(IconListBoxItem item)
        {
            AddItem(item, true);
        }

        public void AddItem(string text, Image imagen)
        {
            IconListBoxItem item = new IconListBoxItem();
            item.Text = text;
            item.Image = imagen;
            AddItem(item, true);
        }

        public void Actualizar()
        {
            if (Items.Count == 0)
            {
                y = 1;
                this.Controls.Clear();
                return;
            }

            this.Controls.Clear();
            y = 1;
            foreach (IconListBoxItem item in Items)
            {
                AddItem(item,false);
            }
        }

        public void RemoveItem(IconListBoxItem item)
        {
            if (item.Selected)
            {
                if (Items.IndexOf(item) != -1 && Items.IndexOf(item) != Items.Count - 1)
                    this.SelectedItem = Items[Items.IndexOf(item) + 1];
                else if (Items.Count > 1)
                    this.SelectedItem = Items[0];
                else
                    this.SelectedItem = null;
            }
            if (Items.IndexOf(item) != -1)
            {
                int indice = Items.IndexOf(item);
                this.Controls.Remove(item);
                y = indice * this.itemHeight;
                if (y == 0) y = 1;
                for (int contador = indice + 1; contador < Items.Count; contador++)
                {
                    Items[contador].Top = y;
                    y += this.itemHeight;
                }
            }
            Items.Remove(item);
        }

        public void RemoveItem(int index)
        {
                RemoveItem(Items[index]);
        }

        public System.Collections.Generic.List<IconListBoxItem> Items;



        public event EventHandler SelectedIndexChanged;
        private IconListBoxItem selected;
        public IconListBoxItem SelectedItem
        {
            get { return selected; }
            set
            {
                if (selected != null)
                {
                    selected.Selected = false;
                }
                selected = value;
                if (value != null)
                {
                    value.Selected = true;
                }     
                if (SelectedIndexChanged != null)
                    SelectedIndexChanged(this, new EventArgs());
            }
        }

        public int SelectedIndex
        {
            get
            {
                try
                {
                    return Items.IndexOf(SelectedItem);
                }
                catch { return -1; }
            }
            set
            {
                try
                {
                    if (value == 0 && Items.Count == 0)
                        return;
                    if (value < 0)
                        SelectedItem = null;
                    else
                    {
                        if (Items.Count >value)
                            SelectedItem = Items[value];
                    }
                }
                catch { }
            }
        }


        public int Count
        {
            get { return Items.Count; }
        }

        private int itemHeight;
        public int ItemHeight
        {
            get { return itemHeight; }
            set { itemHeight = value; this.Refresh(); }
        }

        private int progressBarHeight;
        public int ProgressBarHeight
        {
            get { return progressBarHeight; }
            set { progressBarHeight = value; this.Refresh(); }
        }

        private Size imageSize;
        public Size ImageSize
        {
            get { return imageSize; }
            set { imageSize = value; this.Refresh(); }
        }

        private int top;
        public int BorderTop
        {
            get { return top; }
            set { top = value; this.Refresh(); }
        }

        private int bottom;
        public int BorderBottom
        {
            get { return bottom; }
            set { bottom = value; this.Refresh(); }
        }

        private int left;
        public int BorderLeft
        {
            get { return left; }
            set { left = value; this.Refresh(); }
        }

        private int right;
        public int BorderRight
        {
            get { return right; }
            set { right = value; this.Refresh(); }
        }
    }
}
