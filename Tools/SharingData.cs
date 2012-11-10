using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    /// <summary>
    /// Classe utilisée pour transmettre des informations entre des modules lors de leurs chargement
    /// </summary>
    public class SharingData
    {
        public string DestinationModuleName { get; set; }

        public string UserName { get; set; }
    }
}