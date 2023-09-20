using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Controls;

namespace MeNoisy_soundboard.App.Base
{
    public interface IWork
    {
        public Task Do();
    }

    public class Work : IWork
    {
        public TaskCompletionSource TaskCompletion { get; set; } = new TaskCompletionSource();

        public object? Context { get; set; } = null;

        public async Task Do()
        {
            await TaskCompletion.Task;
        }

        public void Cancel()
        {
            TaskCompletion.SetCanceled();
        }
    }

    public class Workflow : IWork
    {
        protected List<IWork> Works { get; set; }

        public async Task Do()
        {

            int currentIndex = 0;
            while(currentIndex >= 0 && currentIndex < Works.Count)
            {
                try
                {
                    await Works[currentIndex].Do();
                }
                catch (TaskCanceledException)
                {
                    currentIndex--;
                    continue;
                }
                currentIndex++;
            }

            if (currentIndex < 0) throw new TaskCanceledException();
        }
    }
}
