using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SingleInstanceNoMutex
{
    /// <summary>
    /// This implements single instance.  It turns out that single instance is
    /// fairly easy with either mutex or event.  The other part of the job is
    /// to get the first instance to display its window in the foreground, which
    /// is much more difficult, especially if the app window is initially hidden.
    /// this class should be created in main().  the first instance will create
    /// the event.  The second instance will open it, then signal it and exit.  The first
    /// event starts a thread which waits indefinitely for the event to signal.
    /// That thread has to call Invoke() so that the main thread can update the GUI.
    /// This class must implement IDisposable to ensure that the unmanaged Win32 handle is released
    /// 
    /// SPECIAL CREDIT:
    /// I can't take any credit for this class.  Yes, I did make some mods, but
    /// the real credit goes to drdre2005, member on CodeProject.com
    /// His original code is available on the website.  The article is entitled
    /// "Enforcing Single Instance of a Hidden-Window Application"
    /// http://www.codeproject.com/KB/threads/SingleInstance.aspx
    /// This class just abstracts the things he's done to make it reusable.
    /// </summary>
    public class SingleInstance : IDisposable
    {
        private bool disposed = false;

        #region "Imported DLLs, kernel32.dll functions"
        //#############################################
        [DllImport("kernel32.dll")]
        static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);
        [DllImport("kernel32.dll")]
        static extern bool SetEvent(IntPtr hEvent);
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenEvent(UInt32 dwDesiredAccess, bool bInheritable, string lpName);
        [DllImport("kernel32.dll")]
        static extern bool CloseHandle(IntPtr hHandle);
        [DllImport("kernel32.dll")]
        static extern bool ResetEvent(IntPtr hEvent);
        //#############################################
        #endregion

        private string EVENT_NAME = string.Format("Local:{0}:{1}:{2}",
            Application.CompanyName,
            Application.ProductName,
            Application.ProductVersion);
        private const uint INFINITE = 0xFFFFFFFF;
        private const uint SYNCHRONIZE = 0x00100000;
        private const uint EVENT_MODIFY_STATE = 0x0002;

        private IntPtr eventHandle = IntPtr.Zero;		// unmanaged
        private Form form = null;

        /// <summary>
        /// Execute a form base application if another instance already running on
        /// the system activate previous one
        /// </summary>
        /// <param name="frmMain">main form</param>
        /// <returns>true if no previous instance is running</returns>
        public SingleInstance(Form mainForm)
        {
            if (InstanceAlreadyExists())
            {
                SignalInstance();
            }
            else
            {
                form = mainForm;
                Application.Run();
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Runs based on inherited form of System.Windows.Forms.ApplicationContext
        /// If another instance is already running on the system, activate the previous one
        /// </summary>
        /// <param name="appContext">ApplicationContext</param>
        /// <returns>true if no previous instance is running</returns>
        public SingleInstance(ApplicationContext appContext)
        {
            if (InstanceAlreadyExists())
            {
                SignalInstance();
            }
            else
            {
                form = appContext.MainForm;
                Application.Run();
                Environment.Exit(0);
            }
        }

        // Destructor
        ~SingleInstance()
        {
            Dispose(false);
        }

        // after creation, call this to determine if we are the first instance
        private bool InstanceAlreadyExists()
        {
            eventHandle = OpenEvent(EVENT_MODIFY_STATE | SYNCHRONIZE, false, EVENT_NAME);
            if (eventHandle == IntPtr.Zero)
            {
                eventHandle = CreateEvent(IntPtr.Zero, true, false, EVENT_NAME);
                if (eventHandle != IntPtr.Zero)
                {
                    Thread thread = new Thread(new ThreadStart(WaitForSignal));
                    thread.Start();
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        // an instance calls this when it detects that it is
        // the second instance.  Then it exits
        private void SignalInstance()
        {
            if (eventHandle != IntPtr.Zero)
                SetEvent(eventHandle);
        }

        // thread method will wait on the event, which will signal
        // if another instance tries to start
        private void WaitForSignal()
        {
            while (true)
            {
                uint result = WaitForSingleObject(eventHandle, INFINITE);

                if (result == 0)
                {

                    ResetEvent(eventHandle);
                    if (form != null) restoreInstance();
                }
                else
                    break;              
            }
        }

        private delegate void restoreInstanceDel();
        private void restoreInstance()
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new restoreInstanceDel(restoreInstance));
            }
            else
            {
                form.Show();
                form.WindowState = FormWindowState.Normal;
                form.Activate();
            }
        }

        #region IDisposable Members
        //#############################################################
        protected virtual void Dispose(bool disposeManagedResources)
        {
            if (!this.disposed)
            {
                if (disposeManagedResources)
                {
                    // dispose managed resources
                    if (form != null)
                    {
                        form.Dispose();
                        form = null;
                    }
                }
                // dispose unmanaged resources
                if (eventHandle != IntPtr.Zero)
                    CloseHandle(eventHandle);
                eventHandle = IntPtr.Zero;

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        //#############################################################        
        #endregion
    }
}