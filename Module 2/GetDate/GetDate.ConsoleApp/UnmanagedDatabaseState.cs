using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace GetDate.ConsoleApp
{
    public class UnmanagedDatabaseState : DatabaseState
    {
        private SqlCommand _command;

        private IntPtr _unmanagedPointer;

        public override string GetDate()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("UnmanagedDatabaseState");
            }

            var sqlDate = base.GetDate();
            if (_command == null)
            {
                _command = _connection.CreateCommand();
            }
            if (_unmanagedPointer == IntPtr.Zero)
            {
                _unmanagedPointer = Marshal.AllocHGlobal(100 * 1024 * 1024);
            }
            return sqlDate;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && // This means that Dispose() is called from the code that consumes this class, not from the finalizer invoked by GC.
                _command != null)
            {
                _command.Dispose();
                _command = null;
            }
            
            // Free up unmanaged resources as well unconditionally (i.e. both when calling Dispose() by the consumer and as the part of GC process.
            // It's important to skip managed objects disposing in the case of being part of GC cleanup process becaise managed objects ("_command" in our case)
            // may already have been reclaimed by the GC themselves, so we cannot guarantee that we can access their Dispose() method.
            if (_unmanagedPointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_unmanagedPointer);
                _unmanagedPointer = IntPtr.Zero;
            }
            base.Dispose(disposing);
        }

        ~UnmanagedDatabaseState()
        {
            Dispose(false);
        }
    }
}
