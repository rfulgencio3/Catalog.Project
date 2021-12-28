using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Catalog.Application.ViewModels
{
    public class ResponseBase
    {
        public ResponseBase()
        {
            Errors = new List<string>();
        }

        [JsonIgnore]
        public ICollection<string> Errors { get; set; }

        public ResponseBase AddError(string error)
        {
            Errors.Add(error);
            return this;
        }

    }
    public class ResponseBase<T> where T : new()
    {
        public ResponseBase()
        {
            Errors = new List<string>();
        }

        [JsonIgnore]
        public ICollection<string> Errors { get; set; }


        public ResponseBase<T> AddError(string error)
        {
            Errors.Add(error);
            return this;
        }


    }

}