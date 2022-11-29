using Assignment_4_SI.Models.API.RequestObjects;
using Assignment_4_SI.Models.API.ResponseObjects;
using Assignment_4_SI.Models.Database.EventRepository;
using Assignment_4_SI.Models.Entities;
using Assignment_4_SI.Utility;
using Assignment_4_SI.Validation;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_4_SI.Controllers;

[Produces("application/json")]
[Route("api/event")]
public class EventController : Controller
{
    private readonly IEventRepository _repo;

    public EventController(IEventRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<EventResponseObject>))]
    public IActionResult Get()
    {
        return Ok(_repo.GetAll().Select(e => e.ToResponseObject()));
    }

    // GET: api/event/5
    [HttpGet("{id}", Name = "GetEvent")]
    [ProducesResponseType(200, Type = typeof(EventResponseObject))]
    [ProducesResponseType(404)]
    public IActionResult GetById(long id)
    {
        Event evt = _repo.Get(id);

        if (evt == null)
        {
            return NotFound("No event with the given id found");
        }
        return Ok(evt.ToResponseObject());
    }
    
    //// POST: api/event
    [HttpPost]
    [RequestFormSizeLimit(valueCountLimit: 12000, Order = 1)]
    [ProducesResponseType(201, Type = typeof(EventResponseObject))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> PostAsync([FromForm] EventCreateObject createObj)
    {
        if (createObj == null)
        {
            return BadRequest();
        }
        //check for unique name
        if (createObj.Name != null)
        {
            Event e = _repo.GetByName(createObj.Name);
            if (e != null)
            {
                ModelState.AddModelError("name", "Name already in use");
            }
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Event createEvent = Event.FromCreateObject(createObj);

        //upload the image and retrieve url
        if (createObj.Image != null)
        {
            await FileUtility.ProcessFile(new List<IFormFile>() { createObj.Image}, createEvent);
        }

        //INSERT new event
        _repo.Create(createEvent);

        return CreatedAtRoute("GetEvent", new { id = createEvent.Id }, createEvent.ToResponseObject());
    }


}