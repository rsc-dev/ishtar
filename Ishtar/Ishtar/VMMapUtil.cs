using System.Collections;
using System.IO;
using System.Linq;

namespace Ishtar
{
    class VMMapUtil
    {
        public static void ParseCsv(string fileName)
        {
            var ret = new ArrayList();

            StreamReader reader = new StreamReader(File.OpenRead(fileName));
            var lines = File.ReadAllLines(fileName).Where(l => l.Contains("Managed Heap"));

           
        }
    }
}
