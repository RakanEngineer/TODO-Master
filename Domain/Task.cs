using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO_Master.Domain
{
    class Task
    {
        public int id;
        public string title;
        public DateTime dueDate;

        public Task(int id, string title, DateTime dueDate)
        {
            this.id = id;
            this.title = title;
            this.dueDate = dueDate;
        }
}
