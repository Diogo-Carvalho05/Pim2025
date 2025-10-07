using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BemAgendado.Model;

namespace BemAgendado.Controler
{
    public static class UsuarioSessao
    {
        public static Usuario UsuarioLogado { get; set; }
        public static bool EstaLogado => UsuarioLogado != null;

        public static void Logout()
        {
            UsuarioLogado = null;
        }
    }
}
