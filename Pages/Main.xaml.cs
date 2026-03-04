using ChatStudents_Klimov.Classes;
using ChatStudents_Klimov.Classes.Common;
using ChatStudents_Klimov.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace ChatStudents_Klimov.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Users SelectedUser = null;
        public UsersContext usersContext = new UsersContext();
        public MessagesContext messagesContext = new MessagesContext();
        public DispatcherTimer Timer = new DispatcherTimer() { Interval = new TimeSpan(0,0,3) };

        public Main()
        {
            InitializeComponent();
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        public void LoadUsers()
        {
            foreach (Users user in usersContext.Users)
            {
                if (user.Id != MainWindow.Instance.LoginUser.Id)
                {
                    ParentUsers.Children.Add(new Pages.Items.User(user, this));
                }
            }
        }

        public void SelectUser(Users User)
        {
            SelectedUser = User;
            Chat.Visibility = Visibility.Visible;
            imgUser.Source = BitmapFromArrayByte.LoadImage(User.Photo);
            FIO.Content = User.ToFIO();
            ParentMessages.Children.Clear();

            foreach (Messages Message in messagesContext.Messages.Where(x =>
                (x.UserForm == User.Id && x.UserTo == MainWindow.Instance.LoginUser.Id) ||
                (x.UserForm == MainWindow.Instance.LoginUser.Id && x.UserTo == User.Id)))
            {
                ParentMessages.Children.Add(new Pages.Items.Message(Message, usersContext.Users.Where(x => x.Id == Message.UserForm).First()));
            }
        }

        private void Send(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Messages message = new Messages(
                    MainWindow.Instance.LoginUser.Id,
                    SelectedUser.Id,
                    Message.Text
                    );
                messagesContext.Messages.Add(message);
                messagesContext.SaveChanges();
                ParentMessages.Children.Add(new Pages.Items.Message(message, MainWindow.Instance.LoginUser));
                Message.Text = "";
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (SelectedUser != null)
            {
                SelectUser(SelectedUser);
            }
        }
    }
}
