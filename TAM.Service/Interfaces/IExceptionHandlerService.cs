using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;

namespace TAM.Service.Interfaces
{
    public interface IExceptionHandlerService
    {
        public IEnumerable<ExceptionHandler> GetAll();
        public ExceptionHandler GetById(int id);
        public void Update(ExceptionHandler exception);
        public void Delete(ExceptionHandler exception);
        public void Add(ExceptionHandler exception);
    }
}
