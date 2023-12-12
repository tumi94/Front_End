using Microsoft.AspNetCore.Mvc;
using Task = Task_Management_System__Client_.Models.Task;

namespace Task_Management_System__Client_.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApiGateways apiGateways;

        public TaskController(ApiGateways apiGateways)
        {
            this.apiGateways = apiGateways;
        }

        public IActionResult Index()
        {
            List<Task> tasks;
            //Api Get will come
            tasks = apiGateways.GetAllTasks();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Task task = new Task();
            return View(task);
        }
        [HttpPost]
        public IActionResult Create(Task task)
        {
            apiGateways.AddTask(task);
            //do the api create action and send the control to Index Action
            return RedirectToAction("Index");
        }

        public IActionResult Details(int Id)
        {
            Task task = new Task();
            //fetch the task from the API and show the task details in the Details View
            task = apiGateways.GetTaskById(Id);
            return View(task);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Task task;
            //fetch the task from the API and show the task details in the Edit View
            task = apiGateways.GetTaskById(Id);
            return View(task);
        }
        [HttpPost]
        public IActionResult Edit(Task task)
        {

            //do the API Edit  Action and send the control to Index Action
            apiGateways.UpdateTask(task);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Task task;
            //fetch the task from the API and show the task details in the Delete View
            task = apiGateways.GetTaskById(Id);
            return View(task);
        }
        [HttpPost]
        public IActionResult Delete(Task task)
        {

            //do the API Delete  Action and send the control to Index Action
            apiGateways.DeleteTask(task.Id);
            return RedirectToAction("Index");

        }
    }
}
