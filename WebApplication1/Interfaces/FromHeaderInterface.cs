using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Interfaces
{
    public class FromHeaderInterface
    {
        [FromHeader]
        public string Authorization { get; set; }
    }
}
