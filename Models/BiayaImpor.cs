using System.Numerics;

namespace ILCSTest.Models
{
    public class BiayaImpor
    {
        public Guid id_simulasi { get; set; }
        public string? kode_barang { get; set; }
        public string? Uraian_barang { get; set; } 
        public Int32 Bm { get; set; }    
            public float Nilai_komoditas { get; set;}
        public float Nilai_bm { get; set;}
        public DateTime Waktu_Insert { get; set;}
                    
    }
}
