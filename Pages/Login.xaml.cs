using ChatStudents_Klimov.Classes;
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChatStudents_Klimov.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public string srcUserImage = "";
        UsersContext usersContext = new UsersContext();

        public Login()
        {
            InitializeComponent();
        }

        public bool CheckEmpty(string Pattern, string Input)
        {
            Match m = Regex.Match(Input, Pattern);
            return m.Success;
        }

        private void SelectPhoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите фотографию:";
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                imgUser.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                srcUserImage = openFileDialog.FileName;
            }
        }

        private void Continue(object sender, RoutedEventArgs e)
        {
            if (!CheckEmpty("^[А-ЯёЁ][а-яА-ЯёЁ]*$", Lastname.Text))
            {
                MessageBox.Show("Укажите фамилию!");
                return;
            }
            if (!CheckEmpty("^[А-ЯёЁ][а-яА-ЯёЁ]*$", Firstname.Text))
            {
                MessageBox.Show("Укажите имя!");
                return;
            }
            if (!CheckEmpty("^[А-ЯёЁ][а-яА-ЯёЁ]*$", Surename.Text))
            {
                MessageBox.Show("Укажите отчество!");
                return;
            }
            if (String.IsNullOrEmpty(srcUserImage))
            {
                MessageBox.Show("Выберите изображение!");
                return;
            }
            if (usersContext.Users.Where(x => x.Firstname == Firstname.Text &&
                                              x.Lastname == Lastname.Text &&
                                              x.Surename == Surename.Text).Count() > 0)
            {
                MainWindow.Instance.LoginUser = usersContext.Users.Where(x => x.Firstname == Firstname.Text &&
                                                                              x.Lastname == Lastname.Text &&
                                                                              x.Surename == Surename.Text).First();
                MainWindow.Instance.LoginUser.Photo = File.ReadAllBytes(srcUserImage);
                usersContext.SaveChanges();
            }
            else
            {
                usersContext.Users.Add(new Models.Users(Lastname.Text, Firstname.Text, Surename.Text, File.ReadAllBytes(srcUserImage)));
                usersContext.SaveChanges();
                MainWindow.Instance.LoginUser = usersContext.Users.Where(x => x.Firstname == Firstname.Text &&
                                                                              x.Lastname == Lastname.Text &&
                                                                              x.Surename == Surename.Text).First();
            }
            MainWindow.Instance.OpenPages(new Pages.Main());
        }
    }
}
