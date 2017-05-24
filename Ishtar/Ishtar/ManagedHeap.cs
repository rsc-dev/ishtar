using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ishtar
{
    /// <summary>
    /// Util class for Managed heap.
    /// </summary>
    class ManagedHeap
    {
        /// <summary>
        /// VMMap output CSV columns number for heap element.
        /// </summary>
        const int VMMAP_CSV_COUNT = 13;

        /// <summary>
        /// Parse VMMap output CSV file and return list of Managed Heap items.
        /// </summary>
        /// <param name="filePath">VMMap output CSV file.</param>
        /// <returns>List of ManagedHeapItem objects for each managed heap in output file.</returns>
        public static List<ManagedHeapItem> ParseVMMapCsv(string filePath)
        {
            List<ManagedHeapItem> ret = new List<ManagedHeapItem>();

            IEnumerable<string> lines = File.ReadAllLines(filePath, Encoding.ASCII).Where(l => l.Contains("Managed Heap"));
            foreach (string line in lines)
            {
                string[] split = line.Split(',');
                if (split.Length == VMMAP_CSV_COUNT)
                {
                    // Remove all non-ascii characters
                    string newline = line.Replace("?", String.Empty);
                    ret.Add(new ManagedHeapItem(newline));
                }
            }

            return ret;
        }

        public static List<ManagedHeapItem> ScanCurrentProcess()
        { 
            //Assemblies.DT.
            return null;
        }
    }

    /// <summary>
    /// Managed Heap object.
    /// </summary>
    class ManagedHeapItem
    {
        public string Address { get; private set; }
        public long Size { get; private set; }
        public long Commited { get; private set; }
        public long Priv { get; private set; }
        public long Total_ws { get; private set; }
        public long Priv_ws { get; private set; }
        public long Shareable_ws { get; private set; }
        public long Shared_ws { get; private set; }
        public long Locked_ws { get; private set; }

        public long Blocks { get; private set; }

        public string Protection { get; private set; }
        public string Details { get; private set; }

        /// <summary>
        /// Ctor. From VMMap output CSV line for managed heap.
        /// </summary>
        /// <param name="vmmap_scv_line">VMMap output CSV line for Managed heap.</param>
        public ManagedHeapItem(String vmmap_scv_line)
        {
            string[] items = vmmap_scv_line.Split(',');

            Debug.Assert(items.Length == 13, "Invalid VMMap SCV line!");

            Address = items[0].Replace("\"", String.Empty);
            Size = this.ItemToValue(items[2]);
            Commited = this.ItemToValue(items[3]);
            Priv = this.ItemToValue(items[4]);
            Total_ws = this.ItemToValue(items[5]);
            Priv_ws = this.ItemToValue(items[6]);
            Shareable_ws = this.ItemToValue(items[7]);
            Shared_ws = this.ItemToValue(items[8]);
            Locked_ws = this.ItemToValue(items[9]);

            string blocks = Regex.Replace(items[10], "(\\s+)|(\")", String.Empty);
            Blocks = String.IsNullOrEmpty(blocks) ? 0 : long.Parse(blocks);

            Protection = items[11];
            Details =items[12];
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="address">Heap address.</param>
        /// <param name="size">Heap size.</param>
        /// <param name="commited">Commited memory.</param>
        /// <param name="priv">Private memory.</param>
        /// <param name="total_ws">Total WS.</param>
        /// <param name="priv_ws">Private WS.</param>
        /// <param name="shareable_ws">Shareable WS.</param>
        /// <param name="shared_ws">Shared WS.</param>
        /// <param name="locked_ws">Locked WS.</param>
        /// <param name="blocks">Number of blocks.</param>
        /// <param name="protection">Protection string.</param>
        /// <param name="details">Details string.</param>
        public ManagedHeapItem(string address, long size, long commited,
                           long priv, long total_ws, long priv_ws,
                           long shareable_ws, long shared_ws,
                           long locked_ws, long blocks,
                           string protection, string details)
        {
            Address = address;
            Size = size;
            Commited = commited;
            Priv = priv;
            Total_ws = total_ws;
            Priv_ws = priv_ws;
            Shareable_ws = shareable_ws;
            Shared_ws = shared_ws;
            Locked_ws = locked_ws;
            Blocks = blocks;
            Protection = protection;
            Details = details;
        }

        /// <summary>
        /// Helper method for parsing VMMap output CSV element.
        /// </summary>
        /// <param name="vmmap_csv_item">Single column item.</param>
        /// <returns>Long value for given item.</returns>
        private long ItemToValue(string vmmap_csv_item)
        {
            long retVal = 0L;

            string item = Regex.Replace(vmmap_csv_item, "(\\s+)|(\")", String.Empty);
            int multiplier = item.Contains("bytes") ? 1 : 1024;
            item = item.Replace("bytes", String.Empty);

            long.TryParse(item, out retVal);

            return retVal * multiplier;
        }

        /// <summary>
        /// ToString method. Overriden.
        /// </summary>
        /// <returns>String representation of ManagedHeapItem.</returns>
        public override string ToString()
        {
            return String.Format("{0} ({1} b)", this.Address, this.Size);
        }
    }
}
