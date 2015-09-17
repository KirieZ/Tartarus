using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Provides ability to Add Tasks to the Critical Tasks queue which is processed by the ThreadScheduler
    /// <note>The Critical Tasks should never exceed 4</note>
    /// </summary>
    public class Threads
    {
        /// <summary>
        /// Tasks to be processed
        /// </summary>
        public static List<Task> Tasks = new List<Task>();

        public static ThreadScheduler Scheduler = new ThreadScheduler();
        /// <summary>
        /// Factory used to create new Tasks which will be stored in Tasks
        /// </summary>
        public static TaskFactory Factory = new TaskFactory(Scheduler);
    }

    public class ThreadScheduler : TaskScheduler
    {
        [ThreadStatic]
        bool _isProcessing;

        readonly LinkedList<Task> _taskQueue = new LinkedList<Task>();

        readonly int _maxThreads;

        int _delegatesRunning = 0;

        // TODO : Determine the MaxThreads by cpu cores * 2
        public ThreadScheduler()
        {
            _maxThreads = Globals.MaxThreads;
        }

        protected sealed override void QueueTask(Task _task)
        {
            lock (_taskQueue)
            {
                _taskQueue.AddLast(_task);
                if (_delegatesRunning < _maxThreads)
                {
                    ++_delegatesRunning;
                    NotifyThreadPool();
                }
            }
        }

        private void NotifyThreadPool()
        {
            ThreadPool.UnsafeQueueUserWorkItem(_ => 
            {
                // Note that the current thread is now processing work items. 
                // This is necessary to enable inlining of tasks into this thread.
                _isProcessing = true;
                try
                {
                    // Process all available items in the queue. 
                    while (true)
                    {
                        Task item;
                        lock (_taskQueue)
                        {
                            // When there are no more items to be processed, 
                            // note that we're done processing, and get out. 
                            if (_taskQueue.Count == 0)
                            {
                                --_delegatesRunning;
                                break;
                            }

                            // Get the next item from the queue
                            item = _taskQueue.First.Value;
                            _taskQueue.RemoveFirst();
                        }

                        // Execute the task we pulled out of the queue 
                        base.TryExecuteTask(item);
                    }
                }
                // We're done processing items on the current thread 
                finally { _isProcessing = false; }
            }, null);
        }

        // Attempts to execute the specified task on the current thread.  
        protected sealed override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            // If this thread isn't already processing a task, we don't support inlining 
            if (!_isProcessing) return false;

            // If the task was previously queued, remove it from the queue 
            if (taskWasPreviouslyQueued)
                // Try to run the task.  
                if (TryDequeue(task))
                    return base.TryExecuteTask(task);
                else
                    return false;
            else
                return base.TryExecuteTask(task);
        }

        // Attempt to remove a previously scheduled task from the scheduler.  
        protected sealed override bool TryDequeue(Task task)
        {
            lock (_taskQueue) return _taskQueue.Remove(task);
        }

        // Gets the maximum concurrency level supported by this scheduler.  
        public sealed override int MaximumConcurrencyLevel { get { return _maxThreads; } }

        // Gets an enumerable of the tasks currently scheduled on this scheduler.  
        protected sealed override IEnumerable<Task> GetScheduledTasks()
        {
            bool lockTaken = false;
            try
            {
                Monitor.TryEnter(_taskQueue, ref lockTaken);
                if (lockTaken) return _taskQueue;
                else throw new NotSupportedException();
            }
            finally
            {
                if (lockTaken) Monitor.Exit(_taskQueue);
            }
        }
    }
}
