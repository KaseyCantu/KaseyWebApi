using KaseyWebApi.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KaseyWebApi.Controllers.v1;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
[Produces("application/json")]
public class ValuesController : Controller
{
    private readonly List<string> _items = new()
    {
        "i01", "i02", "i03", "i04", "i05", "i06", "i07", "i08", "i09", "i10",
        "i11", "i12", "i13", "i14", "i15", "i16"
    };

    // GET api/values
    [HttpGet]
    public IActionResult Get([FromQuery] int pageSize = 25, [FromQuery] int pageIndex = 0)
    {
        var total = _items.Count;

        List<string> _itemsOnPage;
        _itemsOnPage = pageSize * pageIndex + pageSize > total
            ? _items.GetRange(pageSize * pageIndex, total - pageSize * pageIndex)
            : _items.GetRange(pageSize * pageIndex, pageSize);

        var model = new PaginatedItems<string>(
            pageIndex, pageSize, total, _itemsOnPage
        );

        return Ok(model);
    }

    // GET api/values/5
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        return Ok($"value {id}");
    }

    // POST api/values
    [HttpPost]
    public IActionResult InsertOrUpdate([FromBody] string value)
    {
        if (value == null)
        {
            return NotFound();
        }

        return Ok(_items.Append(value).ToList());
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] string value)
    {
        if (value == null)
        {
            return NotFound();
        }

        return Ok(value);
    }

    // DELETE api/values/5
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        return Ok($"{id}");
    }
}