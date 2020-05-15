using Model;
using MongoDB.Bson;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft;

namespace Service
{
    [DataContract]
    public class Remember
    {
        [DataMember]
        public string Nickname { get; set; }

        public Remember(){}

        public Remember(string Nickname)
        {
            this.Nickname = Nickname;
        }
    }

    public class UserService
    {
        UserRepository repository;
        public UserService()
        {
            repository = new UserRepository();
        }


        public bool UpdatePassword(string username, string oldpasssword, string newpassword)
        {
            if (CheckPassword(username, oldpasssword))
            {
                repository.UpdateField(username, "Password", GetHashStringSHA256(newpassword));
                return true;
            }
            else
            {
                return false;
            }
        }
        

        public bool CheckPassword(string nickname, string password)
        {
            User user = new User();
            user = repository.GetUser(nickname);
            if (user != null)
            {
                if (user.Password == GetHashStringSHA256(password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public string GetHashStringSHA256(string str)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hashPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
            string result = "";
            foreach (byte b in hashPassword)
            {
                result += b.ToString();
            }
            return result;
        }

        public bool CheckIndentityOfNickname(string nickname)
        {
            List<User> users = new List<User>();
            users = repository.GetUsers();
            foreach (var elem in users)
            {
                if (elem.Nickname == nickname)
                {
                    return false;
                }
            }

            return true;
        }



        public void NicknameWrite(string Nickname)
        {
            var user = new Remember();

            user.Nickname = Nickname;
            if (user != null)
            {
                using (FileStream fs = new FileStream("Remember.json", FileMode.Create))
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Remember));
                    jsonFormatter.WriteObject(fs, user);
                }
            }
            else
            {
                using (FileStream fs = new FileStream("Remember.json", FileMode.Create))
                {
                    user = new Remember { Nickname = "" };
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Remember));
                    jsonFormatter.WriteObject(fs, user);
                }
            }


        }

        public string NicknameRead()
        {
            var user = new Remember();
            using (FileStream fs = new FileStream("NickInfo.json", FileMode.OpenOrCreate))
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Remember));
                if (fs.Length != 0)
                {
                    user = (Remember)jsonFormatter.ReadObject(fs);
                }
            }
            return user.Nickname;
        }



        public bool CheckAlreadyFollow(string nickname, string usernickname)
        {
            User user = new User();
            user = repository.GetUser(nickname);
            if (user != null && user.Following != null)
            {
                foreach (var el in user.Following)
                {
                    if (el == usernickname)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public bool CheckIfUserIsInDatabase(string nickname)
        {
            User user = new User();
            user = repository.GetUser(nickname);
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public void InsertUser(string nickname, string email, string password)
        {
            User user = new User();
            user.Nickname = nickname;
            user.Email = email;
            user.Password = GetHashStringSHA256(password);
            repository.Add(user);
        }



        public User GetUser()
        {
            try
            {
                return repository.GetUser(NicknameRead());
            }
            catch
            {
                return new User();
            }

        }

        public ObjectId GetUserId()
        {
            try
            {
                return repository.GetId(NicknameRead());
            }
            catch
            {
                return new ObjectId();
            }

        }

        public User GetUser(string nickname)
        {
            try
            {
                return repository.GetUser(nickname);
            }
            catch
            {
                return new User();
            }

        }

        public IList<User> GetAllUsers()
        {
            IList<User> result = new List<User>();
            foreach (User currentUser in repository.GetUsers())
            {
                result.Add(currentUser);
            }
            return result;
        }

        public IList<string> GetAllUsersNicknames()
        {
            IList<string> result = new List<string>();
            foreach (User currentUser in repository.GetUsers())
            {
                result.Add(currentUser.Nickname);
            }
            return result;
        }

        public User GetUser(ObjectId id)
        {
            try
            {
                return repository.GetUser(id);
            }
            catch
            {
                return new User();
            }

        }

        public List<string> GetFollowers()
        {
            List<string> ls = new List<string>();
            try
            {
                ls = repository.GetFollowers(NicknameRead());
                return ls;
            }
            catch
            {
                return ls;
            }
        }


        public int GetFollowers(string username)
        {
            int result = 0;
            if (repository.GetUser(username).Followers != null)
            {
                result = repository.GetUser(username).Followers.Count;
            }
            return result;
        }


        public int GetFollowing(string username)
        {
            int result = 0;
            if (repository.GetUser(username).Following != null)
            {
                result = repository.GetUser(username).Following.Count;
            }
            return result;
        }

        public List<string> GetFollowing()
        {
            List<string> ls = new List<string>();
            try
            {
                ls = repository.GetFollowing(NicknameRead());
                return ls;
            }
            catch
            {
                return ls;
            }
        }


    }
}
