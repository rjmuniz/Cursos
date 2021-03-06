﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using FluentNHibernate.Testing.Values;
using MBCorpHealthTest.Infraestrutura.ContextoBooking;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MBCorpHealthTestTest
{
    [TestClass]
    public class TestesIntegradosContextoBooking
    {

        [TestMethod]
        public void TestParaTestarOJenkins ()
    { }

        [TestMethod]
        public void ComoUsuarioDoServicoDeAgendamentoQueroAAgendaDisponivelParaUmTipoDeExameEUmCentroDeDiagnostico()
        {
            //Arrange

            IMeuServicoDeBooking meuServicoDeBooking = new ServicoDeBookingRest();

            //act

            var datasDisponiveis = meuServicoDeBooking.RetornarAgendaDisponivelParaOExameEUnidadeDEDiagnostico("1", "1");

            //Assert
            Assert.IsTrue(datasDisponiveis.Count >0);

        }

        //target
        public interface IMeuServicoDeBooking
        {
             List<DateTime> RetornarAgendaDisponivelParaOExameEUnidadeDEDiagnostico(string cbhpm,string CodigoDaUnidadeDeDiagnostico);
        }


        //Adapter
          class ServicoDeBookingWS:IMeuServicoDeBooking
          {
              public List<DateTime> RetornarAgendaDisponivelParaOExameEUnidadeDEDiagnostico(string cbhpm, string CodigoDaUnidadeDeDiagnostico)
              {
                  throw new NotImplementedException();
              }
          }

        //Adapter
        class ServicoDeBookingRest:IMeuServicoDeBooking
        {
            public List<DateTime> RetornarAgendaDisponivelParaOExameEUnidadeDEDiagnostico(string cbhpm, string CodigoDaUnidadeDeDiagnostico)
            {
                HttpClient cliente = null;
                Uri enderecoDoServico = new Uri("http://servicodebooking.azurewebsites.net");

                cliente = new HttpClient();
                cliente.BaseAddress = enderecoDoServico;
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                

                HttpResponseMessage resposta =
                    cliente.GetAsync(string.Format("api/ServicoBooking?CBHPM={0}&codigoCentroDeDiagnostico={1}", 1, 1))
                        .Result;


                List<DateTime> datasDisponiveis = new List<DateTime>();
                if (resposta.IsSuccessStatusCode)
                {
                    datasDisponiveis.AddRange(resposta.Content.ReadAsAsync<IEnumerable<DateTime>>().Result);
                }

                return datasDisponiveis;
            }
        }


    }
}
