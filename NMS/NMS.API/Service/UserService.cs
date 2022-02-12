using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NMS.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NMS.API.Service
{
    public class UserService
    {
        public bool Create(User dto, string filepath)
        {
            try
            {
                var UserData = DataDeserialize(filepath);
                UserData.Add(dto);
                UserJsonSave(UserData, filepath);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool UniqueCheck(Guid UserId, string filepath)
        {
            var UserData = DataDeserialize(filepath);
            bool alreadyExists = false;
            foreach (var s in UserData)
            {
                if (s.Id == UserId)
                {
                    alreadyExists = true;
                }
            }
            return alreadyExists;
        }
        public List<User> DataDeserialize(string filepath)
        {
            var NewJson = new List<User>();
            var CurrentJson = File.ReadAllText(filepath);
            if (!String.IsNullOrEmpty(CurrentJson))
            {
                var CurrentJsonParse = JObject.Parse(CurrentJson);
                var CurrentJsonRoutes = CurrentJsonParse["Users"];
                NewJson = JsonConvert.DeserializeObject<List<User>>(CurrentJsonRoutes.ToString());
            }
            return NewJson;
        }
        public List<User> GetByUser(Guid id, string filepath)
        {
            var allData = DataDeserialize(filepath);
            var dataByUser = allData.Where(w => w.Id == id).ToList();
            return dataByUser;
        }
        public bool Update(User model, string filepath)
        {
            try
            {
                var allData = DataDeserialize(filepath);
                var dataById = allData.FirstOrDefault(w => w.Id == model.Id);
                allData.Remove(dataById);
                dataById.Id = model.Id;
                dataById.Name = model.Name;
                dataById.Email = model.Email;
                dataById.Password = model.Password;
                dataById.DateOfBirth = model.DateOfBirth;
                allData.Add(dataById);
                UserJsonSave(allData, filepath);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Delete(Guid id, string filepath)
        {
            try
            {
                var allData = DataDeserialize(filepath);
                var dataById = allData.FirstOrDefault(w => w.Id == id);
                allData.Remove(dataById);
                UserJsonSave(allData, filepath);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public void UserJsonSave(List<User> UserData, string filepath)
        {
            var ConvertedJson = JsonConvert.SerializeObject(UserData, Formatting.Indented);
            var ConvertedJsonRouteFormat = "{\"Users\":" + ConvertedJson + "}";
            File.WriteAllText(filepath, ConvertedJsonRouteFormat);
        }
    }
}
