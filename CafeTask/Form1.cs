using System;
using System.Globalization;
using System.Windows.Forms;

namespace CafeTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double total = 0;

        private void UpdateTotalDisplay()
        {
            label5.Text = total > 0 ? Math.Round(total, 2).ToString() : "0";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Burger - 6.3");
            total += 6.3;
            UpdateTotalDisplay();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Armud - 2.5");
            total += 2.5;
            UpdateTotalDisplay();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Tort - 4.5");
            total += 4.5;
            UpdateTotalDisplay();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Portağal - 2.0");
            total += 2.0;
            UpdateTotalDisplay();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Pizza - 8.5");
            total += 8.5;
            UpdateTotalDisplay();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Çizburger - 5.5");
            total += 5.5;
            UpdateTotalDisplay();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Kola - 2.0");
            total += 2.0;
            UpdateTotalDisplay();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Kokteyl - 3.5");
            total += 3.5;
            UpdateTotalDisplay();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Spaqetti - 6.0");
            total += 6.0;
            UpdateTotalDisplay();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && listBox1.SelectedItem != null)
            {
                string selectedItem = listBox1.SelectedItem.ToString() ?? "";

                string[] parts = selectedItem.Split('-');
                if (parts.Length > 1 && double.TryParse(parts[1].Trim().Replace(',', '.'), CultureInfo.InvariantCulture, out double price))
                {
                    total -= price;
                    if (total < 0) total = 0;
                }

                listBox1.Items.RemoveAt(listBox1.SelectedIndex);

                if (listBox1.Items.Count == 0)
                {
                    total = 0;
                    textBox2.Clear();
                }

                UpdateTotalDisplay();

                string foodName = parts.Length > 0 ? parts[0].Trim() : selectedItem;
                MessageBox.Show($"{foodName} səbətdən silindi", "Bildiriş", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Zəhmət olmasa silmək istədiyiniz yeməyi səbətdən seçin!", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xanalar sıfırlansınmı?", "Bildiriş", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                listBox1.Items.Clear();
                textBox1.Clear();
                textBox2.Clear();
                label5.Text = "0";
                total = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Səbətdə yemək yoxdur!", "Bildiriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                double sum = 0;
                foreach (var item in listBox1.Items)
                {
                    if (item == null) continue;
                    string[] parts = item.ToString()?.Split('-') ?? Array.Empty<string>();
                    if (parts.Length > 1 && double.TryParse(parts[1].Trim().Replace(',', '.'), CultureInfo.InvariantCulture, out double p))
                    {
                        sum += p;
                    }
                }
                if (sum > 0) total = sum;

                textBox2.Text = Math.Round(total, 2).ToString();
                UpdateTotalDisplay();

                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    HesablaQaliq();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HesablaQaliq();
        }

        private void HesablaQaliq()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Zəhmət olmasa məbləği daxil edin!", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(textBox1.Text.Trim().Replace(',', '.'), CultureInfo.InvariantCulture, out double mebleg))
            {
                MessageBox.Show("Daxil edilən məbləğ düzgün deyil!", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double hesab = 0;
            if (!string.IsNullOrWhiteSpace(textBox2.Text) && double.TryParse(textBox2.Text.Trim().Replace(',', '.'), CultureInfo.InvariantCulture, out double parsedHesab))
            {
                hesab = parsedHesab;
            }
            else if (listBox1.Items.Count > 0)
            {
                double sum = 0;
                foreach (var item in listBox1.Items)
                {
                    if (item == null) continue;
                    string[] parts = item.ToString()?.Split('-') ?? Array.Empty<string>();
                    if (parts.Length > 1 && double.TryParse(parts[1].Trim().Replace(',', '.'), CultureInfo.InvariantCulture, out double p))
                    {
                        sum += p;
                    }
                }
                if (sum > 0) total = sum;

                hesab = total;
                textBox2.Text = Math.Round(total, 2).ToString();
            }
            else
            {
                MessageBox.Show("Səbətdə yemək yoxdur!", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mebleg < hesab)
            {
                MessageBox.Show("Daxil edilən məbləğ hesabdan azdır", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                double qaliq = mebleg - hesab;
                label5.Text = Math.Round(qaliq, 2).ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            label5.Text = "0";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }
    }
}
