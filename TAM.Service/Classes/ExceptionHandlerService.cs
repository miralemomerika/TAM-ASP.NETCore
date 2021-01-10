using System;
using System.Collections.Generic;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class ExceptionHandlerService : IExceptionHandlerService
    {
        readonly IRepository<ExceptionHandler> _exceptionHandlerRepository;

        public ExceptionHandlerService(IRepository<ExceptionHandler> repository)
        {
            _exceptionHandlerRepository = repository;
        }

        public void Add(ExceptionHandler exception)
        {
            _exceptionHandlerRepository.Add(exception);
        }

        public void Delete(ExceptionHandler exception)
        {
            _exceptionHandlerRepository.Delete(exception);
        }

        public IEnumerable<ExceptionHandler> GetAll()
        {
            return _exceptionHandlerRepository.GetAll();
        }

        public ExceptionHandler GetById(int id)
        {
            return _exceptionHandlerRepository.GetById(id);
        }

        public void Update(ExceptionHandler exception)
        {
            _exceptionHandlerRepository.Update(exception);
        }
    }
}
