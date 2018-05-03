using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FilRouge.MVC.Services;
using FilRouge.MVC.ViewModels;

namespace FilRouge.MVC.API.Controllers
{
    //[Authorize]
    public class ContactController : ApiController
    {
        ContactService _contactService = new ContactService();
        // GET: api/Contact
        public IHttpActionResult GetAllContact()
        {
            try
            {
                List<ContactViewModel> contactViewModels = _contactService.GetAllContact();
                return Ok(contactViewModels);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        // GET: api/Contact/5
        public IHttpActionResult GetContact(string id)
        {
            try
            {
                ContactViewModel contactViewModels = _contactService.GetContactById(id);
                return Ok(contactViewModels);
            }
            catch (Exception) //InvalidOperationException
            {
                return NotFound();
            }
        }

    }
}
