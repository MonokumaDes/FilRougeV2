using System.Web;
using System.Web.Mvc;
using FilRouge.MVC.Services;
using FilRouge.MVC.ViewModels;
using Microsoft.AspNet.Identity.Owin;

namespace FilRouge.MVC.Controllers
{
    //[Authorize]
    public class ContactController : Controller
    {

        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private ContactService _contactService = new ContactService();

        // GET: Contact
        public ActionResult Index()
        {
            var contacts = _contactService.GetAllContact();
            return View(contacts);
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Roles = _contactService.GetListItemsRoles();
            ViewBag.ModeEdition = false;
            return View(new ContactViewModel());
        }

        [HttpPost]
        public ActionResult Create(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                _contactService.AddContact(contactViewModel, UserManager);
                return RedirectToAction("Index", "Contact");
            }
            return View(contactViewModel);
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            ContactViewModel contactViewModel = _contactService.GetContactById(id);

            var roles = _contactService.GetListItemsRoles();
            contactViewModel.OldRoleId = contactViewModel.RoleId;

            ViewBag.Roles = roles;
            ViewBag.ModeEdition = true;
            return View(contactViewModel);
        }

        [HttpPost]
        public ActionResult Edit(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                _contactService.EditContact(contactViewModel, UserManager);
                return RedirectToAction("Index", "Contact");
            }
            return View(contactViewModel);
        }

        public ActionResult Details(string id)
        {
            ContactViewModel contactViewModel = _contactService.GetContactById(id);
            return View(contactViewModel);
        }

    }
}