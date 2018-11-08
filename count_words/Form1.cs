using LemmaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace count_words
{
    public partial class Form1 : Form
    {
        static List<Word> words = new List<Word>();

        public Form1()
        {
            InitializeComponent();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            label2.Text = "0";
            progressBar2.Value = 0;
            progressBar1.Value = 0;
            label5.Text = "0";
            label6.Text = "0";
            label8.Text = "0";
            listView2.Items.Clear();
        }



        private void action(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor = Color.White;
            var str = listView2.SelectedItems[0].SubItems[0].Text;
            var word = words.Find(p => p.Words == str);
            var lenght = word.Words.Length;
            foreach (var pos in word.WordPos)
            {
                richTextBox1.Select(pos, lenght);
                richTextBox1.SelectionBackColor = Color.Red;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var text = richTextBox1.Text;
            char x = Convert.ToChar(092);
            string[] st = text.Split(new char[] { '\n', ',', ':', ' ', '.', '?', '"', '—', '*', '«', '»', '…', '-', '(', ')', '{', '}', '=', '/', x }, StringSplitOptions.RemoveEmptyEntries);
            float cw = st.Count();
            label2.Text = cw.ToString();
            words = StringHelper.EqualWords(st);
            float uw = StringHelper.UniqWords(st);
            float puw = 0;
            if (cw > 0)  puw = (uw / cw) * 100; 
            else  puw = 0; 
            progressBar2.Value = int.Parse(String.Format("{0:0}", puw));
            label8.Text = String.Format("{0:0.00}%", puw);
            label5.Text = uw.ToString();
            List<Word> words1 = new List<Word>();
            foreach (var y in words)
            {
                words1.Add((Word)y.Clone());
            }
            words.Clear();
            progressBar1.Maximum = words1.Count();
            progressBar1.Value = 0;
            
            for (var i = 0; i < words1.Count(); i++)
            {
                List<int> wordPos = new List<int>();
                List<string> str = new List<string>();
                for (var j = i; j - i <= words1[i].CountNotLem - 1; j++)
                {
                    str.Add(words1[j].Words);
                }
                wordPos = StringHelper.GetIndexForKeyWord(text, str);
                if (wordPos.Count > 1)
                {
                        words.Add(new Word(words1[i].Words, wordPos, wordPos.Count));
                }
                i += words1[i].CountNotLem - 1;
                progressBar1.Value = i;

            }
            words.Sort((a, b) => a.Count.CompareTo(b.Count));

            label6.Text = words.Count().ToString();
            for (var i = 0; i < words.Count(); i++)
            {
                ListViewItem lvi = new ListViewItem(words[i].Words);
                lvi.SubItems.Add(words[i].Count.ToString());
                listView2.Items.Add(lvi);
            }

        }
    }
}
