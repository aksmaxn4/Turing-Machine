using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace MT
{
    public partial class Form1 : Form
    {
        int x = 0;//переменная для выставления интервалов таймера   
        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            RulesDataGrid();
           
            
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)//запрещения ввода кроме с, d и BackSpace
        {
            if (e.KeyChar.ToString() == "d" || e.KeyChar.ToString() == "c" || e.KeyChar == (char)Keys.Back)
                e.Handled = false;
            else e.Handled = true;
        }

        public void ToDataGridView()//ввод в DGW слова из TextBox
        {
            int i, j;
            char[] word = textBox1.Text.ToCharArray();

            for(j=0, i=1; j<word.Length;j++)
            {
                dataGridView1[i++, 0].Value = word[j];
            }
        }

        private void Button1_Click(object sender, EventArgs e)//кнопка для ввода в DGV слова из TextBox
        {
            if (textBox1.Text.Length > 12) MessageBox.Show("Введите слово до 12 символов");
            else
            {
                textBox2.Enabled = true;
                
                dataGridView1.Rows.Clear();
                ToDataGridView();
                
            }
        }

        public void TuringMachine(DataGridViewCell cell)//первое состояние с условиями перехода в другое состояние и заменами символов;как аргумент передается начальная ячейка DGV
        {
            Thread.Sleep(x);//приостанавливает текущий поток
            int current = cell.ColumnIndex;
            if (cell.Value == null) { DataGridClear(); dataGridView2[3, 0].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh(); label2.Left -= 30; dataGridView1.Refresh(); Seventh(dataGridView1[--current, 0]);  }
            else if (cell.Value.ToString() == "c")
            {
                DataGridClear();//присвоение белого цвета
                cell.Value = "@";//замена символа
                dataGridView2[0, 0].Style.BackColor = System.Drawing.Color.Red;//присваивание соответствующей ячейе в таблице состояний красного цвета
                dataGridView2.Refresh();//обновление таблицы состояний
                label2.Left += 30;//перемещение головки МТ
                dataGridView1.Refresh();//обновлеие ленты МТ
                Second(dataGridView1[++current, 0]);//переход в другое состояние
                //этот алгоритм идет дальше по всей программе 
            }
            else if (cell.Value.ToString() == "d")
            {
                DataGridClear();
                cell.Value = "@";
                dataGridView2[1, 0].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh();
                label2.Left += 30;
                dataGridView1.Refresh();
                Third(dataGridView1[++current, 0]);
            }
            else if (cell.Value.ToString() == "@") { DataGridClear(); dataGridView2[2, 0].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh(); dataGridView1.Refresh(); label2.Left += 30; TuringMachine(dataGridView1[++current, 0]); }
             
              
        }
        public void Second(DataGridViewCell cell)//второе состояние
        {
            Thread.Sleep(x);
            int current = cell.ColumnIndex;
            if (cell.Value == null)
            {
                DataGridClear();
                dataGridView2[3, 1].Style.BackColor = System.Drawing.Color.Red;
                dataGridView2.Refresh();
                dataGridView1.Refresh();
                label2.Left -= 30;
                Fourth(dataGridView1[--current, 0]);
            }
            else if (cell.Value.ToString() == "c") { DataGridClear(); dataGridView2[0, 1].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh(); dataGridView1.Refresh(); label2.Left += 30; Second(dataGridView1[++current, 0]); }
            else if (cell.Value.ToString() == "d")
            {
                DataGridClear();
                dataGridView2[1, 1].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh();
                cell.Value = "@";
                dataGridView1.Refresh();
                Eighth(dataGridView1[current, 0]);

            }
            else if (cell.Value.ToString() == "@") { DataGridClear(); dataGridView2[2, 1].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh(); label2.Left += 30; dataGridView1.Refresh(); Second(dataGridView1[++current, 0]); }
            
        }

        public void Third(DataGridViewCell cell)//третье состояние
        {
            Thread.Sleep(x);
            int current = cell.ColumnIndex;
            if (cell.Value == null) { DataGridClear(); dataGridView2[3, 2].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh(); label2.Left -= 30; dataGridView1.Refresh(); Fifth(dataGridView1[--current, 0]); }
            else if (cell.Value.ToString() == "c")
            {
                DataGridClear();
                dataGridView2[0, 2].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh();
                cell.Value = "@"; dataGridView1.Refresh();
                Eighth(dataGridView1[current, 0]);
            }
            else if (cell.Value.ToString() == "d") { DataGridClear(); dataGridView2[1, 2].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh(); label2.Left += 30; dataGridView1.Refresh(); Third(dataGridView1[++current, 0]); }
            else if (cell.Value.ToString() == "@") { DataGridClear(); dataGridView2[2, 2].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh(); label2.Left += 30; dataGridView1.Refresh(); Third(dataGridView1[++current, 0]); }
             
        }
        public void Fourth(DataGridViewCell cell)//четвертое состояние
        {
            Thread.Sleep(x);
            int current = cell.ColumnIndex;
            if (cell.Value == null) { DataGridClear(); dataGridView2[3, 3].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh(); label2.Left += 30; dataGridView1.Refresh(); EnableAll(); return; }
            else if(cell.Value.ToString() == "c")
            {
                DataGridClear();
                dataGridView2[0, 3].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh();
                cell.Value = null;
                label2.Left -= 30; dataGridView1.Refresh();
                Fourth(dataGridView1[--current, 0]);
               
            }
            else if (cell.Value.ToString() == "@")
            {
                DataGridClear();
                dataGridView2[2, 3].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh();
                cell.Value = null;
                label2.Left -= 30; dataGridView1.Refresh();
                Fourth(dataGridView1[--current, 0]);
                
            }
             
        }

        public void Fifth(DataGridViewCell cell)//пятое состояние 
        {
            Thread.Sleep(x);
            int current = cell.ColumnIndex;
            if (cell.Value == null)
            {
                DataGridClear();
                dataGridView2[3, 4].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh();
                Thread.Sleep(x);
                dataGridView1[current,0].Value = "d";//шестое состояни, которое просто заканчивает алгоритм
                label2.Left += 30; dataGridView1.Refresh();

                DataGridClear();

                
                dataGridView2[1, 5].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh();
                EnableAll();//включение элементов управления
                if(MessageBox.Show("Алгоритм закончен! Применить алгоритм к полученному слову?","Успех!",MessageBoxButtons.OKCancel)==DialogResult.OK)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1[1, 0].Value = "d";
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    textBox2.Clear();
                    button2.Enabled = false;
                    
                }

                return;

            }
            else if (cell.Value.ToString() == "d")
            {
                DataGridClear();
                dataGridView2[1, 4].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh();
                cell.Value = null;
                label2.Left -= 30; dataGridView1.Refresh();
                Fifth(dataGridView1[--current, 0]);
               
            }
            else if (cell.Value.ToString() == "@")
            {
                DataGridClear();
                dataGridView2[2, 4].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh();
                cell.Value = null;
                label2.Left -= 30; dataGridView1.Refresh();
                Fifth(dataGridView1[--current, 0]);
                
            }
             
        }
        public void Seventh(DataGridViewCell cell)//седьмое состояние
        {
            Thread.Sleep(x);
            int current = cell.ColumnIndex;
            if (cell.Value == null)
            {
                DataGridClear();
                dataGridView2[3, 6].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh();
                               
                dataGridView1[current, 0].Value = "c"; dataGridView1.Refresh();
                Seventh(dataGridView1[current, 0]);
               
               

                
            }
            else if (cell.Value.ToString()=="c")
            {
                label2.Left += 30;
                DataGridClear();
                dataGridView2[0, 6].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh();
                EnableAll();

                if (MessageBox.Show("Алгоритм закончен! Применить алгоритм к полученному слову?", "Успех!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1[1, 0].Value = "c";
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    textBox2.Clear();
                    button2.Enabled = false;

                }
                return;

            }
           else if (cell.Value.ToString()=="@")
            {
                DataGridClear();
                dataGridView2[2, 6].Style.BackColor = System.Drawing.Color.Red;
                dataGridView2.Refresh();
                cell.Value = null; dataGridView1.Refresh();
                label2.Left -= 30;
                Seventh(dataGridView1[--current, 0]);
                
            }
            
        }
        public void Eighth(DataGridViewCell cell)//восьмое состояние
        {
            Thread.Sleep(x);
            int current = cell.ColumnIndex;
            if (cell.Value == null) { DataGridClear(); dataGridView2[3, 7].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh(); label2.Left += 30; dataGridView1.Refresh(); TuringMachine(dataGridView1[++current, 0]); }
            else if (cell.Value.ToString() == "c") { DataGridClear(); dataGridView2[0, 7].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh(); label2.Left -= 30; dataGridView1.Refresh(); Eighth(dataGridView1[--current, 0]); }
            else if (cell.Value.ToString() == "d") { DataGridClear(); dataGridView2[1, 7].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh(); label2.Left -= 30; dataGridView1.Refresh(); Eighth(dataGridView1[--current, 0]); }
            else if (cell.Value.ToString() == "@") { DataGridClear(); dataGridView2[2, 7].Style.BackColor = System.Drawing.Color.Red; dataGridView2.Refresh(); label2.Left -= 30; dataGridView1.Refresh(); Eighth(dataGridView1[--current, 0]); }
             

        }

        private void Button2_Click(object sender, EventArgs e)//запуск алгоритма
        {

            x = Convert.ToInt32(textBox2.Text)*100;//выставление таймера
            if (x > 1500)
            {
                MessageBox.Show("Введите значение не больше 15!");
                return;
            }
            else
            {
                DisableAll();//отключение всех элементов управление
                TuringMachine(dataGridView1[1, 0]);//запуск МТ с 1 ячейки
                textBox2.Enabled = true;
                button1.Enabled = true;
            }
            
            
        }

        private void TextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox2.Text.Length == 0) button2.Enabled = false;
            else button2.Enabled = true;
        }
        private void DisableAll()//отключение элементов управления
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            button2.Enabled = false;
            button1.Enabled = false;
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)//запрещение ввода символов кроме цифр в текстбокс таймера
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) e.Handled = true; else e.Handled = false;
        }
        private void EnableAll()//включение элементов управления
        {
            textBox1.Enabled = true;
            button1.Enabled = true;
            if (textBox2.Text.Length > 0) button2.Enabled = true;
        }

        private void RulesDataGrid()//оформление таблицы с состояниями МТ
        {
            dataGridView2.Rows.Add(8);
            dataGridView2.RowHeadersWidth = 50;
            for(int i=1;i<=dataGridView2.Rows.Count;i++)
            {
                dataGridView2.Rows[i - 1].HeaderCell.Value = "q" + i.ToString();
                
            }
            dataGridView2[0, 0].Value = "q2,R,@";
            dataGridView2[0, 1].Value = ",R,";
            dataGridView2[0, 2].Value = "q8,@,";
            dataGridView2[0, 3].Value = ",L,λ";
            dataGridView2[0, 4].Value = "—";
            dataGridView2[0, 5].Value = "—";
            dataGridView2[0, 6].Value = ",R,!";
            dataGridView2[0, 7].Value = ",L,";
            dataGridView2[1, 0].Value = "q3,R,@";
            dataGridView2[1, 1].Value = "q8,@,";
            dataGridView2[1, 2].Value = ",R,";
            dataGridView2[1, 3].Value = "—";
            dataGridView2[1, 4].Value = ",L,λ";
            dataGridView2[1, 5].Value = ",R,!";
            dataGridView2[1, 6].Value = "—";
            dataGridView2[1, 7].Value = ",L,";
            dataGridView2[2, 0].Value = ",R,";
            dataGridView2[2, 1].Value = ",R,";
            dataGridView2[2, 2].Value = ",R,";
            dataGridView2[2, 3].Value = ",L,λ";
            dataGridView2[2, 4].Value = ",L,λ";
            dataGridView2[2, 5].Value = "—";
            dataGridView2[2, 6].Value = ",L,λ";
            dataGridView2[2, 7].Value = ",L,";
            dataGridView2[3, 0].Value = "q7,L,";
            dataGridView2[3, 1].Value = "q4,L,";
            dataGridView2[3, 2].Value = "q5,L,";
            dataGridView2[3, 3].Value = ",R,!";
            dataGridView2[3, 4].Value = "q6,N,d";
            dataGridView2[3, 5].Value = "—";
            dataGridView2[3, 6].Value = ",N,c";
            dataGridView2[3, 7].Value = "q1,R,";
            
            
        }

        private void DataGridView2_SelectionChanged(object sender, EventArgs e)//отмена выделения ячеек DGV
        {


            dataGridView2.ClearSelection();
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)//отмена выделения ячеек DGV
        {
            dataGridView1.ClearSelection();
        }
        private void DataGridClear()//присваение всей таблице состояний белого цвета фона
        {
            for (int i = 0; i < dataGridView2.ColumnCount; i++)
                for (int j = 0; j < dataGridView2.Rows.Count; j++)
                    dataGridView2[i, j].Style.BackColor = System.Drawing.Color.White;
            
        }
    }
}
