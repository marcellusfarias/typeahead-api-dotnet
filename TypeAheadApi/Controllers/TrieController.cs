using Microsoft.AspNetCore.Mvc;
using TypeAheadApi.Data.Interfaces;
using TypeAheadApi.Data;

namespace TypeAheadApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TypeAheadController : ControllerBase
{
    private readonly ILogger<TypeAheadController> _logger;
    // private readonly Mutex _mutex = new Mutex();
    private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

    private ITrie _sharedTrie;

    public TypeAheadController(ILogger<TypeAheadController> logger, ITrie trie)
    {
        _logger = logger;
        _sharedTrie = trie;
    }

    [HttpGet]
    public string Get()
    {
        return "Hello world";
    }

    [HttpGet("{prefix}")]
    public ActionResult<IEnumerable<WordData>> GetWordsMatchPrefix(string prefix)
    {
        _logger.LogInformation($"prefix: {prefix}");

        _lock.EnterReadLock();

        var result = _sharedTrie.GetTypeaheadWords(prefix);

        _lock.ExitReadLock();

        return Ok(result);
    }

    public struct IncreasePopularityPayload
    {
        public string Name;
    }

    [HttpPost]
    public ActionResult<JsonContent> IncreasePopularity(IncreasePopularityPayload payload)
    {
        _logger.LogInformation($"payload: {payload}");

        _lock.EnterWriteLock();

        var result = _sharedTrie.IncreasePopularity(payload.Name);

        _lock.ExitWriteLock();

        return Created("", result);
    }
}