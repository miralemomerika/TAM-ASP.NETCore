using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Interfaces;

namespace TAM.Service.Classes
{
    public class StatistikaService : IStatistikaService
    {
        private IKursService _kursService;
        private IPolaznikService _polaznikService;
        private IPredavacService _predavacService;
        private IDogadjajService _dogadjajService;

        public StatistikaService(IKursService kursService, IPolaznikService polaznikService, 
            IPredavacService predavacService, IDogadjajService dogadjajService)
        {
            _kursService = kursService;
            _polaznikService = polaznikService;
            _predavacService = predavacService;
            _dogadjajService = dogadjajService;
        }

        public StatistikaCentra GetStatistika()
        {
            var statistika = new StatistikaCentra
            {
                BrojKurseva = _kursService.GetAll().Count(),
                BrojDogadjaja = _dogadjajService.GetAll().Count(),
                BrojPolaznika = _polaznikService.GetAll().Count(),
                BrojPredavaca = _predavacService.GetAll().Count()
            };
            return statistika;
        }
    }
}
