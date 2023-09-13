create database ILCSTest
go

use ILCSTest
go

create table biaya_impor(
id_simulasi uniqueidentifier not null,
kode_barang varchar(8),
Uraian_barang varchar(200),
Bm integer,
Nilai_komoditas float,
Nilai_bm float,
Waktu_Insert datetime,
Constraint PK_biaya primary key(id_simulasi)
);
go


