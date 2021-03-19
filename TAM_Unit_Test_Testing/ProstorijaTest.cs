using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TAM.Core;
using TAM.Repository;
using TAM.Service.Classes;
using TAM.Test;

namespace TAM_Unit_Test_Testing
{
    [TestClass]
    public class ProstorijaTest
    {
        readonly ProstorijaService ProstorijaService;

        public ProstorijaTest()
        {
            ProstorijaService = new ProstorijaService(new Repository<Prostorija>(TestHelper.GetTAMDbContext()));
        }

        [TestMethod]
        public void ProstorijaCRUD()
        {
            var naziv = Guid.NewGuid().ToString().Substring(0, 20);
            Prostorija prostorija = new Prostorija
            {
                Naziv = naziv,
                BrojMjesta = 20
            };
            ProstorijaService.Add(prostorija);
            var rez = ProstorijaService.GetById(prostorija.Id);
            Assert.IsNotNull(rez);
            Assert.AreEqual(naziv, rez.Naziv);

            Assert.IsTrue(ProstorijaService.GetAll().ToList().Count >= 1);

            naziv = Guid.NewGuid().ToString().Substring(0, 20);
            rez.Naziv = naziv;
            rez.BrojMjesta = 15;
            ProstorijaService.Update(rez);
            var izmjenjen = ProstorijaService.GetById(rez.Id);
            Assert.AreEqual(naziv, izmjenjen.Naziv);
            Assert.AreEqual(15, izmjenjen.BrojMjesta);
            ProstorijaService.Delete(prostorija);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ProstorijaSaNegativnimKapacitetom()
        {
            ProstorijaService.Add(new Prostorija
            {
                Naziv = "Nova prostorija",
                BrojMjesta = -5
            });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ProstorijaBezNaziva()
        {
            ProstorijaService.Add(new Prostorija
            {
                BrojMjesta = 10
            });
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void ProstorijaSaPredugimNazivom()
        {
            ProstorijaService.Add(new Prostorija
            {
                Naziv = Guid.NewGuid().ToString(),
                BrojMjesta = 15
            });
        }
    }
}
