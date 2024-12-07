using Microsoft.EntityFrameworkCore;
using PL;
using PL.Models;
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;
namespace TaskWithEF
{
    public partial class Form1 : Form , Operations
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e){}
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            arabicText.Text = $"{numericUpDown1.Value.ToString()} %";
            arabicProgressBar.Value = (int)numericUpDown1.Value;
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            englishText.Text = $"{numericUpDown2.Value.ToString()} %";
            englishProgressBar.Value = (int)numericUpDown2.Value;
        }
        private void genderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (genderComboBox.Text == "Male")
            {
                if (!statusComboBox.Items.Contains("Divorced"))
                {
                    statusComboBox.Items.Add("Divorced");
                }
                statusComboBox.Items.Remove("Divorcee");
                statusComboBox.Enabled = true;
            }
            else
            {
                if (!statusComboBox.Items.Contains("Divorcee"))
                {
                    statusComboBox.Items.Add("Divorcee");
                }
                statusComboBox.Items.Remove("Divorced");
                statusComboBox.Enabled = true;
            }
        }
        private void addBtn_Click(object sender, EventArgs e)
        {
            Insert();
        }
        private void IdsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IdsComboBox.Text != null)
            {
                if (IdsComboBox.Text == "Employee")
                {
                    comboBox1.Items.Clear();
                    using (var context = new AppDbContext())
                    {
                        var users = context.Users.Where(x => x.JobTitle == "Employee");
                        foreach (var user in users)
                        {
                            comboBox1.Items.Add(user.Id);
                        }
                    }
                    comboBox1.Enabled = true;
                }
                else
                {
                    comboBox1.Items.Clear();
                    using (var context = new AppDbContext())
                    {
                        var ids = new List<int>();
                        var users = context.Users.Where(x => x.JobTitle == "Manager");
                        foreach (var user in users)
                        {
                            comboBox1.Items.Add(user.Id);
                        }
                    }
                    comboBox1.Enabled = true;

                }
            }
        }
        private void ShowBtn_Click(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                int userId = int.Parse(comboBox1.Text);
                var user = context.Users.Include(u => u.Hobbies).FirstOrDefault(x => x.Id == userId);
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;

                if (user != null)
                {
                    nameText.Text = user.Name;
                    jobTitleComboBox.Text = user.JobTitle;
                    dateTimePicker1.Value = user.BirthDate;
                    genderComboBox.Text = user.Gender;
                    statusComboBox.Text = user.status;
                    numericUpDown1.Value = user.ArabicRate;
                    numericUpDown2.Value = user.EnglishRate;
                    foreach (var hobby in user.Hobbies)
                    {
                        if (hobby.HobbyName1 != "null")
                        {
                            checkBox1.Checked = true;
                        }
                        if (hobby.HobbyName2 != "null")
                        {
                            checkBox2.Checked = true;
                        }
                        if (hobby.HobbyName3 != "null")
                        {
                            checkBox3.Checked = true;
                        }
                        if (hobby.HobbyName4 != "null")
                        {
                            checkBox4.Checked = true;
                        }
                    }
                    dataGridView1.Rows.Clear(); // Clear existing rows
                    dataGridView1.Columns.Clear(); // Clear existing columns
                    dataGridView1.Columns.Add("Name", "Name");
                    dataGridView1.Columns.Add("JobTitle", "Job Title");
                    dataGridView1.Columns.Add("BirthDate", "Birth Date");
                    dataGridView1.Columns.Add("Gender", "Gender");
                    dataGridView1.Columns.Add("Status", "Status");
                    dataGridView1.Columns.Add("ArabicRate", "Arabic Rate");
                    dataGridView1.Columns.Add("EnglishRate", "English Rate");
                    dataGridView1.Columns.Add("HobbyName1", "Hobby 1");
                    dataGridView1.Columns.Add("HobbyName2", "Hobby 2");
                    dataGridView1.Columns.Add("HobbyName3", "Hobby 3");
                    dataGridView1.Columns.Add("HobbyName4", "Hobby 4");
                    foreach (var hobby in user.Hobbies)
                    {
                        dataGridView1.Rows.Add(
                            user.Name,
                            user.JobTitle,
                            user.BirthDate.ToShortDateString(),
                            user.Gender,
                            user.status,
                            user.ArabicRate,
                            user.EnglishRate,
                            hobby.HobbyName1,
                            hobby.HobbyName2,
                            hobby.HobbyName3,
                            hobby.HobbyName4
                        );
                    }
                }
                else
                {
                    MessageBox.Show("User not found.");
                }
            }
        }
        public void Clean(Control parent)
        {
            foreach (Control item in parent.Controls)
            {
                if (item is TextBox || item is ComboBox)
                    item.Text = string.Empty;
                if (item is CheckBox)
                    ((CheckBox)item).Checked = false;
                if (item is Panel || item is GroupBox)
                {
                    Clean(item);
                }
            }
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            arabicProgressBar.Value = 0;
            englishProgressBar.Value = 0;
        }
        public void Insert()
        {
            if (nameText.Text != string.Empty)
            {
                using (var context = new AppDbContext())
                {
                    var user = new User
                    {
                        Name = nameText.Text,
                        JobTitle = jobTitleComboBox.Text,
                        BirthDate = dateTimePicker1.Value,
                        Gender = genderComboBox.Text,
                        status = statusComboBox.Text,
                        ArabicRate = (int)numericUpDown1.Value,
                        EnglishRate = (int)numericUpDown2.Value
                    };
                    var hobbies = new List<string> { "null", "null", "null", "null" };
                    if (checkBox1.Checked) hobbies[0] = checkBox1.Text;
                    if (checkBox2.Checked) hobbies[1] = checkBox2.Text;
                    if (checkBox3.Checked) hobbies[2] = checkBox3.Text;
                    if (checkBox4.Checked) hobbies[3] = checkBox4.Text;
                    var hobby = new Hobbie
                    {
                        HobbyName1 = hobbies[0],
                        HobbyName2 = hobbies[1],
                        HobbyName3 = hobbies[2],
                        HobbyName4 = hobbies[3],
                        User = user
                    };
                    context.Hobbies.Add(hobby);
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                MessageBox.Show("Added Successfully");
                Clean(this);
            }
            else { MessageBox.Show("Sorry, You Have An Error!!"); }
        }
        public void Update()
        {
            using (var context = new AppDbContext())
            {
                if (comboBox1.Text != string.Empty)
                {
                    int userId = int.Parse(comboBox1.Text);
                    var user = context.Users.Include(u => u.Hobbies).FirstOrDefault(x => x.Id == userId);
                    if (user != null)
                    {
                        user.Name = nameText.Text;
                        user.JobTitle = jobTitleComboBox.Text;
                        user.BirthDate = dateTimePicker1.Value;
                        user.Gender = genderComboBox.Text;
                        user.status = statusComboBox.Text;
                        user.ArabicRate = (int)numericUpDown1.Value;
                        user.EnglishRate = (int)numericUpDown2.Value;
                        var hobbies = new List<string> { "null", "null", "null", "null" };
                        if (checkBox1.Checked) hobbies[0] = checkBox1.Text;
                        if (checkBox2.Checked) hobbies[1] = checkBox2.Text;
                        if (checkBox3.Checked) hobbies[2] = checkBox3.Text;
                        if (checkBox4.Checked) hobbies[3] = checkBox4.Text;
                        var existingHobby = user.Hobbies.FirstOrDefault();
                        if (existingHobby != null)
                        {
                            existingHobby.HobbyName1 = hobbies[0];
                            existingHobby.HobbyName2 = hobbies[1];
                            existingHobby.HobbyName3 = hobbies[2];
                            existingHobby.HobbyName4 = hobbies[3];
                        }
                        else
                        {
                            var newHobby = new Hobbie
                            {
                                HobbyName1 = hobbies[0],
                                HobbyName2 = hobbies[1],
                                HobbyName3 = hobbies[2],
                                HobbyName4 = hobbies[3],
                                User = user
                            };
                            context.Hobbies.Add(newHobby);
                        }

                        context.SaveChanges();
                        MessageBox.Show("Updated Successfully");
                        Clean(this);
                    }
                }
                else
                {
                    MessageBox.Show("User not found.");
                }
            }
        }
        public void Delete() {
            using (var context = new AppDbContext())
            {
                if (comboBox1.Text != string.Empty)
                {
                    int userId = int.Parse(comboBox1.Text);
                    var user = context.Users.Include(u => u.Hobbies).FirstOrDefault(x => x.Id == userId);
                    if (user != null)
                    {
                        context.Hobbies.RemoveRange(user.Hobbies);
                        context.Users.Remove(user);
                        context.SaveChanges();
                        MessageBox.Show("User and related hobbies deleted successfully.");
                        Clean(this);
                    }
                }
                else
                {
                    MessageBox.Show("User not found.");
                }
            }
        }
        private void updateBtn_Click(object sender, EventArgs e)
        {
            Update();
        }
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            Delete();
        }
    }
}