using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace test_OS
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            //textBox2.Text = @"C:\Users\"; //для наглядности

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string initialDirectoryPath = @"C:\Users\79265\Desktop\check_files";
            textBox1.Text = initialDirectoryPath;
            Standart(textBox1.Text);

            timer1.Start(); // Start the timer when the form loads
        }

        private void Standart(string DirectoryPath)
        {
            listBox1.Items.Clear();

            try
            {
                DirectoryInfo dir = new DirectoryInfo(textBox1.Text);

                DirectoryInfo[] dirs = dir.GetDirectories();

                foreach (DirectoryInfo crrDir in dirs)
                {
                    listBox1.Items.Add(crrDir);
                }

                FileInfo[] files = dir.GetFiles();

                foreach (FileInfo crrFile in files)
                {
                    listBox1.Items.Add(crrFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Standart(textBox1.Text);
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Path.GetExtension(Path.Combine(textBox1.Text, listBox1.SelectedItem.ToString())) == "")
            {

                textBox1.Text = Path.Combine(textBox1.Text, listBox1.SelectedItem.ToString());

                Standart(textBox1.Text);
            }
            else
            {
                Process.Start(Path.Combine(textBox1.Text, listBox1.SelectedItem.ToString()));
            }

            Standart(textBox1.Text);
        }

        private void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition, ToolStripDropDownDirection.Right);
            }
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedFileName = listBox1.SelectedItem.ToString();

            Clipboard.SetText(Path.Combine(textBox1.Text, selectedFileName));

            MessageBox.Show("File path copied to clipboard");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random random = new Random();

            this.BackColor = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get the file path from the clipboard
            string clipboardText = Clipboard.GetText();

            if (!string.IsNullOrEmpty(clipboardText) && File.Exists(clipboardText))
            {
                string destinationFilePath = Path.Combine(textBox1.Text, Path.GetFileName(clipboardText));

                // Copy the file to the destination directory
                File.Copy(clipboardText, destinationFilePath);
                MessageBox.Show("File pasted successfully");
            }
            else
            {
                MessageBox.Show("No valid file path");
            }
            Standart(textBox1.Text);
        }

        private void переместитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get the file path from the clipboard
            string clipboardText = Clipboard.GetText();

            if (!string.IsNullOrEmpty(clipboardText) && File.Exists(clipboardText))
            {
                string destinationFilePath = Path.Combine(textBox1.Text, Path.GetFileName(clipboardText));

                // Copy the file to the destination directory
                File.Move(clipboardText, destinationFilePath);
                MessageBox.Show("File replaced successfully");
            }
            else
            {
                MessageBox.Show("No valid file path");
            }
            Standart(textBox1.Text);
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(textBox1.Text, listBox1.SelectedItem.ToString());
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }
            Standart(textBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text[textBox1.Text.Length - 1] == '\\')
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);

                while (textBox1.Text[textBox1.Text.Length - 1] != '\\')
                {
                    textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                }
            }
            else if (textBox1.Text[textBox1.Text.Length - 1] != '\\')
            {
                while (textBox1.Text[textBox1.Text.Length - 1] != '\\')
                {
                    textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                }
            }
            Standart(textBox1.Text);
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //textBox2.Text = @"C:\Users\"; //для наглядности 

            string path_make = textBox1.Text;

            string NewPath = Path.Combine(path_make, textBox2.Text);

            try
            {
                Directory.CreateDirectory(NewPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }

            Standart(textBox1.Text);
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {


            string filePath = Path.Combine(textBox1.Text, listBox1.SelectedItem.ToString());


            try
            {
                FileInfo fileInfo = new FileInfo(filePath);

                // Отображение информации о файле в текстовом поле
                textBox3.Text = $"Имя файла: {fileInfo.Name}" + Environment.NewLine +
                                       $"Расположение: {fileInfo.DirectoryName}" + Environment.NewLine +
                                       $"Размер (в байтах): {fileInfo.Length}" + Environment.NewLine +
                                       $"Последний доступ: {fileInfo.LastAccessTime}" + Environment.NewLine +
                                       $"Последнее изменение: {fileInfo.LastWriteTime}" + Environment.NewLine +
                                       $"Время создания: {fileInfo.CreationTime}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
    }
}
