using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.DataTransferObjects
{
    public class BaseResponseDto
    {

        public ResponseInfos Infos { get; set; }
        public Dictionary<string, object> Data { get; set; }
        public bool HasError => Infos.Errors.Count > 0;
        public HttpStatusCode StatusCode { get; set; }

        public BaseResponseDto()
        {
            Infos = new ResponseInfos();
            Data = new Dictionary<string, object>();
            StatusCode = HttpStatusCode.OK;
        }

        public void AddError(string error)
        {
            Infos.Errors.Add(error);
        }

        public void AddErrors(List<string> errors)
        {
            Infos.Errors.AddRange(errors);
        }

        public void AddInfo(string info)
        {
            Infos.Infos.Add(info);
        }

        public void AddMessage(string message)
        {
            Infos.Messages.Add(message);
        }

    }

    public class ResponseInfos
    {
        public List<string> Errors { get; set; }
        public List<string> Infos { get; set; }
        public List<string> Messages { get; set; }

        public ResponseInfos()
        {
            Errors = new List<string>();
            Infos = new List<string>();
            Messages = new List<string>();
        }

    }
}
