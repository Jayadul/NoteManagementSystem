using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NMS.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NMS.Service
{
    public class NoteService
    {
        public bool Create(Note dto, string filepath)
        {
            try
            {
                var NoteData = DataDeserialize(filepath);
                NoteData.Add(dto);
                NoteJsonSave(NoteData, filepath);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool UniqueCheck(Guid NoteId, string filepath)
        {
            var NoteData = DataDeserialize(filepath);
            bool alreadyExists = false;
            foreach (var s in NoteData)
            {
                if (s.Id == NoteId)
                {
                    alreadyExists = true;
                }
            }
            return alreadyExists;
        }
        public List<Note> DataDeserialize(string filepath)
        {
            var NewJson = new List<Note>();
            var CurrentJson = File.ReadAllText(filepath);
            if (!String.IsNullOrEmpty(CurrentJson))
            {
                var CurrentJsonParse = JObject.Parse(CurrentJson);
                var CurrentJsonRoutes = CurrentJsonParse["Notes"];
                NewJson = JsonConvert.DeserializeObject<List<Note>>(CurrentJsonRoutes.ToString());
            }          
            return NewJson;
        }
        public List<Note> GetByUser(Guid id, string filepath)
        {
            var allData = DataDeserialize(filepath);
            var dataByUser = allData.Where(w => w.Id == id).ToList();
            return dataByUser;
        }
        public bool Update(Note model, string filepath)
        {
            try
            {
                var allData = DataDeserialize(filepath);
                var dataById = allData.FirstOrDefault(w => w.Id == model.Id);
                allData.Remove(dataById);
                dataById.Id = model.Id;
                dataById.NoteType = model.NoteType;
                dataById.Text = model.Text;
                dataById.Url = model.Url;
                dataById.CreatedBy = model.CreatedBy;
                dataById.Date = model.Date;
                allData.Add(dataById);
                NoteJsonSave(allData, filepath);
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
                NoteJsonSave(allData, filepath);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public void NoteJsonSave(List<Note> NoteData, string filepath)
        {
            var ConvertedJson = JsonConvert.SerializeObject(NoteData, Formatting.Indented);
            var ConvertedJsonRouteFormat = "{\"Notes\":" + ConvertedJson + "}";
            File.WriteAllText(filepath, ConvertedJsonRouteFormat);
        }
    }
}
