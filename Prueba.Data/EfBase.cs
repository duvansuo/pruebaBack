using Prueba.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Data
{
    public class EfBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected PruebaFinaktivaEntities _context;
        /// <summary>
        /// 
        /// </summary>
        public EfBase()
        {

            _context = new PruebaFinaktivaEntities();
            _context.Configuration.ProxyCreationEnabled = false;

#if DEBUG
            _context.Database.Log = WriteEFLog;
#endif
        }

#if DEBUG
        private void WriteEFLog(string logMessage)
        {
            System.Diagnostics.Debug.WriteLine(logMessage, "EF Log");
        }
#endif

    }
}