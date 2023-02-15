using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using System.Data;
using MySqlConnector;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    [Route("DataConnection")]
    public class MySQLConnection: ControllerBase
    {
        //[HttpPost]
        //public string PostTeste([FromHeader] FromHeaderInterface header, [FromBody] string testeBody)
        //{
        //    try
        //    {
        //        var teste = Validation.ValidateToken(header.Authorization);
        //        if(teste == null)
        //        {
        //            return "token inválido";
        //        }
        //        return $"token válido, você mandou no body: {testeBody}";
        //    }
        //    catch (Exception error)
        //    {
        //        Console.WriteLine(error.Message);
        //        return "error";
        //    };
        //}

        [HttpPost]
        public void Post([FromBody] BodyRequest req)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(req.connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(req.selectCommand, con))
                {
                    con.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row;
                    foreach (DataRow dr in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                        rows.Add(row);
                    }
                    Console.WriteLine(JsonConvert.SerializeObject(rows));
                }
            }
        }
    }
}
