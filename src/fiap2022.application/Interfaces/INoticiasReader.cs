using fiap2022.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fiap2022.application.Interfaces
{
    public interface INoticiasReader
    {

        public List<Noticia> Load();
    }
}
