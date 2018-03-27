using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System;


namespace ChatService
{
    #region Interfases

    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatCallback))]
    public interface IChat
    {
        [OperationContract(IsInitiating = true, IsOneWay = false, IsTerminating = false)]
        String[] Join(string name);

        [OperationContract(IsInitiating = false, IsOneWay = true, IsTerminating = false)]
        void Send(string msg);
        
        [OperationContract(IsInitiating = false, IsOneWay = true, IsTerminating = true)]
        void Leave();
    }

    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void Receive(string name, string msg);

        [OperationContract(IsOneWay = true)]
        void UserEnter(string name);

        [OperationContract(IsOneWay = true)]
        void UserLeave(string name);
    }

    #endregion


    internal class ChatUser
    {
        public String Name { get; set; }
        public IChatCallback Callback { get; set; }
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Chat : IChat
    {
        private ChatUser _user;
        private static List<ChatUser> _chatUsers = new List<ChatUser>();
        
        
        public string[] Join(string name)
        {
            //Получаем Интерфейс обратного вызова
            IChatCallback callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();

            //Массив всех участников чата, который мы вернем клиенту
            string[] tmpUsers = new string[_chatUsers.Count];
            for (int i = 0; i < _chatUsers.Count; i++)
            {
                tmpUsers[i] = _chatUsers[i].Name;
            }

            //Оповещаем всех клиентов что в чат вощел новый пользователь
            foreach (ChatUser user in _chatUsers)
            {
                user.Callback.UserEnter(name);
            }

            //Создаем новый экземплар пользователя и заполняем все его поля
            ChatUser chatUser = new ChatUser() { Name = name, Callback = callback };
            _chatUsers.Add(chatUser);
            _user = chatUser;
            Console.WriteLine(">>User Enter: {0}", name);
            return tmpUsers;
        }

        public void Leave()
        {
            _chatUsers.Remove(_user);
            //Оповещаем всех клиентов о том что пользователь нас покинул
            foreach (ChatUser item in _chatUsers)
            {
                item.Callback.UserLeave(_user.Name);
            }
            _user = null;

            //Закрываем канал связи с текущим пользователем
            OperationContext.Current.Channel.Close();
        }

        public void Send(string msg)
        {
            var usersSending = from u in _chatUsers
                               where u.Name != _user.Name
                               select u;
            foreach (ChatUser user in usersSending)
            {
                user.Callback.Receive(_user.Name, msg);
            }
        }
    }
}