using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static _TestSW_2.Storage;

namespace _TestSW_2
{
    public partial class App : Form
    {
        public App()
        {
            InitializeComponent();
        }

        void RefreshList()
        {
            dataGridView.Rows.Clear();
            foreach(Person person in Storage.Records)
            {
                dataGridView.Rows.Add(person.Name, person.Surname, person.DOB.ToString(), person.Address);
            }
        }

        void AddPerson()
        {
            Storage.AddPerson(textBoxName.Text, textBoxSurname.Text, dateTimePickerDOB.Value, textBoxAddress.Text);
            RefreshList();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxSurname.TextLength < 3 || !Regex.IsMatch(textBoxSurname.Text, @"^\w+$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Фамилия введена некоректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBoxName.TextLength < 3 || !Regex.IsMatch(textBoxName.Text, @"^\w+$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Имя введено некоректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBoxAddress.TextLength < 5 || Regex.IsMatch(textBoxAddress.Text, @"^[\s.\W]+$"))
            {
                MessageBox.Show("Адрес введен некоректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DateTime dat = dateTimePickerDOB.Value;
            DateTime cur = DateTime.Now;
            int cnt = cur.Year - dat.Year;
            if (cnt > 7 || cnt < 1)
            {
                MessageBox.Show("Возраст ребёнка должен быть от 2 до 8 лет");
            }
            else
            {
                AddPerson();
                MessageBox.Show("Форма успешно заполнена");
            }
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            if (e.Handled)
            {
                ToolTip t = new ToolTip();
                t.Show("Разрешены только символы латиницы и кириллицы", (TextBox)sender, 1000);
            }
        }

        private void textBoxSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            if (e.Handled)
            {
                ToolTip t = new ToolTip();
                t.Show("Разрешены только символы латиницы и кириллицы", (TextBox)sender, 1000);
            }
        }

        private void textBoxAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back) || (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) || e.KeyChar == '"' || e.KeyChar == '/'))
            {
                e.Handled = false;
                return;
            }
            else
            {
                if (e.KeyChar == ' ' || e.KeyChar == '.')
                {
                    TextBox b = ((TextBox)sender);
                    if (b.TextLength > 0)
                    {
                        char c = b.Text.Last();
                        if (c == ' ' || c == '.')
                        {
                            e.Handled = true;
                            ToolTip t1 = new ToolTip();
                            t1.Show("Недопустим ввод двух и более пробелов или точек", (TextBox)sender, 1000);
                            return;
                        }
                        else
                        {
                            e.Handled = false;
                            return;
                        }
                    }
                }
            }
            ToolTip t2 = new ToolTip();
            t2.Show("Разрешены только символы латиницы и кириллицы, цифры, а также пробел и точка", (TextBox)sender, 1000);
            e.Handled = true;
        }

        private void App_Load(object sender, EventArgs e)
        {
            DateTime cur = DateTime.Now;
            int maxdt = cur.Year - 1;
            int mindt = cur.Year - 7;
            dateTimePickerDOB.MaxDate = new DateTime(maxdt, 12, 31);
            dateTimePickerDOB.MinDate = new DateTime(mindt, 12, 31);
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            RefreshList();
        }
    }
}
