﻿namespace MBCorpHealthTest.Dominio.ContextoAdministracaoDeAgendamentosDeExame.Entidades
{
    public class Credencial
    {
        public Credencial(string user, string senha)
        {
            User = user;
            Senha = senha;
        }

        protected Credencial()
        {
        }

        public string User { get; protected set; }
        public string Senha { get; protected set; }
    }
}