using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Threading.Tasks;
using System.Web;

namespace Igra.Hubs
{
    [HubName("tasks")]
    public class Tasks : Hub
    {
        /// <summary>
        /// Create a new task
        /// </summary>
        public bool Add(Task newTask)
        {
            try
            {
                Clients.All.taskAdded(5);
                return true;
            }
            catch (Exception ex)
            {
                Clients.Caller.reportError("Unable to create task. Make sure title length is between 10 and 140");
                return false;
            }
        }

        public string GetConnectionId()
        {
            return "ConnectionID";
        }


    }

    public class Task
    {
        public int taskId { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
        public DateTime lastUpdated { get; set; }

    }
}