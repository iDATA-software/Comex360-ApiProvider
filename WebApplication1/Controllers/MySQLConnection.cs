using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("DataConnection")]
    public class MySQLConnection: ControllerBase
    {
        [HttpGet]
        public string Get([FromHeader] dynamic authorization)
        {
            try
            {
                Console.WriteLine(authorization);
                return "teste";
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return "error";
            };
        }

        [HttpPost]
        public async Task<object> Post([FromBody] BodyRequest req)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(req.connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = req.selectCommand;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();
                return Ok();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500);
            };
        }
    }
}
