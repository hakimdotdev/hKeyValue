using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Net;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using NRedisStack;

namespace hKeyValue.Controllers
{
    namespace KeyValueStoreAPI.Controllers
    {
        [Route("[controller]")]
        [ApiController]
        public class KeyValueController : ControllerBase
        {
            private readonly IDatabase _redisDb;
            private readonly ConnectionMultiplexer _connectionMultiplexer;

            public KeyValueController(ConnectionMultiplexer connectionMultiplexer)
            {
                _connectionMultiplexer = connectionMultiplexer;
                _redisDb = connectionMultiplexer.GetDatabase();
            }

            [HttpPut("{key}")]
            public async Task<IActionResult> PutValue(string key, [FromBody] string value)
            {
                var setResult = await _redisDb.StringSetAsync(key, value, when: When.NotExists);

                if (!setResult)
                {
                    return StatusCode((int)HttpStatusCode.Conflict, "Key already exists.");
                }

                return CreatedAtAction(nameof(GetValue), new { key }, value);
            }

            [HttpGet("{key}")]
            public async Task<IActionResult> GetValue(string key)
            {
                string? value = await _redisDb.StringGetAsync(key);

                if (!string.IsNullOrEmpty(value))
                {
                    return Ok(value);
                }
                else
                {
                    return NotFound();
                }
            }

            [HttpDelete("{key}")]
            public async Task<IActionResult> DeleteValue(string key)
            {
                await _redisDb.KeyDeleteAsync(key);

                return NoContent();
            }
        }
    }
}
