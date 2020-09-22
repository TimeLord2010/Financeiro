using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Financeiro.Scripts.DB.DynamoDB {

    public static class UploadToDynamoDBTable {

        const string Url = "https://48hn3p40k2.execute-api.sa-east-1.amazonaws.com/clinica/uploadtolambda";

        public static async Task<HttpResponseMessage> Upload (string table_name, object obj) {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(new {
                TableName = table_name,
                Content = obj
            });
            return await WebMethods.Request(HttpMethod.Post, Url, json);
        }

    }
}
