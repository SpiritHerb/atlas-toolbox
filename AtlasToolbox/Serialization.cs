using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox
{
    internal class Serialization
    {
        public static void Serialize(int value)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine("C:\\Users\\TheyCreeper\\Documents\\Dev\\AtlasToolbox\\AtlasToolbox\\regValues.csv")))
            {
                //Ecrit les données du joueur dans le fichier
                outputFile.WriteLine("copilot;recal");
                outputFile.WriteLine($"{value.ToString()};0");
            }
        }
    }
}
